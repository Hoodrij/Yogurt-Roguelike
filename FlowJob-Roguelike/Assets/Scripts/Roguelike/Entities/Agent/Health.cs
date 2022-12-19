using Core.Tools;
using FlowJob;
using UnityEditor.VersionControl;

namespace Roguelike
{
    public class Health : IComponent
    {
        public int Value;
        public Job<Void, Entity> OnHealthChangedJob; 
    }
}