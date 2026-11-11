using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MinigameManager : MonoBehaviour
{
    [Header("Chrono")]
    public Image timerBar;
    public float maxTime = 10f;

    [Header("Événements personnalisés")]
    public UnityEvent onGameStart; // Fonction init du mini-jeu
    public UnityEvent onGameWon;   // Inutilisé pour le moment, servira pour gérer la victoire
    public UnityEvent onGameLost;  // Inutilisé pour le moment, servira pour gérer la défaite

    private float currentTime;
    private bool isPlaying = false;
    public void OpenMinigame()
    {
        gameObject.SetActive(true);
        currentTime = maxTime;
        isPlaying = true;
        if (timerBar != null) timerBar.fillAmount = 1f;
        onGameStart.Invoke();
    }

    void Update()
    {
        if (!isPlaying || timerBar == null) return;

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            timerBar.fillAmount = currentTime / maxTime;
        }
        else
        {
            LoseGame();
        }
    }

    public void WinGame()
    {
        if (!isPlaying) return;
        isPlaying = false;
        Debug.Log("Victoire");
        onGameWon.Invoke();
        CloseMinigame();
    }

    public void LoseGame()
    {
        if (!isPlaying) return;
        isPlaying = false;
        timerBar.fillAmount = 0;
        Debug.Log("Défaite");
        onGameLost.Invoke();
        CloseMinigame();
    }

    public void CloseMinigame()
    {
        isPlaying = false;
        gameObject.SetActive(false);
    }
}