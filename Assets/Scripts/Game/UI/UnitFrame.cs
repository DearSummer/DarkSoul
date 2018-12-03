using DS.Game.DamageSystem;
using UnityEngine;

namespace DS.Game.UI
{
    public class UnitFrame : MonoBehaviour
    {
        public SliderDisappearDelay primary;
        public SliderDisappearDelay secondary;

        public void SetPrimary(float value)
        {
            primary.SetValue(value);
        }

        public void SetPrimary(Damageable d)
        {
            primary.SetValue(value: (float)d.hp / d.hpMax);
        }

        public void SetSecondary(float value)
        {
            secondary.SetValue(value);
        }

    }
}
