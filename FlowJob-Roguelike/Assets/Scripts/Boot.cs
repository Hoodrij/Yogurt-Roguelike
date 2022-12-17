using UnityEngine;

namespace Roguelike
{
    public class Boot : MonoBehaviour
    {
        private async void Awake()
        {
            await new RunGameJob().Run();
        }
    }
}