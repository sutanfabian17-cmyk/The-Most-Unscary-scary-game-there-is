using UnityEngine;

public class JumpscareTrigger : MonoBehaviour
{
    public GameObject jumpscareObject;   // scary UI image
    public AudioSource jumpscareSound;   // scream sound (keep this on a separate active GameObject)
    private CanvasGroup canvasGroup;

    private void Start()
    {
        if (jumpscareObject != null)
        {
            canvasGroup = jumpscareObject.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                // Add CanvasGroup automatically if missing
                canvasGroup = jumpscareObject.AddComponent<CanvasGroup>();
            }
            jumpscareObject.SetActive(false); // start hidden
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jumpscareObject.SetActive(true);

            if (jumpscareSound != null && jumpscareSound.enabled)
            {
                jumpscareSound.Play();
            }

            StartCoroutine(FadeOut());
        }
    }

    private System.Collections.IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2f); // wait before fading

        float duration = 1.5f; // fade time
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
            yield return null;
        }

        jumpscareObject.SetActive(false); // fully hidden
        canvasGroup.alpha = 1f; // reset for next trigger
    }
}
