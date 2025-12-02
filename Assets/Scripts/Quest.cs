using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest System/Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    public string itemName;         // Name of item to collect
    public QuestItem itemPrefab;    // Prefab to spawn
}