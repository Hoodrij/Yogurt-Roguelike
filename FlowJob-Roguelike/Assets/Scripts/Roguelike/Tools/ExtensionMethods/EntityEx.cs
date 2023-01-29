using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using FlowJob;
using UnityEngine;

namespace Core.Tools.ExtensionMethods
{
    public static class EntityEx
    {
        public static Entity AddForLife<TComponent>(this Entity entity, TComponent component) where TComponent : IComponent, IDisposable
        {
            entity.Add(component);
            entity.WaitForDeadAndRemoveComponent(component);
            return entity;
        }

        private static async void WaitForDeadAndRemoveComponent<TComponent>(this Entity entity, TComponent component) where TComponent : IComponent, IDisposable
        {
            await UniTask.WaitWhile(() => !Application.isPlaying || entity.Exist);

            if (Application.isPlaying)
            {
                component.Dispose();
            }
        }
    }
}