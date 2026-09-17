using UnityEngine;

namespace Game.Scripts.Core
{
    public static class PhysicsUtils
    {
        public static float GetJumpHeight(float gravityScale, float jumpStrength)
        {
            var gravity = Mathf.Abs(Physics2D.gravity.y);
            var effectiveGravity = gravity * gravityScale;
            var height = jumpStrength * jumpStrength / (2.0f * effectiveGravity);
            return height;
        }
    }
}