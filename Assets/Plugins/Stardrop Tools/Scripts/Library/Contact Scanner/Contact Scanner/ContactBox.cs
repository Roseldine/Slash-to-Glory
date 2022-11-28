using System.Collections;
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Collider like Class, that checks and invokes events based on contact with filtered colliders
    /// </summary>
    public class ContactBox : ContactScanner
    {
        public Vector3 scale = new Vector3(.4f, .05f, .4f);

        public override bool ContactScan()
        {
            bool contact = Physics.CheckBox(Position, scale, Rotation, contactLayers);
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
                Utilities.DrawCube(Position, scale, Rotation);
            }
        }
#endif
    }
}