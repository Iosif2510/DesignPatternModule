using UnityEngine;

namespace _2510.DesignPatternModule.Singleton
{
    /// <summary>
    /// Abstract class for a singleton that extends MonoBehaviour.
    /// This singleton will be destroyed when the scene changes.
    /// To use, inherit from this class and implement the Init method.
    /// The singleton instance can be accessed via the static Instance property.
    /// <example>
    /// <code>
    /// public class SceneManager : SceneSingleton&lt;SceneManager&gt;
    /// </code>
    /// </example>
    /// </summary>
    /// <typeparam name="T">Type of derived singleton class</typeparam>
    public abstract class SceneSingleton<T> : MonoBehaviour where T : SceneSingleton<T>
    {
        private static bool _isShuttingDown = false;
        
        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if (!_isShuttingDown && _instance == null) InitSingleton();
                return _instance;
            }
        }
        
        private static void InitSingleton()
        {
            if (_instance != null) return;
            if (_isShuttingDown) return;
            
#if UNITY_6000_0_OR_NEWER
            _instance = FindAnyObjectByType<T>();
#else
            instance = FindObjectOfType<T>();
#endif
            if (_instance != null)
            {
                _instance.Init();
                return;
            }

            var managerObject = GameObject.Find(typeof(T).Name);
            if (managerObject == null)
            {
                managerObject = new GameObject
                {
                    name = typeof(T).Name
                };
            }

            _instance = managerObject.AddComponent<T>();
            _instance.Init();
        }

        /// <summary>
        /// Check if the singleton instance is itself. Do not override or reimplement.
        /// </summary>
        private void Awake()
        {
            if (Instance == this) return;
            if (Instance.gameObject != gameObject) Destroy(gameObject);
            else Destroy(this);
        }

        private void OnDestroy()
        {
            _isShuttingDown = true;
        }

        /// <summary>
        /// Implement to initialize derived singleton class.
        /// </summary>
        protected abstract void Init();
    }
}