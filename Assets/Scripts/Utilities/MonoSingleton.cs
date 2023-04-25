using UnityEngine;

namespace Utilities
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public bool global = true;
        private static readonly object _lock = new object();
        private static bool IsAppQuit { get; set; }


        private static T _instance;

        protected MonoSingleton()
        {
            IsAppQuit = false;
        }
        public static T Instance
        {
            get
            {
                if(IsAppQuit && Debug.isDebugBuild)
                {
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    Debug.LogWarning("singleton"+typeof(T)+" already be destroyed when aqq quitting");
                    return null;
                }
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<T>();
                        if (_instance != null) return _instance;
                        GameObject go = new GameObject(typeof(T).Name);
                        _instance = go.AddComponent<T>();
                        go.name = "singleton"+typeof(T);
                    }
                    return _instance;
                }

            }
        }
        void Start()
        {
            if (global) DontDestroyOnLoad(this.gameObject);
            this.OnStart();
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
            else
            {
                //Destroy(this.gameObject);
            }
            OnStart();
        }

        protected virtual void OnStart()
        {

        }

    }
}

