using DS.Role;
using DS.Role.Interface;
using UnityEngine;

namespace DS.Util
{
    public class PostionUtil {

        public static Vector3 GetCenterPostion(Vector3 postion,CapsuleCollider collider)
        {
            return postion + Vector3.up * collider.height / 2;
        }

        public static bool IsInEnemyBack(IActorManager attacker, IActorManager receiver)
        {
            if (attacker == null || receiver == null)
                return false;

            Vector3 attackerForward = attacker.GetForwardVec();
            Vector3 receiverForward = receiver.GetForwardVec();

            Vector3 attackerPos = attacker.transform.position;
            Vector3 receiverPos = receiver.transform.position;

            Vector3 attackerDir = receiverPos - attackerPos;
            float attackerAngle = Vector3.Angle(attackerDir, attackerForward);

            Vector3 receiverDir = attackerPos - receiverPos;
            float receiverAngle = Vector3.Angle(receiverDir, receiverForward);
            float arAngle = Vector3.Angle(attackerDir, receiverDir);

            if (attackerAngle < 45f)
                return receiverAngle > 135f && Mathf.Abs(arAngle - 180) < 10f;



            return false;
        }
    }
}
