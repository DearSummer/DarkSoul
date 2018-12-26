using UnityEngine;

namespace DS.Game.Core.Sender
{
    [RequireComponent(typeof(Collider))]
    public class OnTriggerEnterCommand : GameCommandSender
    {
        public LayerMask targetLayer;


        private bool triggerEnable = true;
        private void Awake()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        public void IsTriggerOpen(bool open)
        {
            triggerEnable = open;
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((targetLayer.value & ( 1 << other.gameObject.layer)) == 0)
                return;
            Debug.Log("in");
//            if (!triggerEnable)
//                return;

            Send(other.gameObject);
        }
    }
}
