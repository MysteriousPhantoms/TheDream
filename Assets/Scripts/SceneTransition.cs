using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;

    [Header("Fade Settings")]
    public Image fadeImage;       // Assign FadePanel Image here
    public float fadeTime = 1f;
    public AudioSource transitionSound; // Optional sound

    [Header("Key Settings")]
    public KeyCode transitionKey = KeyCode.Space; // Key to press to go to main game

    private bool isTransitioning = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        // Wait for key press
        if (!isTransitioning && Input.GetKeyDown(transitionKey))
        {
            StartCoroutine(TransitionRoutine());
        }
    }

    /// <summary>
    /// Call this to start scene transition manually (optional)
    /// </summary>
    public void GoToMainGame()
    {
        if (!isTransitioning)
            StartCoroutine(TransitionRoutine());
    }

    private IEnumerator TransitionRoutine()
    {
        isTransitioning = true;

        // Play sound if assigned
        if (transitionSound != null)
            transitionSound.Play();

        // Fade to black
        float t = 0f;
        Color c = fadeImage.color;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(0, 1, t / fadeTime);
            fadeImage.color = c;
            yield return null;
        }

        // Load the main game scene
        SceneManager.LoadScene("The Dream");
    }
}
