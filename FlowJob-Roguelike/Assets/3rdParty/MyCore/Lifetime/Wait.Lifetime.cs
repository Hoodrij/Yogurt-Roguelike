using System;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks;

namespace Core.Tools
{
    public static partial class Wait
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask WaitUpdate(this ILifetimeOwner lifetimeOwner) 
            => Update.AttachExternalCancellation(lifetimeOwner.Lifetime);
		
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask WaitLateUpdate(this ILifetimeOwner lifetimeOwner) 
            => LateUpdate.AttachExternalCancellation(lifetimeOwner.Lifetime);
		
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask WaitFixedUpdate(this ILifetimeOwner lifetimeOwner) 
            => FixedUpdate.AttachExternalCancellation(lifetimeOwner.Lifetime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask WaitSeconds(this ILifetimeOwner lifetimeOwner, float duration)
            => Seconds(duration).AttachExternalCancellation(lifetimeOwner.Lifetime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask WaitSecondsRealtime(this ILifetimeOwner lifetimeOwner, float duration)
            => SecondsRealtime(duration).AttachExternalCancellation(lifetimeOwner.Lifetime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask WaitUntil(this ILifetimeOwner lifetimeOwner, Func<bool> func)
            => Until(func).AttachExternalCancellation(lifetimeOwner.Lifetime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UniTask WaitWhile(this ILifetimeOwner lifetimeOwner, Func<bool> func)
            => While(func).AttachExternalCancellation(lifetimeOwner.Lifetime);
    }
}