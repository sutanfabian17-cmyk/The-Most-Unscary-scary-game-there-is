using UnityEngine;
using System.Collections;

public class ExitDoor : MonoBehaviour
{
    [Header("Exit Settings")]
    public AudioSource exitSound;        // assign your mp3 AudioSource here
    public float delay = 4f;             // delay before playing sound
    public GameObject victoryUI;         // assign your Victory Screen UI Canvas

    private BoxCollider2D doorCollider;
    private SpriteRenderer doorRenderer;

    void Start()
    {
        doorCollider = GetComponent<BoxCollider2D>();
        doorRenderer = GetComponent<SpriteRenderer>();

        // Ensure victory UI is hidden at start
        if (victoryUI != null)
        {
            victoryUI.SetActive(false);
        }

        // Ensure audio does not auto-play at start
        if (exitSound != null)
        {
            exitSound.playOnAwake = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Maze completed! Player reached the exit door.");

            // Optionally hide or disable the door
            doorRenderer.enabled = false;
            doorCollider.enabled = false;

            // Start coroutine for sound + victory UI
            StartCoroutine(PlayExitSoundAndShowUI());
        }
    }

    private IEnumerator PlayExitSoundAndShowUI()
    {
        // Wait before playing sound
        yield return new WaitForSeconds(delay);

        if (exitSound != null)
        {
            exitSound.Play();
        }

        // Show victory UI after sound starts
        if (victoryUI != null)
        {
            victoryUI.SetActive(true);
        }
    }

    // Quit button method
    public void QuitGame()
    {
        Debug.Log("Quit Game pressed!");
        Application.Quit();

        // Note: In the Unity Editor, Application.Quit() does nothing.
        // It only works in a built game.
    }
}
