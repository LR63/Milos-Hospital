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
            Vector2 targetSpeed = _move * moveSpeed;
            Vector2 diff = targetSpeed - _rigidbody2D.linearVelocity;
            _rigidbody2D.AddForce(Vector2.one * diff, ForceMode2D.Impulse);
        }
    }
}
