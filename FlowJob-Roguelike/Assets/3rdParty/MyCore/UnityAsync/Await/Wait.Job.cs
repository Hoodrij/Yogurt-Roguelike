using System;
using System.Runtime.CompilerServices;
using Core.Tools;

namespace UnityAsync
{
    public partial class Wait
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitForFrames> WaitUpdate(this ILifetimeOwner lifetimeOwner) 
			=> Update.ConfigureAwait(lifetimeOwner.Lifetime);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitForFrames> WaitLateUpdate(this ILifetimeOwner lifetimeOwner) 
			=> LateUpdate.ConfigureAwait(lifetimeOwner.Lifetime);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitForFrames> WaitFixedUpdate(this ILifetimeOwner lifetimeOwner) 
			=> FixedUpdate.ConfigureAwait(lifetimeOwner.Lifetime);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitForSeconds> WaitSeconds(this ILifetimeOwner lifetimeOwner, float duration) 
			=> Seconds(duration).ConfigureAwait(lifetimeOwner.Lifetime);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitForSecondsRealtime> WaitSecondsRealtime(this ILifetimeOwner lifetimeOwner, float duration) 
			=> SecondsRealtime(duration).ConfigureAwait(lifetimeOwner.Lifetime);
  
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitUntil> WaitUntil(this ILifetimeOwner lifetimeOwner, Func<bool> condition) 
			=> Until(condition).ConfigureAwait(lifetimeOwner.Lifetime);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitWhile> WaitWhile(this ILifetimeOwner lifetimeOwner, Func<bool> condition) 
			=> While(condition).ConfigureAwait(lifetimeOwner.Lifetime);
    
    }
}