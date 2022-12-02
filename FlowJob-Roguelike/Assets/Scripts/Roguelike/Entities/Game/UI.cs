using FlowJob;
using UnityEngine;
using UnityEngine.UI;

namespace Roguelike.Entities
{
    public class UI : MonoBehaviour, IComponent
    {
        [SerializeField] private Text healthText;

        public void UpdateView(int health)
        {
            healthText.text = health.ToString();
        }
    }
}