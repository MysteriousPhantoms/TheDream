using UnityEngine;

public class InteractableObject : MonoBehaviour, Interactable
{
    [TextArea] public string[] dialogLines;

    [Header("UI / Bubble")]
    public GameObject interactPromptUI;   // On-screen "Press E"
    public GameObject interactBubblePrefab;
    public float bubbleHeight = 2f;

    private GameObject interactBubbleInstance;
    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;

            // Debug
            Debug.Log("Player entered trigger");

            // Show UI prompt
            if (interactPromptUI != null)
                interactPromptUI.SetActive(true);

            // Spawn bubble above object
            if (interactBubblePrefab != null && interactBubbleInstance == null)
            {
                Vector3 pos = transform.position + Vector3.up * bubbleHeight;
                interactBubbleInstance = Instantiate(interactBubblePrefab, pos, Quaternion.identity, transform);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player exited trigger");

            if (interactPromptUI != null)
                interactPromptUI.SetActive(false);

            if (interactBubbleInstance != null)
                Destroy(interactBubbleInstance);
        }
    }

    public void Interact()
    {
        if (interactPromptUI != null)
            interactPromptUI.SetActive(false);

        if (interactBubbleInstance != null)
            Destroy(interactBubbleInstance);

        // Start dialog
        Dialog dialog = new Dialog { Lines = dialogLines };
        DialogManager.Instance.StartDialog(dialog, null, false);
    }
}
