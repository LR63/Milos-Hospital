using UnityEngine;
using UnityEngine.InputSystem;

namespace Script
{
    public class Movement : MonoBehaviour
    {
        [Range(0f, 10f)] public float moveSpeed = 7f;
    
        public InputAction moveAction;

        private Rigidbody2D _rigidbody2D;

        private Vector2 _move;

        // private SpriteRenderer _spriteRenderer;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            moveAction = InputSystem.actions.FindAction("Move");
            if (moveAction == null)
            {
                Debug.Log("No action found");
            }
            //_spriteRenderer = gameObject.transform.Find("Sprites").GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        private void Update()
        {
            _move = moveAction.ReadValue<Vector2>();
            HandleMovement();
        }
    
        private void HandleMovement()
        {
            float targetSpeedHorizontal = _move[0] * moveSpeed;
            float diffHorizontal = targetSpeedHorizontal - _rigidbody2D.linearVelocity.x;
            _rigidbody2D.AddForce(Vector2.right * diffHorizontal, ForceMode2D.Impulse);
            
            float targetSpeedVertical = _move[1] * moveSpeed;
            float diffVertical = targetSpeedVertical - _rigidbody2D.linearVelocity.y;
            _rigidbody2D.AddForce(Vector2.up * diffVertical, ForceMode2D.Impulse);
        }
    }
}
