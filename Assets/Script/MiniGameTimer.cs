using UnityEngine;
using UnityEngine.UI; 
public class MinigameTimer : MonoBehaviour
{
    public Image timerBar; // La barre visuelle à lier
    public float maxTime = 10f; 

    private float currentTime;
    private bool isPlaying = false;

    void OnEnable()
    {
        currentTime = maxTime;
        isPlaying = true;
        if (timerBar != null)
        {
            timerBar.fillAmount = 1f;
        }
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
            // Le chronomètre a atteint zéro
            isPlaying = false;
            timerBar.fillAmount = 0;
            Debug.Log("Temps écoulé, perdu");
            gameObject.SetActive(false); 
        }
    }
}