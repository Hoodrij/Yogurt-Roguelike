using System.Threading.Tasks;
using UnityEngine;

namespace Roguelike.Tools
{
    public class ResourcesLoader : IAssetLoader
    {
        public static IAssetLoader Instance { get; } = new ResourcesLoader();
        
        public async Task<Object> Load(string path)
        {
            ResourceRequest resourceRequest = Resources.LoadAsync(path);
            while (!resourceRequest.isDone)
            {
                await Task.Yield();
            }
            return resourceRequest.asset;
        }

        public async Task<TComponent> Load<TComponent>(string path) where TComponent : Component
        {
            GameObject loaded = (GameObject) await Load(path);
            return loaded.GetComponent<TComponent>();
        }
    }
}