using UnityEngine;

namespace _2510.DesignPatternModule.ObjectPool
{
    public interface IPooled
    {
        public ObjectPool Pool { get; }
        
        // Properties to access the GameObject and Transform of the pooled object
        // Implement on MonoBehaviour-derived classes to omit explicit definitions
        public GameObject gameObject { get; }
        public Transform transform { get; }

        public void Initialize(ObjectPool pool);
        public void Activate();
        public void Return();
    }
}