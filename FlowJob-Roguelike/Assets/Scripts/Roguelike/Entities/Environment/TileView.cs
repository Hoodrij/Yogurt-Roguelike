using System;
using Core.Tools.ExtensionMethods;
using FlowJob;
using UnityEngine;

namespace Roguelike
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