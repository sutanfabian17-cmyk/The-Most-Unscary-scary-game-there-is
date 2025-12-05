using UnityEngine;

public class KeyDoorPuzzle : MonoBehaviour
{
    [Header("References")]
    public GameObject doorObject;  // assign your door sprite

    private BoxCollider2D doorCollider;
    private SpriteRenderer doorRenderer;

    void Start()
    {
        if (doorObject != null)
        {
            doorCollider = doorObject.GetComponent<BoxCollider2D>();
            doorRenderer = doorObject.GetComponent<SpriteRenderer>();

            // Start with door hidden
            doorRenderer.enabled = false;
            doorCollider.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Hide this key
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            // Show door
            if (doorObject != null)
            {
                doorRenderer.enabled = true;   // make door visible
                doorCollider.enabled = true;   // make door block player
            }
        }
    }
}
