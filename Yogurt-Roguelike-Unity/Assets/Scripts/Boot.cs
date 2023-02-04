using UnityEngine;

namespace Yogurt.Roguelike
{
    public class Boot : MonoBehaviour
    {
        private void Awake()
        {
            new RunGameJob().Run();
        }
    }
}