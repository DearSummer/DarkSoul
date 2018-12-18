using UnityEngine;
using UnityEngine.UI;

namespace DS.Game.UI
{
    public class UnitFrameEnemy : UnitFrame
    {
        public Text enemyName;

        private Vector3 scale = Vector3.zero;

        public void ResetValue()
        {
            SetPrimary(1f);
        }

        private void OnEnable()
        {
            if (scale == Vector3.zero)
                scale = this.transform.localScale;
            else
                this.transform.localScale = scale;
        }

        public void SetName(string n)
        {
            enemyName.text = n;
        }



    }
}
