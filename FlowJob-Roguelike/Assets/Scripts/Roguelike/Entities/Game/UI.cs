﻿using System;
using Core.Tools.ExtensionMethods;
using FlowJob;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike
{
    public class UI : MonoBehaviour, IComponent, IDisposable
    {
        [SerializeField] private Text healthText;

        public void UpdateView(int health)
        {
            healthText.text = health.ToString();
        }

        public void Dispose()
        {
            gameObject.Destroy();
        }
    }
}