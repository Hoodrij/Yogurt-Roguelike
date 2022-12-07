using System;
using Core.Tools.ExtensionMethods;
using DG.Tweening;
using FlowJob;
using UnityEngine;

namespace Entities.Environment
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
            gameObject.Destroy();
        }
    }
}