using UnityEngine;
using UnityEngine.UI;
using System;

public class ChoicePanel : MonoBehaviour
{
    public static ChoicePanel Instance;
    public GameObject panel;
    public Text choiceText;
    public Button okButton;
    public Button cancelButton;

    private Action onOkCallback;
    private Action onCancelCallback;

    private void Awake()
    {
        Instance = this;
        panel.SetActive(false);
        okButton.onClick.AddListener(OnAccept);
        cancelButton.onClick.AddListener(OnCancel);
    }

    public void ShowChoices(string message, Action onOk = null, Action onCancel = null)
    {
        choiceText.text = message;
        panel.SetActive(true);
        onOkCallback = onOk;
        onCancelCallback = onCancel;
    }

    private void OnAccept()
    {
        panel.SetActive(false);
        onOkCallback?.Invoke();
        ClearCallbacks();
    }

    private void OnCancel()
    {
        panel.SetActive(false);
        onCancelCallback?.Invoke();
        ClearCallbacks();
    }

    private void ClearCallbacks()
    {
        onOkCallback = null;
        onCancelCallback = null;
    }
}
