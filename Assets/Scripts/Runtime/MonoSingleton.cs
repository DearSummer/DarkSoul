using UnityEngine;

namespace DS.Runtime
{
    public class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        private static T mInstance;
        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = FindObjectOfType<T>();
                    if (mInstance == null)
                    {
                        GameObject obj = new GameObject("__monoSingleton");
                        mInstance = obj.AddComponent<T>();
                    }
                }

                return mInstance;
            }
        }


    }
}
