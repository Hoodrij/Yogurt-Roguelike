using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Tools
{
    public class ResourcesLoader : IAssetLoader
    {
        public static IAssetLoader Instance { get; } = new ResourcesLoader();
        
        public async Task<Object> Load(string path)
        {
            return await Resources.LoadAsync(path);
        }

        public async Task<TComponent> Load<TComponent>(string path) where TComponent : Component
        {
            Object loaded = await Resources.LoadAsync<GameObject>(path);
            return ((GameObject) loaded).GetComponent<TComponent>();
        }
    }
}