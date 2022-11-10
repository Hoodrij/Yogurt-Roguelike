using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using FlowJob;

namespace Roguelike.Tools
{
    public static class EntityEx
    {
        public static async void SetParent(this Entity child, Entity parent)
        {
            await UniTask.WaitWhile(() => parent.Exist);
            child.Kill();
        }  
    }
}