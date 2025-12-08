using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    public int coinsCollected = 0;
    public int coinsNeeded = 4;

    public GameObject portal;   // Drag your portal object here in Inspector

    private void Awake()
    {
        Instance = this;
    }

    public void AddCoin()
    {
        coinsCollected++;

        if (coinsCollected >= coinsNeeded)
        {
            portal.SetActive(true);   // ← Portal becomes visible now
        }
    }
}