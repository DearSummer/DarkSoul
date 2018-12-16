using UnityEngine;

namespace DS.Game.Core.Sender
{
    [RequireComponent(typeof(Collider))]
    public class OnTriggerEnterCommand : GameCommandSender
    {
        public LayerMask targetLayer;

        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((targetLayer.value & ( 1 << other.gameObject.layer)) == 0)
                return;

            Send();
        }
    }
}
