using System;
using System.Collections;
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
    [SerializeField] private Color defaultItemColor;
    [SerializeField] private Color selectedItemColor;
    [SerializeField] private HeroData heroData;

    public List<ItemObject> currentItemsInShop = new();

    private List<Image> _slotsImage = new();
    private List<int> _randList = new();
    private float _timer;
    private bool _flag;
    private int _numItemsBeforePurchase;

    private void Awake()
    {
        foreach (Slot slot in shopSlots)
            _slotsImage.Add(slot.itemObj.GetComponent<Image>());
    }

    private void OnEnable()
    {
        _timer = timeUntilClose;
        _numItemsBeforePurchase = heroData.acquiredItems.Count;
        _flag = false;
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
        
        if (_timer <= timeUntilClose / 2 && !_flag && _numItemsBeforePurchase < heroData.acquiredItems.Count)
        {
            Image selectedSlotImage = null;
            for (int i = 0; i < items.Length; i++)
            {
                if (currentItemsInShop[i] == heroData.acquiredItems[^1])
                {
                    selectedSlotImage = _slotsImage[i];
                    break;
                }
            }
            StartCoroutine(SlotSelectedVisual(selectedSlotImage));
            _flag = true;
        }
    }

    private void OnDisable()
    {
        _randList = new List<int>();
        currentItemsInShop.Clear();
        foreach (Image image in _slotsImage) 
            image.color = defaultItemColor;
        OnShopLeave.Raise();
    }

    private IEnumerator SlotSelectedVisual(Image slotImage)
    {
        slotImage.color = selectedItemColor;
        yield return new WaitForSeconds(timeUntilClose / 10f);
        slotImage.color = defaultItemColor;
        yield return new WaitForSeconds(timeUntilClose / 10f);
        slotImage.color = selectedItemColor;
        yield return null;
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