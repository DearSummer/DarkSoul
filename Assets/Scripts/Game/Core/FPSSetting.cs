using UnityEngine;

namespace DS.Game.Core
{
    public class FPSSetting : MonoBehaviour
    {

        public int FPS = 60;

        private void OnEnable()
        {
            Application.targetFrameRate = FPS;
        }
    }
}
