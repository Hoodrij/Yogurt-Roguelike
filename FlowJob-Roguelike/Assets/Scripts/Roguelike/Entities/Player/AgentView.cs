﻿using Core.Tools.ExtensionMethods;
using FlowJob;
using UnityEngine;

namespace Roguelike.Entities
{
    public class AgentView : MonoBehaviour, IComponent
    {
        public void Update(AgentAspect agentAspect)
        {
            transform.position = agentAspect.Position.Coord.ToV3XY();
        }
    }
}