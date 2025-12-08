using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour, Interactable
{
    public void Interact()
    {
        SceneManager.LoadScene("EndGame");
    }
}