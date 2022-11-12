using System.Threading.Tasks;
using UnityEngine;

namespace Tools
{
    public interface IAssetLoader
    {
        Task<Object> Load(string path);
        Task<TComponent> Load<TComponent>(string path) where TComponent : Component;
    }
}