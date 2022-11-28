using System.Collections;
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// BaseComponent class with the IManager interface. Still use Initialize() and LateInitialze() for class setup
    /// <para>Note : NOT a singleton!</para>
    /// </summary>
    public abstract class BaseManager : BaseComponent, IManager
    {
        public void InitializeManager() => Initialize();

        public void LateInitializeManager() => LateInitialize();

        public override void Initialize()
        {
            base.Initialize();
            EventFlow();
        }

        /// <summary>
        /// Subscribe to events on this method
        /// </summary>
        protected abstract void EventFlow();
    }
}