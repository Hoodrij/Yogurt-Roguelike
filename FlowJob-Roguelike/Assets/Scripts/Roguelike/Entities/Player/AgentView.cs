using Core.Tools.ExtensionMethods;
using DG.Tweening;
using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class AgentView : MonoBehaviour, IComponent
    {
        public void UpdateView(AgentAspect agentAspect)
        {
            transform.DOKill();
            transform.DOMove(agentAspect.PhysBodyAspect.Position.Coord.ToV3XY(), 0.05f);
        }
    }
}