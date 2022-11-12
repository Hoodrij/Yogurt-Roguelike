using Cysharp.Threading.Tasks;
using FlowJob;
using Tools;
using UnityEngine;

namespace Roguelike.Entities
{
    public class Assets : IComponent
    {
        public readonly Asset Player = new Asset("Player");
    }
}