using UnityEngine;

namespace _2510.DesignPatternModule.Singleton
{
    /// <summary>
    /// Abstract class for a universal singleton that extends MonoBehaviour.
    /// </summary>
    /// <example>
    /// <code>
    /// public class GameManager : UniversalMonoBehaviourSingleton&lt;GameManager&gt;
    /// </code>
    /// </example>
    /// <typeparam name="T">Type of derived singleton class</typeparam>
    public abstract class UniversalMonoBehaviourSingleton<T> : MonoBehaviour where T : UniversalMonoBehaviourSingleton<T>
    {
        private static readonly string GameObjectName = "@Managers";
        
        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if (_instance == null) InitSingleton();
                return _instance;
            }
        }
        
        private static void InitSingleton()
        {
            if (_instance != null) return;
            
#if UNITY_6000_0_OR_NEWER
            _instance = FindAnyObjectByType<T>();
#else
            instance = FindObjectOfType<T>();
#endif
            if (_instance != null)
            {
                if (Application.isPlaying) DontDestroyOnLoad(_instance.gameObject);
                _instance.Init();
                return;
            }

            var managerObject = GameObject.Find(GameObjectName);
            if (managerObject == null)
            {
                managerObject = new GameObject
                {
                    name = GameObjectName
                };
            }

            _instance = managerObject.AddComponent<T>();
            if (Application.isPlaying) DontDestroyOnLoad(managerObject);
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

        /// <summary>
        /// Implement to initialize derived singleton class.
        /// </summary>
        protected abstract void Init();
    }
}