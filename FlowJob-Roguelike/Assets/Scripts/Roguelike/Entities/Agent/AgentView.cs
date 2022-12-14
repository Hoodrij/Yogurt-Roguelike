using System;
using Core.Tools.ExtensionMethods;
using DG.Tweening;
using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
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

        public void RunAnimation(Animation animation)
        {
            animator.SetSingleTrigger(animation.ToString());
        }
        
        public void Dispose()
        {
            transform.DOKill();
            transform.DOScale(0, 0.05f).OnComplete(() =>
            {
                gameObject.Destroy();
            });
        }
    }
}