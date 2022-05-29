using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Shop : MonoBehaviour
{
    [SerializeField] private float timeUntilClose = 3;
    [SerializeField] private ItemObject[] items;
    [SerializeField] private Slot[] shopSlots;

    public List<ItemObject> CurrentItemsInShop = new();

    private List<int> randList = new();
    private float time;
    
    private void OnEnable()
    {
        time = timeUntilClose;
        foreach (Slot slot in shopSlots)
        {
            int rand = Random.Range(0, shopSlots.Length);
            while (randList.Contains(rand))
                rand = Random.Range(0, shopSlots.Length);
            randList.Add(rand);
            
            ItemObject item = items[rand];
            CurrentItemsInShop.Add(item);
            slot.imageObj.sprite = item.sprite;
            slot.textNameObj.text = item.itemName;
            slot.textStatsObj.text = "";
            foreach (ItemObject.Stat itemStat in item.stats)
                slot.textStatsObj.text += itemStat.statName + ": " + itemStat.statValue + "\n";
        }
    }

    private void Update()
    {
        if (time > 0) time -= Time.deltaTime;
        else gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        randList = new List<int>();
    }

    [Serializable]
    private class Slot
    {
        public Image imageObj;
        public TextMeshProUGUI textNameObj;
        public TextMeshProUGUI textStatsObj;
    }
}