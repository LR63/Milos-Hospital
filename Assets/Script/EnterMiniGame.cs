using UnityEngine;

public class EnterMiniGame : MonoBehaviour
{
    public GameObject minigamePanel;


    void Start()
    {
        if (minigamePanel != null)
            minigamePanel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            minigamePanel.SetActive(true);
            Cursor.visible = true; // Si applicable pour ton jeu
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            minigamePanel.SetActive(false);
        }
    }
}