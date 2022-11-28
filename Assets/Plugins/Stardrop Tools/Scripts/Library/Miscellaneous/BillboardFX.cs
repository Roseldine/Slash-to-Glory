using UnityEngine;

namespace StardropTools
{
    public class BillboardFX : BaseComponent
    {
        public Transform camTransform;
        Quaternion originalRotation;

        protected override void Awake()
        {
            base.Awake();

            if (camTransform == null)
                camTransform = Camera.main.transform;
            originalRotation = transform.rotation;
        }

        public override void HandleUpdate()
        {
            base.HandleUpdate();
            transform.rotation = camTransform.rotation * originalRotation;
        }
    }
}
