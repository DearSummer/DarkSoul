using DS.Role.Riko;
using UnityEngine;

namespace DS.Role.Interface
{
    public class IActorManager : MonoBehaviour
    {
        protected RikoManager RikoManager;

        public void InitManager(RikoManager manager)
        {
            RikoManager = manager;
        }
    }
}
