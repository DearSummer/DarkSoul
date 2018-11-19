using UnityEngine;

namespace DS.UI
{
    public class DamageUIManager : MonoSingleton<DamageUIManager>
    {
        private GameObject canvas;
        public GameObject damageText;

        public float aliveTime;

        private Color damageColor = ColorSet.normalColor;


        private void Start()
        {
            if (canvas == null)
                canvas = GameObject.FindWithTag(ProjectConstant.Tag.DAMAGE_CANVAS);
        }

        public void SetColor(Color color)
        {
            damageColor = color;
        }

        public void SetDamage(int damage,Vector3 pos)
        {
            GameObject text = ObjectPool.Instance.Get(damageText);
            text.transform.SetParent(canvas.transform, false);

            DamageText dt = text.GetComponent<DamageText>();
            dt.SetText(damage);
            dt.SetAliveTime(aliveTime);
            dt.color = damageColor;

            text.transform.position = UnityEngine.Camera.main.WorldToScreenPoint(pos);


        }

    }
}
