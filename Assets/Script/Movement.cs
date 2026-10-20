using UnityEngine;

namespace Script
{
    public class Movement : MonoBehaviour
    {
        public static void Move(Vector2 movement, float moveSpeed, Rigidbody2D rigidbody)
        {
            var targetSpeed = movement * moveSpeed;
            var diff = targetSpeed - rigidbody.linearVelocity;
            rigidbody.AddForce(Vector2.one * diff, ForceMode2D.Impulse);
        }
    }
}