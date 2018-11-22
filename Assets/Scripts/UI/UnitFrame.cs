using UnityEngine;
using UnityEngine.UI;

namespace DS.UI
{
    public class UnitFrame : MonoBehaviour
    {
        public SliderDisappearDelay primary;
        public SliderDisappearDelay secondary;

        public void SetPrimary(float value)
        {
            primary.SetValue(value);
        }

        public void SetSecondary(float value)
        {
            secondary.SetValue(value);
        }

    }
}
