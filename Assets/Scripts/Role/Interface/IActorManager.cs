using UnityEngine;

namespace DS.Role.Interface
{
    public class IActorManager : MonoBehaviour
    {
        protected ActorManager actorManager;

        public void InitManager(ActorManager manager)
        {
            actorManager = manager;
        }
    }
}
