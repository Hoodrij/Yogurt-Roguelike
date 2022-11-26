using System;
using Cysharp.Threading.Tasks;

namespace Core.Tools
{
    public static partial class Wait
    {
        public static UniTask Update => UniTask.DelayFrame(1);
        public static UniTask LateUpdate => UniTask.DelayFrame(1, PlayerLoopTiming.PostLateUpdate);
        public static UniTask FixedUpdate => UniTask.DelayFrame(1, PlayerLoopTiming.FixedUpdate);
		
        public static UniTask Until(Func<bool> func) => UniTask.WaitUntil(func);
        public static UniTask While(Func<bool> func) => UniTask.WaitWhile(func);

        public static UniTask Seconds(float duration) => UniTask.Delay(TimeSpan.FromSeconds(duration));
        public static UniTask SecondsRealtime(float duration) => UniTask.Delay(TimeSpan.FromSeconds(duration), true);
    }
}