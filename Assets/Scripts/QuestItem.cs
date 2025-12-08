using UnityEngine;

public class QuestItem : MonoBehaviour, Interactable
{
    public string itemName;
    public AudioClip collectSound;   // Assign in inspector
    [Range(0f, 1f)] public float volume = 1f; // Control loudness

    private AudioSource audioSource;
    private Renderer itemRenderer;
    private Collider itemCollider;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        itemRenderer = GetComponent<Renderer>();
        itemCollider = GetComponent<Collider>();
    }

    public void Interact()
    {
        // Original logic
        Inventory.Instance.AddItem(itemName);
        CoinManager.Instance.AddCoin();

        // Play sound with set volume
        if (collectSound != null)
        {
            audioSource.volume = volume;
            audioSource.PlayOneShot(collectSound, volume);
        }

        // Make object disappear immediately
        if (itemRenderer != null) itemRenderer.enabled = false;
        if (itemCollider != null) itemCollider.enabled = false;

        // Destroy object after sound
        Destroy(gameObject, collectSound != null ? collectSound.length : 0f);
    }
}