using System;
using Core.Tools.ExtensionMethods;
using DG.Tweening;
using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class AgentView : MonoBehaviour, IComponent, IDisposable
    {
        public void UpdateView(AgentAspect agentAspect)
        {
            transform.DOKill();
            transform.DOMove(agentAspect.PhysBodyAspect.Position.Value.ToV3XY(), 0.05f);
        }
        
        public void Dispose()
        {
            gameObject.Destroy();
        }
    }
}