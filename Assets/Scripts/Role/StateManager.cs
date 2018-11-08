using DS.Role.Interface;
using UnityEngine;

namespace DS.Role
{
    public class StateManager : IActorManager
    {
        public int hpMax;
        public int hp;

        private void Start()
        {
            AddHP(hpMax);
        }

        public bool AddHP(int value)
        {
            hp += value;
            hp = Mathf.Clamp(hp, 0, hpMax);

            return hp != 0;
        }
    }
}
