using UnityEngine;

namespace DS.Game.DamageSystem
{
    public struct DamageData
    {
        public int damage;
        public MonoBehaviour attacker;
        public Vector3 direction;
        public Vector3 damagePos;
    }

}
