using System;
using UnityEngine;

namespace DS
{
    public class EnemyInput : PlayerInput
    {

        private GameObject player;

        private float timer = 0f;
        public float attackFrequency = 3f;

        // Use this for initialization
        void Start ()
        {
            Attack = false;
            targetUpValue = 0.0f;
            targetCRightValue = 0.0f;

            player = GameObject.FindGameObjectWithTag(ProjectConstant.Tag.PLAYER);
        }
	
        // Update is called once per frame
        protected override void Update () {

            base.Update();



            Vector3 dir = Vector3.Normalize(player.transform.position - this.transform.position);
            targetUpValue = dir.z;
            targetRightValue = dir.x;

            timer += Time.deltaTime;
        }

        public void InAttackRange()
        {
            if (timer > attackFrequency)
            {
                Attack = true;
                timer = 0;
            }
            
        }

        public void OutOfAttackRange()
        {
            Attack = false;
        }
    }
}
