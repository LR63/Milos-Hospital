using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MinigameManager : MonoBehaviour
{
    [Header("Chrono")]
    public RectTransform timerBar;
    public float maxTime = 10f;
    private float initialTimerWidth;

    [Header("Événements personnalisés")]
    public UnityEvent onGameStart; // Fonction init du mini-jeu, à changer selon le mini-jeu

    [Header("Panel du mini-jeu")]
    public GameObject minigamePanel; // Panel du mini-jeu, à changer selon le mini-jeu

    private float currentTime;
    private bool isPlaying = false;
    public bool IsPlaying => isPlaying; // Propriété publique qui équivaut à un getter pour isPlaying
    public void OpenMinigame()
    {
        currentTime = maxTime;
        isPlaying = true;
        if (timerBar != null)
        {
            if (initialTimerWidth == 0)
            {
                initialTimerWidth = timerBar.rect.width;
            }
            timerBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, initialTimerWidth);
        }

        onGameStart.Invoke();
        minigamePanel.SetActive(true);
    }

    void Update()
    {
        if (!isPlaying || timerBar == null) return;

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            float percent = currentTime / maxTime;
            timerBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, initialTimerWidth * percent);
        }
        else
        {
            LoseGame();
        }
    }

    public void ShowMinigame() => minigamePanel.SetActive(true);

    public void HideMinigame() => minigamePanel.SetActive(false);

    public void WinGame()
    {
        if (!isPlaying) return;
        isPlaying = false;
        Debug.Log("Victoire");
        CloseMinigame();
    }

    public void LoseGame()
    {
        if (!isPlaying) return;
        isPlaying = false;
        timerBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        Debug.Log("Défaite");
        CloseMinigame();
    }

    public void CloseMinigame()
    {
        isPlaying = false;
        minigamePanel.SetActive(false);
    }
}