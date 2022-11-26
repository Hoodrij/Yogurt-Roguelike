using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityAsync
{
    public partial class Wait
    {
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitForFrames> WaitUpdate(this MonoBehaviour mono) 
			=> Update.ConfigureAwait(mono);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitForFrames> WaitLateUpdate(this MonoBehaviour mono) 
			=> LateUpdate.ConfigureAwait(mono);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitForFrames> WaitFixedUpdate(this MonoBehaviour mono) 
			=> FixedUpdate.ConfigureAwait(mono);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitForSeconds> WaitSeconds(this MonoBehaviour mono, float duration) 
			=> Seconds(duration).ConfigureAwait(mono);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitForSecondsRealtime> WaitSecondsRealtime(this MonoBehaviour mono, float duration) 
			=> SecondsRealtime(duration).ConfigureAwait(mono);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitUntil> WaitUntil(this MonoBehaviour mono, Func<bool> condition) 
			=> Until(condition).ConfigureAwait(mono);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitWhile> WaitWhile(this MonoBehaviour mono, Func<bool> condition) 
			=> While(condition).ConfigureAwait(mono);
    }
}