
namespace StardropTools.Pool
{
    public class PoolableBaseObject : BaseObject, IPoolable<PoolableBaseObject>
    {
        protected PoolItem<PoolableBaseObject> poolItem;

        public void SetPoolItem(PoolItem<PoolableBaseObject> poolItem) => this.poolItem = poolItem;

        public virtual void Despawn() => poolItem.Despawn();

        public virtual void OnDespawn()
        {

        }

        public virtual void OnSpawn()
        {

        }
    }
}