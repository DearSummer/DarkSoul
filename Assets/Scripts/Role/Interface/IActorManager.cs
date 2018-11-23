using UnityEngine;

namespace DS.Role.Interface
{
    public class IActorManager : MonoBehaviour {

        protected WeaponManager weaponManager;
        protected BattleManager battleManager;
        protected StateManager stateManager;
        protected ActorController actorController;

        private float height;

        [Range(0,180)]
        public float attackAngle = 45f;
        public bool isPlayer;

        // Use this for initialization
        void Awake()
        {
            actorController = GetComponent<ActorController>();
            height = GetComponent<CapsuleCollider>().height;

            GameObject hitSenor = transform.Find("HitSenor").gameObject;
            battleManager = BindManager<BattleManager>(hitSenor);
            weaponManager = BindManager<WeaponManager>(actorController.player);
            stateManager = BindManager<StateManager>(this.gameObject);

            isPlayer = this.transform.tag.Equals(ProjectConstant.Tag.PLAYER);
        }

        public float GetCharacterHeight()
        {
            return height;
        }

        public string GetCurrentStateName()
        {
            return actorController.GetCurrentState();
        }

        public Vector3 GetForwardVec()
        {
            return actorController.player.transform.forward;
        }


        public virtual void TryGetHurt(WeaponManager wm, bool counterbackEnable,Vector3 pos)
        {
        }

        public virtual void Stuned()
        {
            actorController.IssueTrigger(ProjectConstant.AnimatorParameter.STUNED);
        }


        public void StaffStunedEnd()
        {
            actorController.IssueBool(ProjectConstant.AnimatorParameter.STAFF_STUNED, false);
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
