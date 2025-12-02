using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public int coins = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void AddCoin(int amount)
    {
        coins += amount;
        Debug.Log("Coins: " + coins);
    }
}