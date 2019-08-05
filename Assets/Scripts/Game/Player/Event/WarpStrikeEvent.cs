using System.ComponentModel.Design;
using DG.Tweening;
using DS.Game.DamageSystem;
using DS.Game.Effect;
using DS.Game.Weapon;
using UnityEngine;

namespace DS.Game.Player.Event
{
    public class WarpStrikeEvent : MonoBehaviour
    {

        public Transform target;
        public Transform weapon;

        public float wrapDuration = 0.3f;
        

        public void WrapStrike()
        {
            GameObject clone = Instantiate(gameObject, transform.position, transform.rotation);
            Destroy(clone.GetComponent<KeyBoardInput>());
            Destroy(clone.GetComponent<PlayerController>());
            Destroy(clone.GetComponent<Damageable>());
            Destroy(clone.GetComponent<EffectEvent>());
            Destroy(clone.GetComponent<Animator>());
            Destroy(clone.GetComponent<Rigidbody>());
            Destroy(clone.GetComponent<CapsuleCollider>());
            Destroy(clone.GetComponentInChildren<MeleeWeapon>());
            Destroy(clone.GetComponent<WarpStrikeEvent>());
            Destroy(clone.GetComponentInChildren<ParticleSystem>());

            ShowBody(false);

            transform.DOMove(target.position, wrapDuration)
                .SetEase(Ease.InExpo)
                .onComplete = OnWrapFinish;

//            weapon.parent = null;
//            weapon.DOMove(target.position, wrapDuration / 2);
//            weapon.DORotate(new Vector3(0, 90, 0), .3f);
        }

        public void OnWrapFinish()
        {
            ShowBody(true);
        }

        private void ShowBody(bool isShow)
        {
            SkinnedMeshRenderer[] smr = GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var skinnedMeshRenderer in smr)
            {
                skinnedMeshRenderer.enabled = isShow;
            }
        }
    }
}
