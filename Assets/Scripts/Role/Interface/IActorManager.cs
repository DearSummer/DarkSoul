using UnityEngine;

namespace DS.Role.Interface
{
    public class IActorManager : MonoBehaviour {

        protected WeaponManager weaponManager;
        protected BattleManager battleManager;
        protected StateManager stateManager;
        protected ActorController actorController;

        // Use this for initialization
        void Start()
        {
            actorController = GetComponent<ActorController>();

            GameObject hitSenor = transform.Find("HitSenor").gameObject;
            battleManager = BindManager<BattleManager>(hitSenor);
            weaponManager = BindManager<WeaponManager>(actorController.player);
            stateManager = BindManager<StateManager>(this.gameObject);
        }

        public string GetCurrentStateName()
        {
            return actorController.GetCurrentState();
        }

        public Vector3 GetForwardVec()
        {
            return actorController.player.transform.forward;
        }


        public virtual void TryGetHurt(WeaponManager wm, bool counterbackEnable)
        {
        }

        public virtual void Stuned()
        {
            actorController.IssueTrigger(ProjectConstant.AnimatorParameter.STUNED);
        }

        protected T BindManager<T>(GameObject _where) where T : IActorStateManager
        {
            T manager = _where.GetComponent<T>();
            if (manager == null)
            {
                manager = _where.AddComponent<T>();
            }
            manager.InitManager(this);
            return manager;
        }
    }
}
