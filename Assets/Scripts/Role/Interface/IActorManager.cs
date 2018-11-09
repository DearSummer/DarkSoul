using DS.Role.Riko;
using UnityEngine;

namespace DS.Role.Interface
{
    public class IActorManager : MonoBehaviour
    {
        protected RikoManager actorManager;

        public void InitManager(RikoManager manager)
        {
            this.actorManager = manager;
        }

        public RikoManager GetManager()
        {
            return actorManager;
        }
    }
}
