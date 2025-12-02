using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager Instance;
    public GameObject dialogBox;
    public Text dialogText;
    public float lettersPerSecond = 30f;

    private QuestNPC currentNPC;
    private bool showChoicePanel = false;

    private void Awake() => Instance = this;

    public void StartDialog(Dialog dialog, QuestNPC npc, bool showChoice = false)
    {
        currentNPC = npc;
        showChoicePanel = showChoice;
        StartCoroutine(RunDialog(dialog));
    }

    private IEnumerator RunDialog(Dialog dialog)
    {
        dialogBox.SetActive(true);

        foreach (string line in dialog.Lines)
        {
            dialogText.text = "";
            foreach (char c in line)
            {
                dialogText.text += c;
                yield return new WaitForSeconds(1f / lettersPerSecond);
            }
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        dialogBox.SetActive(false);

        if (showChoicePanel && currentNPC != null)
            ChoicePanel.Instance.ShowChoices($"Start quest for {currentNPC.questName}?", 
                onOk: () => QuestManager.Instance.StartQuest(currentNPC));
    }
}