using DS.Role.Interface;
using UnityEngine;

namespace DS.Role.Riko
{
    public class RikoManager : MonoBehaviour
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

        public void TryGetHurt()
        {

            if (_actorController.GetCurrentState() == ProjectConstant.PlayerState.DEFENSE)
                _actorController.IssueTrigger(ProjectConstant.AnimatorParameter.BLOACKED);
            else if (_stateManager.AddHP(-5))
                _actorController.IssueTrigger(ProjectConstant.AnimatorParameter.HIT);
            else
            {
                _actorController.IssueTrigger(ProjectConstant.AnimatorParameter.DIE);
                _battleManager.Enable(false);
            }

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
