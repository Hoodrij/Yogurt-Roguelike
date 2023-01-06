using System;
using Core.Tools.ExtensionMethods;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using FlowJob;
using UnityEngine;

namespace Roguelike
{
    public class AgentView : MonoBehaviour, IComponent, IDisposable
    {
        public enum Animation
        {
            Attack = 1,
            Hit = 2,
        }
        
        [SerializeField] private Animator animator;

        public void UpdateView(AgentAspect agentAspect, float duration = 0.05f)
        {
            transform.DOKill();
            transform.DOMove(agentAspect.PhysBodyAspect.Position.Value.ToV3XY(), duration);
        }

        public async UniTask RunAnimation(Animation animation)
        {
            animator.SetSingleTrigger(animation.ToString());
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f)); 
        }
        
        public void Dispose()
        {
            transform.DOKill();
            transform.DOScale(0, 0.05f).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
    }
}