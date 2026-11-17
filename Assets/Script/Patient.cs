using UnityEngine;
using UnityEngine.Serialization;
using static Script.PatientStates;

namespace Script
{
    public class Patient : MonoBehaviour
    {
        [Header("Paramètres")]
        [Range(0f, 10f)] public float moveSpeed = 7f;
        public PatientStates state = WalkingOut;
        public float timeBeforeDeath = 10f;

        [Header("Prefab Pablo")]
        public GameObject PabloPrefab;

        private Rigidbody2D _rigidbody2D;
        private float timer = 0f;


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            // Debug.Log(state);
        }

        // Update is called once per frame
        private void Update()
        {
            switch (state)
            {
                case Waiting:
                    // Waiting for an empty room
                    timer += Time.deltaTime;
                    Debug.Log("Temps écoulé : " + timer);
                    if (timer > timeBeforeDeath)
                    {
                        state = Dead;
                    }
                    break;
                case Walking:
                    // Walking toward a room
                    break;
                case Laying:
                    // Waiting for treatment
                    break;
                case Dead:
                    // Dead
                    _ = Instantiate(PabloPrefab, transform.position, Quaternion.identity);
                    state = Exited;
                    // PABLO ??!?
                    break;
                case Running:
                    // Panic
                    break;
                case WalkingOut:
                    // Leaving after treatment
                    Movement.Move(new Vector2(-1, 0), moveSpeed, _rigidbody2D);
                    if (_rigidbody2D.position.x < -11) state = Exited;
                    break;
                case Exited:
                    // Ready to be removed from the list
                    Destroy(this.gameObject);
                    break;
                default:
                    Debug.Log("c'est pas normal");
                    break;
            }
        }
    }
}