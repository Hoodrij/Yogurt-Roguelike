using System;
using UnityEngine;
using UnityEngine.UI;

namespace Yogurt.Roguelike
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
            Destroy(gameObject);
        }
    }
}