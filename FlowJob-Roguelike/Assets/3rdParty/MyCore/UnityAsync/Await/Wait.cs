using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityAsync
{
	public static partial class Wait
	{
		public static AwaitInstructionAwaiter<WaitForFrames> Update => new AwaitInstructionAwaiter<WaitForFrames>(new WaitForFrames(1));
		public static AwaitInstructionAwaiter<WaitForFrames> LateUpdate => new AwaitInstructionAwaiter<WaitForFrames>(new WaitForFrames(1), FrameScheduler.LateUpdate);
		public static AwaitInstructionAwaiter<WaitForFrames> FixedUpdate => new AwaitInstructionAwaiter<WaitForFrames>(new WaitForFrames(1), FrameScheduler.FixedUpdate);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SynchronizationContext UnitySyncContext() => AsyncManager.UnitySyncContext;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SynchronizationContext BackgroundSyncContext() => AsyncManager.BackgroundSyncContext;
		
		public static WaitUntil Until(Func<bool> func) => new WaitUntil(func);

		public static WaitWhile While(Func<bool> func) => new WaitWhile(func);

		public static WaitForSeconds Seconds(float duration) => new WaitForSeconds(duration);

		public static WaitForSecondsRealtime SecondsRealtime(float duration) => new WaitForSecondsRealtime(duration);
	}
}