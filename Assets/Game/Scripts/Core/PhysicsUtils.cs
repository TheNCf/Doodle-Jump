using UnityEngine;

namespace Game.Scripts.Core
{
    public static class PhysicsUtils
    {
        public static float GetJumpHeight(float gravityScale, float jumpStrength)
        {
            float gravity = Mathf.Abs(Physics2D.gravity.y);
            float effectiveGravity = gravity * gravityScale;
            float height = (jumpStrength * jumpStrength) / (2.0f * effectiveGravity);
            return height;
        }
    }
}