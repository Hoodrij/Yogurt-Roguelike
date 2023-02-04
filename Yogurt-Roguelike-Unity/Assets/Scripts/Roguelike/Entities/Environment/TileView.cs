using System;
using Core.Tools.ExtensionMethods;
using UnityEngine;

namespace Yogurt.Roguelike
{
    public class TileView : MonoBehaviour, IComponent, IDisposable
    {
        [SerializeField] private SpriteRenderer renderer;
        
        public void SetPosition(Vector2Int position)
        {
            transform.position = position.ToV3XY();
        }

        public void SetView(Sprite sprite)
        {
            renderer.sprite = sprite;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}