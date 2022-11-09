using Roguelike.Jobs;
using UnityEngine;

namespace Roguelike
{
    public class Boot : MonoBehaviour
    {
        private void Awake()
        {
            new RunGameJob().Run();
        }
    }
}