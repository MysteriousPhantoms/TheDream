using UnityEngine;

public class QuestNPC : MonoBehaviour, Interactable
{
    [Header("NPC Info")]
    public string npcName;

    [Header("Quest Info")]
    public string questName;      // You can type the quest name
    public string itemName;       // You can type the quest item name
    public QuestItem itemPrefab;  // Prefab to spawn

    public Transform[] spawnPoints;

    [Header("Dialogs")]
    public Dialog dialogBeforeQuest;
    public Dialog dialogDuringQuest;
    public Dialog dialogAfterQuest;
    public Dialog dialogPostQuest;

    [HideInInspector] public bool isQuestActive = false;
    [HideInInspector] public bool isQuestComplete = false;
    [HideInInspector] public bool rewardGiven = false;

    public void Interact()
    {
        if (!isQuestActive && !isQuestComplete)
        {
            DialogManager.Instance.StartDialog(dialogBeforeQuest, this, showChoice: true);
        }
        else if (isQuestActive && !isQuestComplete)
        {
            QuestManager.Instance.CompleteQuest(this);
        }
        else
        {
            DialogManager.Instance.StartDialog(dialogPostQuest, this, showChoice: false);
        }
    }
}