using System;
using Core.Tools.ExtensionMethods;
using FlowJob;
using Roguelike.Entities;
using UnityEngine;

namespace Entities.Environment
{
    public class TileView : MonoBehaviour, IComponent, IDisposable
    {
        public void UpdateView(Position position)
        {
            transform.position = position.Coord.ToV3XY();
        }

        public void Dispose()
        {
            gameObject.Destroy();
        }
    }
}