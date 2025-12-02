using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [Header("UI References")]
    public GameObject inventoryUI;
    public Transform itemContainer;
    public GameObject itemTextPrefab;

    private List<string> items = new List<string>();

    private void Awake() => Instance = this;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

    public void AddItem(string itemName)
    {
        if (string.IsNullOrEmpty(itemName)) return;

        items.Add(itemName);
        UpdateInventoryUI();
        Debug.Log($"Added {itemName} to inventory.");
    }

    public void RemoveItem(string itemName, int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            int index = items.FindIndex(s => s.Trim().ToLower() == itemName.Trim().ToLower());
            if (index >= 0) items.RemoveAt(index);
        }
        UpdateInventoryUI();
    }

    public bool HasItem(string itemName, int count)
    {
        int found = 0;
        foreach (string s in items)
            if (s.Trim().ToLower() == itemName.Trim().ToLower()) found++;
        return found >= count;
    }

    private void UpdateInventoryUI()
    {
        if (inventoryUI == null || itemContainer == null || itemTextPrefab == null) return;

        foreach (Transform t in itemContainer)
            Destroy(t.gameObject);

        foreach (string item in items)
        {
            GameObject obj = Instantiate(itemTextPrefab, itemContainer);
            Text text = obj.GetComponent<Text>();
            if (text != null) text.text = item;
        }
    }
}