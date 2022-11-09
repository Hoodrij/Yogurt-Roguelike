using UnityEngine;

namespace Core.Tools.ExtensionMethods
{
    public static class MonoBehaviourEx
    {
        public static T GetComponentInParent<T>(this Component beh, bool recursive) where T : Component
        {
            T result = null;
            while (true)
            {
                result = beh.GetComponentInParent<T>();
                if (result == null && beh.transform.parent != null && recursive)
                {
                    beh = beh.transform.parent;
                    continue;
                }

                break;
            }

            return result;
        }
    }
}