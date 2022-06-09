using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Shop : MonoBehaviour
{
    [SerializeField] private float timeUntilClose = 5;
    [SerializeField] private GameEvent OnShopLeave;
    [SerializeField] private ItemObject[] items;
    [SerializeField] private Slot[] shopSlots;

    public List<ItemObject> currentItemsInShop = new();

    private List<int> _randList = new();
    private float _timer;

    private void OnEnable()
    {
        _timer = timeUntilClose;
        foreach (Slot slot in shopSlots)
        {
            int rand = Random.Range(0, shopSlots.Length);
            while (_randList.Contains(rand))
                rand = Random.Range(0, shopSlots.Length);
            _randList.Add(rand);
            
            ItemObject item = items[rand];
            currentItemsInShop.Add(item);
            slot.imageObj.sprite = item.sprite;
            slot.textNameObj.text = item.itemName;
            slot.textStatsObj.text = "";
            foreach (ItemObject.Stat itemStat in item.stats)
                slot.textStatsObj.text += itemStat.statType + ": " + itemStat.statValue + "\n";
        }
        
        ShopAI.instance.BuyItems(currentItemsInShop);
    }

    private void Update()
    {
        if (_timer > 0) _timer -= Time.deltaTime;
        else gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        _randList = new List<int>();
        currentItemsInShop.Clear();
        OnShopLeave.Raise();
    }

    [Serializable]
    private class Slot
    {
        public GameObject itemObj;
        public Image imageObj;
        public TextMeshProUGUI textNameObj;
        public TextMeshProUGUI textStatsObj;
    }
}