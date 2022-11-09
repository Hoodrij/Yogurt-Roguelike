using System;
using Cysharp.Threading.Tasks;

namespace Core.Tools
{
    public static class LifetimeEx
    {
        public static UniTask WaitUpdate(this ILifetimeOwner lifetimeOwner)
            => UniTask.NextFrame(lifetimeOwner.Lifetime);

        public static UniTask WaitLateUpdate(this ILifetimeOwner lifetimeOwner)
            => UniTask.NextFrame(PlayerLoopTiming.PostLateUpdate, lifetimeOwner.Lifetime);
	
        public static UniTask WaitFixedUpdate(this ILifetimeOwner lifetimeOwner) 
            => UniTask.NextFrame(PlayerLoopTiming.FixedUpdate, lifetimeOwner.Lifetime);

        public static UniTask WaitSeconds(this ILifetimeOwner lifetimeOwner, float duration)
            => UniTask.Delay((int) duration * 1000, DelayType.DeltaTime, PlayerLoopTiming.Update, lifetimeOwner.Lifetime);
	
        public static UniTask WaitSecondsRealtime(this ILifetimeOwner lifetimeOwner, float duration) 
            => UniTask.Delay((int) duration / 1000, DelayType.Realtime, PlayerLoopTiming.Update, lifetimeOwner.Lifetime);

        public static UniTask WaitUntil(this ILifetimeOwner lifetimeOwner, Func<bool> funk)
            => UniTask.WaitUntil(funk, PlayerLoopTiming.Update, lifetimeOwner.Lifetime);
	
        public static UniTask WaitWhile(this ILifetimeOwner lifetimeOwner, Func<bool> funk) 
            => UniTask.WaitWhile(funk, PlayerLoopTiming.Update, lifetimeOwner.Lifetime);
    }
}