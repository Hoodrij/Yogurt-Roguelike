using FlowJob;
using UnityEngine;
using Event = Core.Tools.Observables.Event;

namespace Roguelike.Entities
{
    public class Life : IComponent
    {
        public readonly Event UpdateEvent = new Event();
        public readonly Event LateUpdateEvent = new Event();
        public readonly Event FixedUpdateEvent = new Event();
        public readonly Event PauseEvent = new Event();
        public readonly Event ResumeEvent = new Event();
        public readonly Event QuitEvent = new Event();

        public Life()
        {
            LifeBehaviour behaviour = new GameObject("Life").AddComponent<LifeBehaviour>();
            behaviour.gameObject.hideFlags |= HideFlags.HideAndDontSave;
            Object.DontDestroyOnLoad(behaviour);
            behaviour.Life = this;
        }
        
        private class LifeBehaviour : MonoBehaviour
        {
            public Life Life { get; set; }

            private void Update()
            {
                Life?.UpdateEvent.Fire();
            }

            private void LateUpdate()
            {
                Life?.LateUpdateEvent.Fire();
            }

            private void FixedUpdate()
            {
                Life?.FixedUpdateEvent.Fire();
            }

            private void OnApplicationFocus(bool focus)
            {
                if (focus)
                    Life?.ResumeEvent.Fire();
                else
                    Life?.PauseEvent.Fire();
            }

            private void OnApplicationPause(bool pause)
            {
                if (pause)
                    Life?.PauseEvent.Fire();
                else
                    Life?.ResumeEvent.Fire();
            }

            private void OnApplicationQuit()
            {
                Life?.QuitEvent.Fire();
            }
        }
    }
}