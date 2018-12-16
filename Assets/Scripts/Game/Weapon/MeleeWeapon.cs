using System;
using System.Collections.Generic;
using DS.Game.DamageSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DS.Game.Weapon
{
    public class MeleeWeapon : MonoBehaviour
    {

        public int damage;

        [Serializable]
        public class AttackPoint
        {
            public float radius;
            public Vector3 offset;
            public Transform attackRoot;

        }

        public LayerMask targetLayers;
        public AttackPoint[] attackPointArray = new AttackPoint[0];

        private Vector3[] previousAttackPos = new Vector3[0];
    
        private bool isInAttack = false;
        private GameObject mastar;

        private readonly HashSet<GameObject> hasAttackedSet = new HashSet<GameObject>();
        
        private static readonly RaycastHit[] raycastHitCache = new RaycastHit[32];

        public void SetMaster(GameObject master)
        {
            this.mastar = master;

        }

        public void StartAttack()
        {
            isInAttack = true;
            previousAttackPos = new Vector3[attackPointArray.Length];
            hasAttackedSet.Clear();

            for (int i = 0; i < previousAttackPos.Length; i++)
            {
                previousAttackPos[i] = GetAttackPointWorldPos(attackPointArray[i]);
            }

        }

        public void EndAttack()
        {
            isInAttack = false;
        }


        private void FixedUpdate()
        {
            if (isInAttack)
            {
                for (int i = 0; i < attackPointArray.Length; i++)
                {
                    AttackPoint ap = attackPointArray[i];

                    Vector3 worldPos = GetAttackPointWorldPos(ap);
                    Vector3 attackVec = worldPos - previousAttackPos[i];


                    if (attackVec.magnitude < 0.001f)
                    {
                        attackVec = Vector3.forward * 0.0001f;
                    }

                    Ray r = new Ray(worldPos,attackVec.normalized);
                    int contacts = Physics.SphereCastNonAlloc(r, ap.radius, raycastHitCache, attackVec.magnitude, ~0,
                        QueryTriggerInteraction.Ignore);
            
                    for (int j = 0; j < contacts; j++)
                    {
                        Collider col = raycastHitCache[j].collider;
                        if (col != null)
                            CheckDamage(col, attackVec,worldPos);
                    }

                    //update new postion
                    previousAttackPos[i] = worldPos;
                }
  
            }
        }

        private void CheckDamage(Collider col, Vector3 attackDir,Vector3 worldPos)
        {

            if (hasAttackedSet.Contains(col.gameObject))
                return;

            Damageable damageable = col.GetComponent<Damageable>();
            if (damageable == null)
                return;

            if (damageable.gameObject == mastar)
                return;

            if ((targetLayers.value & (1 << col.gameObject.layer)) == 0)
                return;

            DamageData data;

            data.damage = Random.Range(-2, 3) + damage;
            data.attacker = mastar;
            data.direction = attackDir;
            data.damagePos = col.ClosestPointOnBounds(worldPos);

            damageable.TryGetDamage(data);

            hasAttackedSet.Add(col.gameObject);
        }


        private Vector3 GetAttackPointWorldPos(AttackPoint ap)
        {
            if (ap.attackRoot == null)
                return Vector3.zero;
            return ap.attackRoot.position + ap.attackRoot.transform.TransformVector(ap.offset);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            for (int i = 0; i < attackPointArray.Length; i++)
            {
                AttackPoint ap = attackPointArray[i];

                if (ap.attackRoot != null)
                {
                    Vector3 worldPos = ap.attackRoot.TransformVector(ap.offset);
                    Color green = Color.green;
                    green.a = 0.7f;
                    Gizmos.color = green;
                    Gizmos.DrawWireSphere(worldPos + ap.attackRoot.position, ap.radius);
                }
            }
        }
#endif
    }


}

