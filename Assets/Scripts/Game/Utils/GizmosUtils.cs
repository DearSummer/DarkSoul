using UnityEngine;

namespace DS.Game.Utils
{
    public class GizmosUtils  {

        public static void DrawCircle(Vector3 center, float radius,Color color)
        {
            Matrix4x4 defaultMaterix = Gizmos.matrix;


            Gizmos.color = color;

            Vector3 startPoint = Vector3.zero;
            Vector3 firstPoint = Vector3.zero;

            bool isFristTime = true;
            for (float i = 0; i < 2 * Mathf.PI; i += 0.1f)
            {
                float x = radius * Mathf.Cos(i);
                float z = radius * Mathf.Sin(i);

                Vector3 p = new Vector3(center.x + x, center.y, center.z + z);
                if (isFristTime)
                {
                    firstPoint = p;
                    isFristTime = false;
                }
                else
                {
                    Gizmos.DrawLine(startPoint, p);
                }

                startPoint = p;
            }

            Gizmos.DrawLine(startPoint, firstPoint);
        }
    }
}
