using UnityEngine;

public class EnterMiniGame : MonoBehaviour
{
    public MinigameManager manager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && manager != null)
        {
            manager.OpenMinigame();
            Cursor.visible = true;
        }
    }
}