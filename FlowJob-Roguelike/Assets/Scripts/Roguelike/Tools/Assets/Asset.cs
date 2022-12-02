using System.Threading.Tasks;
using UnityEngine;

namespace Tools
{
    public class Asset
    {
        protected IAssetLoader loader => ResourcesLoader.Instance;
        protected string path;

        public Asset(string path)
        {
            this.path = path;
        }

        public async Task<GameObject> Spawn()
        {
            Object asset = await loader.Load(path);
            return (GameObject)Object.Instantiate(asset);
        }
    }

    public class Asset<TComponent> : Asset where TComponent : Component
    {
        public Asset(string path) : base(path) { }

        public async Task<TComponent> Spawn()
        {
            TComponent prefab = await loader.Load<TComponent>(path);
            TComponent result = Object.Instantiate(prefab);
            return result;
        } 
    }
    
    public class SO<Tso> : Asset where Tso : ScriptableObject
    {
        public SO(string path) : base(path) { }

        public async Task<Tso> Load()
        {
            return await loader.Load(path) as Tso;
        } 
    }
}