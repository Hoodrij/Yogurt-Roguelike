using System;
using FlowJob;
using UnityEngine;

namespace Roguelike
{
    [Serializable]
    public class RockData : IComponent
    {
        public Sprite[] Sprites;

        public Sprite GetSprite(Health health)
        {
            int index = Mathf.Clamp(health.Value - 1, 0, Sprites.Length - 1);
            return Sprites[index];
        }
    }
}