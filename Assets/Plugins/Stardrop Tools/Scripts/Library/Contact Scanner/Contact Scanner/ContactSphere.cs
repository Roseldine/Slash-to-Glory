
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Checks & broadcasts events based on contact with filtered colliders
    /// </summary>
    public class ContactSphere : ContactScanner
    {
        public float radius = .4f;

        public override bool ContactScan()
        {
            bool contact = Physics.CheckSphere(Position, radius, contactLayers);
            return ContactCheck(contact);
        }

#if UNITY_EDITOR
        [Header("Render")]
        [SerializeField] Color color = Color.red;
        [SerializeField] bool drawGizmos = true;

        private void OnDrawGizmos()
        {
            if (drawGizmos)
            {
                Gizmos.color = color;
                Gizmos.DrawWireSphere(Position, radius);
            }
        }
#endif
    }
}