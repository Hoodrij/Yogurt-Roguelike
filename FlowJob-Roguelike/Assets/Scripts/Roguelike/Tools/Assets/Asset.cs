using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Tools
{
    public class Asset
    {
        private IAssetLoader loader => ResourcesLoader.Instance;
        private string path;

        public Asset(string path)
        {
            this.path = path;
        }

        public async UniTask<GameObject> Spawn()
        {
            Object asset = await loader.Load(path);
            return (GameObject)Object.Instantiate(asset);
        }
    }
}