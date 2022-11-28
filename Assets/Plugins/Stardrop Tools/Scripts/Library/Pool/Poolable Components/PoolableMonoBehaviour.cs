
using UnityEngine;

namespace StardropTools.Pool
{
    public class PoolableMonoBehaviour : MonoBehaviour, IPoolable<PoolableMonoBehaviour>
    {
        protected PoolItem<PoolableMonoBehaviour> poolItem;

        public void SetPoolItem(PoolItem<PoolableMonoBehaviour> poolItem) => this.poolItem = poolItem;

        public virtual void Despawn() => poolItem.Despawn();

        public virtual void OnDespawn()
        {

        }

        public virtual void OnSpawn()
        {

        }
    }
}