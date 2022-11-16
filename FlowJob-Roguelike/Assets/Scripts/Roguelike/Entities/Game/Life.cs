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
                Life?.UpdateEvent.Fire(default);
            }

            private void LateUpdate()
            {
                Life?.LateUpdateEvent.Fire(default);
            }

            private void FixedUpdate()
            {
                Life?.FixedUpdateEvent.Fire(default);
            }

            private void OnApplicationFocus(bool focus)
            {
                if (focus)
                    Life?.ResumeEvent.Fire(default);
                else
                    Life?.PauseEvent.Fire(default);
            }

            private void OnApplicationPause(bool pause)
            {
                if (pause)
                    Life?.PauseEvent.Fire(default);
                else
                    Life?.ResumeEvent.Fire(default);
            }

            private void OnApplicationQuit()
            {
                Life?.QuitEvent.Fire(default);
            }
        }
    }
}