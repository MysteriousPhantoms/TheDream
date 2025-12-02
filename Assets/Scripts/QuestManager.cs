using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    private void Awake() => Instance = this;

    public void StartQuest(QuestNPC npc)
    {
        if (npc == null || npc.isQuestActive || npc.isQuestComplete) return;

        npc.isQuestActive = true;

        if (npc.itemPrefab == null)
        {
            Debug.LogError($"Quest item prefab for {npc.npcName} is missing!");
            return;
        }

        // Spawn 3 quest items
        for (int i = 0; i < 3; i++)
        {
            Transform spawn = npc.spawnPoints[i % npc.spawnPoints.Length];
            QuestItem item = Instantiate(npc.itemPrefab, spawn.position, Quaternion.identity);
            item.itemName = npc.itemName;
        }

        Debug.Log($"{npc.npcName} quest started: Collect 3 {npc.itemName}.");
    }

    public void CompleteQuest(QuestNPC npc)
    {
        if (npc == null || npc.isQuestComplete) return;

        string questItem = npc.itemName;

        if (!Inventory.Instance.HasItem(questItem, 3))
        {
            DialogManager.Instance.StartDialog(npc.dialogDuringQuest, npc, showChoice: false);
            return;
        }

        Inventory.Instance.RemoveItem(questItem, 3); // Remove items
        Inventory.Instance.AddItem("Coin");          // Give reward

        npc.isQuestActive = false;
        npc.isQuestComplete = true;
        npc.rewardGiven = true;

        DialogManager.Instance.StartDialog(npc.dialogAfterQuest, npc, showChoice: false);
        Debug.Log($"{npc.npcName} quest completed! Coin added to inventory.");
    }
}