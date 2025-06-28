using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace _2510.DesignPatternModule.ObjectPool
{
    public class ObjectPool
    {
        private GameObject _pooledPrefab;
        private Stack<IPooled> _pool;
        private bool _limitMaxSize = false;
        private int _maxInstanceCount = 0;
        private int _currentInstanceCount = 0;

        public UnityAction<IPooled> OnRelease;

        public ObjectPool(GameObject prefab, int initialSize, bool limitMaxSize, int maxSize)
        {
            _pooledPrefab = prefab;
            _limitMaxSize = limitMaxSize;
            if (limitMaxSize)
            {
                initialSize = (initialSize > maxSize) ? maxSize : initialSize;
            }
            _currentInstanceCount = initialSize;
            InitializePool(initialSize);
        }
        
        public ObjectPool(GameObject prefab, int initialSize) : this(prefab, initialSize, false, 0)
        {
        }

        private void InitializePool(int size)
        {
            var pooled = _pooledPrefab.GetComponent<IPooled>();
            if (pooled == null) return;
            _pool = new Stack<IPooled>(size);
        
            for (var i = 0; i < size; i++)
            {
                var pooledInstance = Object.Instantiate(_pooledPrefab).GetComponent<IPooled>();
                pooledInstance.Initialize(this);
                _pool.Push(pooledInstance);
            }
        }

        /// <summary>
        /// Release an instance from the pool.
        /// </summary>
        /// <returns>Released IPooled instance.</returns>
        public IPooled Release()
        {
            IPooled pooledInstance;
            if (_pool == null || _pool.Count == 0)
            {
                if (_limitMaxSize && _currentInstanceCount >= _maxInstanceCount) return null;
                pooledInstance = Object.Instantiate(_pooledPrefab).GetComponent<IPooled>();
                pooledInstance.Initialize(this);
                _currentInstanceCount++;
            }
            else pooledInstance = _pool.Pop();
        
            pooledInstance.Activate();
            OnRelease?.Invoke(pooledInstance);
            return pooledInstance;
        }

        /// <summary>
        /// Called from IPooled instance when it is no longer needed.
        /// </summary>
        /// <param name="pooledInstance"></param>
        public void Return(IPooled pooledInstance)
        {
            _pool.Push(pooledInstance);
        }
    }
}
