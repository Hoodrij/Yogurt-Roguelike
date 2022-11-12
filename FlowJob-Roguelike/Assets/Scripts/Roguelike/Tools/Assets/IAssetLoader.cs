using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Tools
{
    public interface IAssetLoader
    {
        UniTask<Object> Load(string path);
    }
}