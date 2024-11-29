using System;

namespace TP {
    public interface IPooledObject<U> where U : class, IPooledObject<U> {
        //void SetPool(Pool<U> pool);

        void SetReleaseFunc(Action<U> releaseFunc);
    }
}