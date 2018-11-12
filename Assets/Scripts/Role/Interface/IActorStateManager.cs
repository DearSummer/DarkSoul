using UnityEngine;

namespace DS.Role.Interface
{
    public class IActorStateManager : MonoBehaviour
    {
        protected IActorManager actorManager;

        public void InitManager(IActorManager manager)
        {
            this.actorManager = manager;
        }

        public IActorManager GetActorManager()
        {
            return actorManager;
        }
    }
}
