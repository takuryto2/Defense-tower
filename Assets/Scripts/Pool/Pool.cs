using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace TP {
    public class Pool<T> where T : class, IPooledObject<T> {
        private readonly Func<T> _createFunc;
        private readonly Action<T> _onGetFunc;
        private readonly Action<T> _onReleaseFunc;
        private readonly Stack<T> _pooledObjects;

        public Pool(Func<T> createFunc, int capacity = 50, int preAllocationCount = 0)
            : this(createFunc, null, null, capacity, preAllocationCount) {}

        public Pool(Func<T> createFunc, Action<T> onGetFunc, Action<T> onReleaseFunc, int capacity = 50,
            int preAllocationCount = 0) {
            Assert.IsNotNull(createFunc, "The object creation function can't be null.");
            Assert.IsTrue(capacity >= 1, "The capacity of the pool must be greater than or equal to 1.");
            Assert.IsTrue(preAllocationCount >= 0,
                "The pre-allocation count of the pool must be greater than or equal to 0.");

            _pooledObjects = new Stack<T>(capacity);
            _createFunc = createFunc;
            _onGetFunc = onGetFunc;
            _onReleaseFunc = onReleaseFunc;
            PreAllocatePooledObjects(preAllocationCount);
        }

        private void PreAllocatePooledObjects(int preAllocationCount) {
            for (int i = 0; i < preAllocationCount; i++) {
                T pooledObject = CreatePoolObject();
                Release(pooledObject);
            }
        }

        public T Get() {
            T pooledObject;
            if (_pooledObjects.Count > 0) {
                pooledObject = _pooledObjects.Pop();
            } else {
                pooledObject = CreatePoolObject();
            }

            _onGetFunc?.Invoke(pooledObject);
            return pooledObject;
        }

        private T CreatePoolObject() {
            T pooledObject = _createFunc.Invoke();
            Assert.IsNotNull(pooledObject, "The object to create can't be null.");
            pooledObject.SetReleaseFunc(Release);
            return pooledObject;
        }

        public void Release(T pooledObject) {
            Assert.IsNotNull(pooledObject, "The object to release can't be null.");
            _pooledObjects.Push(pooledObject);
            _onReleaseFunc?.Invoke(pooledObject);
        }
    }
}