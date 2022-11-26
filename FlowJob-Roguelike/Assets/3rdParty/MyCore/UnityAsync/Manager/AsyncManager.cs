using System.Collections;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

namespace UnityAsync
{
    [AddComponentMenu("")] // don't show in menu
	public partial class AsyncManager : MonoBehaviour
	{
		/// <summary>
		/// The frame count in the currently active update loop.
		/// </summary>
		public static int CurrentFrameCount { get; private set; }
		
		/// <summary>
		/// The time in the currently active update loop.
		/// </summary>
		public static float CurrentTime { get; private set; }
		
		/// <summary>
		/// The unscaled time in the currently active update loop.
		/// </summary>
		public static float CurrentUnscaledTime { get; private set; }
		
		/// <summary>
		/// Unity's <see cref="System.Threading.SynchronizationContext"/>.
		/// </summary>
		public static SynchronizationContext UnitySyncContext { get; private set; }
		
		/// <summary>
		/// Background (thread pool) <see cref="System.Threading.SynchronizationContext"/>.
		/// </summary>
		public static SynchronizationContext BackgroundSyncContext { get; private set; }
		
		/// <summary>
		/// Returns true if called from Unity's <see cref="System.Threading.SynchronizationContext"/>.
		/// </summary>
		public static bool InUnityContext => Thread.CurrentThread.ManagedThreadId == unityThreadId;

		public static AsyncManager Instance { get; private set; }

		static int unityThreadId, updateCount, lateCount, fixedCount;
		public ContinuationProcessorGroup updates, lateUpdates, fixedUpdates;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		static void Initialize()
		{
			void OnTaskSchedulerOnUnobservedTaskException(object _, UnobservedTaskExceptionEventArgs e) => Debug.LogException(e.Exception);
			
			TaskScheduler.UnobservedTaskException -= OnTaskSchedulerOnUnobservedTaskException;
			TaskScheduler.UnobservedTaskException += OnTaskSchedulerOnUnobservedTaskException;
			
			unityThreadId = Thread.CurrentThread.ManagedThreadId;
			UnitySyncContext = SynchronizationContext.Current;

			BackgroundSyncContext = new SynchronizationContext();

			Instance = new GameObject("Async Manager").AddComponent<AsyncManager>();
			Instance.gameObject.hideFlags = HideFlags.HideInHierarchy;
			DontDestroyOnLoad(Instance);
			
			Instance.updates = new ContinuationProcessorGroup();
			Instance.lateUpdates = new ContinuationProcessorGroup();
			Instance.fixedUpdates = new ContinuationProcessorGroup();
			
			CurrentFrameCount = 0;
			CurrentTime = 0;
			CurrentUnscaledTime = 0;
			updateCount = lateCount = fixedCount = 0;
		}

		/// <summary>
		/// Initializes the manager explicitly. Typically used when running Unity Editor tests (NUnit unit tests).
		/// </summary>
		public static void InitializeForEditorTests()
		{
			Initialize();
			
			// Do not run tasks in background when testing:
			BackgroundSyncContext = UnitySyncContext;
		}

		/// <summary>
		/// Queues a continuation.
		/// Intended for internal use only - you shouldn't need to invoke this.
		/// </summary>
		public void AddContinuation<T>(in T cont) where T : IAwaitInstructionAwaiter
		{
			switch(cont.Scheduler)
			{
				case FrameScheduler.Update:
					updates.Add(cont);
					break;

				case FrameScheduler.LateUpdate:
					lateUpdates.Add(cont);
					break;

				case FrameScheduler.FixedUpdate:
					fixedUpdates.Add(cont);
					break;
			}
		}

		/// <summary>
		/// Start a coroutine from any context without requiring a MonoBehaviour.
		/// </summary>
		public new static Coroutine StartCoroutine(IEnumerator coroutine) => ((MonoBehaviour)Instance).StartCoroutine(coroutine);

		void Update()
		{
			CurrentFrameCount = ++updateCount;
			
			if(CurrentFrameCount <= 1)
				return;
			
			CurrentTime = Time.time;
			CurrentUnscaledTime = Time.unscaledTime;
			
			updates?.Process();
		}

		void LateUpdate()
		{
			CurrentFrameCount = ++lateCount;
			
			if(CurrentFrameCount <= 1)
				return;
			
			lateUpdates?.Process();
		}

		void FixedUpdate()
		{
			CurrentFrameCount = ++fixedCount;
			
			if(CurrentFrameCount <= 1)
				return;
						
			fixedUpdates?.Process();
		}

		private void OnDestroy()
		{
			UnitySyncContext = null;

			BackgroundSyncContext = null;

			updates = null;
			lateUpdates = null;
			fixedUpdates = null;

			if (Instance != null)
			{
				Instance.StopAllCoroutines();
				Instance = null;
			}
		}
	}
}