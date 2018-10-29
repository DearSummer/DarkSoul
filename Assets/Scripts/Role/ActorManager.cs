using DS.Role.Interface;
using UnityEngine;

namespace DS.Role
{
    public class ActorManager : MonoBehaviour
    {
        private WeaponManager _weaponManager;
        private BattleManager _battleManager;
        private StateManager _stateManager;
        private ActorController _actorController;

        // Use this for initialization
        void Start()
        {
            _actorController = GetComponent<ActorController>();

            GameObject hitSenor = transform.Find("HitSenor").gameObject;
            _battleManager = BindManager<BattleManager>(hitSenor);
            _weaponManager = BindManager<WeaponManager>(_actorController.player);
            _stateManager = BindManager<StateManager>(this.gameObject);
        }

        public void GetHurt()
        {
            _stateManager.AddHP(-5);
            _actorController.IssueTrigger(ProjectConstant.AnimatorParameter.HIT);
        }


        private T BindManager<T>(GameObject _where) where T : IActorManager
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
