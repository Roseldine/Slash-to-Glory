
namespace StardropTools.Pool
{
    public class PoolableBaseComponent : BaseComponent, IPoolable<PoolableBaseComponent>
    {
        protected PoolItem<PoolableBaseComponent> poolItem;

        public void SetPoolItem(PoolItem<PoolableBaseComponent> poolItem) => this.poolItem = poolItem;

        public virtual void Despawn() => poolItem.Despawn();

        public virtual void OnDespawn()
        {

        }

        public virtual void OnSpawn()
        {

        }
    }
}