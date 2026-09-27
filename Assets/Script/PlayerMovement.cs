using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Data;

namespace Script
{
    public class PlayerMovement : MonoBehaviour
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
                throw new NullReferenceException("Couldn't find InputAction");
            }
            //_spriteRenderer = gameObject.transform.Find("Sprites").GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        private void Update()
        {
            _move = moveAction.ReadValue<Vector2>();
            Movement.Move(_move, moveSpeed, _rigidbody2D);
        }
    }
}
