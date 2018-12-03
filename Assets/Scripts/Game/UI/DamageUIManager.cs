using DS.Game.DamageSystem;
using DS.Runtime;
using UnityEngine;

namespace DS.Game.UI
{
    public class DamageUIManager : MonoSingleton<DamageUIManager>
    {
        private GameObject canvas;
        public GameObject damageText;

        public float aliveTime;

        private void Start()
        {
            if (canvas == null)
                canvas = GameObject.FindWithTag(ProjectConstant.Tag.DAMAGE_CANVAS);
        }

        public void SetDamage(DamageData d)
        {
            SetDamage(d.damage, d.damagePos, ColorSet.normalColor);
        }

        public void SetDamage(int damage,Vector3 pos,Color color)
        {
            GameObject text = ObjectPool.Instance.Get(damageText);
            text.transform.SetParent(canvas.transform, false);

            DamageText dt = text.GetComponent<DamageText>();      
            dt.SetAliveTime(aliveTime);
            dt.color = color;
            dt.SetText(damage);

            text.transform.position = UnityEngine.Camera.main.WorldToScreenPoint(pos);


        }

    }
}
