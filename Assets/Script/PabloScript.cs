using Assets.Script;
using Script;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PabloScript : MonoBehaviour
{
    public Transform playerTransform;
    [Range(0f, 10f)] public float moveSpeed = 5f;
    public float distanceNearPlayer = 1.5f; // Distance à laquelle Pablo considère qu'il est "proche" du joueur

    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        // Fallback : si on a pas assigné le transform du joueur dans Unity : on le cherche
        if (playerTransform == null)
        {
            var player = FindAnyObjectByType<PlayerMovement>();
            if (player != null)
            {
                playerTransform = player.transform;
            }
        }
    }

    private void Update()
    {
        if (playerTransform == null) return; // Fallback : si on a pas trouvé le joueur, on ne fait rien

        Vector2 direction = playerTransform.position - transform.position; // direction vers le joueur
        float distance = direction.magnitude; // distance entre Pablo et le joueur

        if (distance > distanceNearPlayer)
        {
            Vector2 moveDirection = direction.normalized; // Vecteur unitaire de direction
            Movement.Move(moveDirection, moveSpeed, _rigidbody2D);
        }
        else
        {
            Movement.Move(Vector2.zero, moveSpeed, _rigidbody2D);
        }
    }
}