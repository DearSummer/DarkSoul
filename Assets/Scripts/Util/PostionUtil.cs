using UnityEngine;

namespace DS.Util
{
    public class PostionUtil {

        public static Vector3 GetCenterPostion(Vector3 postion,CapsuleCollider collider)
        {
            return postion + Vector3.up * collider.height / 2;
        }
    }
}
