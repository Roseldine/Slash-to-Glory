
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Checks and invokes events based on contact with filtered colliders
    /// </summary>
    public abstract class ContactScanner : BaseObject
    {
        public LayerMask contactLayers;
        [SerializeField] protected bool hasContact;
        [SerializeField] protected bool debug;

        public bool HasContact { get => hasContact; }

        public readonly GameEvent OnContactStart = new GameEvent();
        public readonly GameEvent InContact = new GameEvent();
        public readonly GameEvent OnContactEnd = new GameEvent();

        /// <summary>
        /// Run this to check for contact. Update per frame for constant check
        /// </summary>
        public abstract bool ContactScan();

        /// <summary>
        /// Checks & broadcasts events based on contact
        /// </summary>
        protected bool ContactCheck(bool physicsContactBoolean)
        {
            if (hasContact != physicsContactBoolean)
            {
                // contact start
                if (physicsContactBoolean && hasContact == false)
                {
                    OnContactStart?.Invoke();
                    if (debug) Utilities.LogDebug("Contact START on: " + name);
                }

                // contact end
                if (physicsContactBoolean == false && hasContact)
                {
                    OnContactEnd?.Invoke();
                    if (debug) Debug.Log(Utilities.DebugAlert + "Contact END on: " + name);
                }

                hasContact = physicsContactBoolean;
            }

            // is in contact
            else
                InContact?.Invoke();

            return physicsContactBoolean;
        }
    }
}