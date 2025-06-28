using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace _2510.DesignPatternModule.ObjectPool
{
    public class MultipleObjectPool : MonoBehaviour
    {
        [Serializable]
        public class PoolSetting
        {
            public GameObject prefab;
            public int initialCount = 8;
            public bool isLimited;
            public int maxCount;
            public UnityEvent<IPooled> onRelease;
        }
        
        [SerializeField] private List<PoolSetting> poolSettings;
        
        private Dictionary<GameObject, ObjectPool> _pools;

        private void Awake()
        {
            InitializePools();
        }

        private void InitializePools()
        {
            _pools = new Dictionary<GameObject, ObjectPool>();
            foreach (var setting in poolSettings)
            {
                var prefab = setting.prefab;
                if (prefab.GetComponent<IPooled>() == null || _pools.ContainsKey(prefab)) continue;
                var pool = new ObjectPool(prefab, setting.initialCount, setting.isLimited, setting.maxCount);
                pool.OnRelease += setting.onRelease.Invoke;
                _pools.Add(prefab, pool);
            }
        }

        public IPooled Release(GameObject prefab)
        {
            return _pools[prefab].Release();
        }

        public void SetInstancePosition(IPooled instance)
        {
            instance.transform.position = transform.position;
        }
    }
}