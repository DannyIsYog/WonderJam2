using System.Collections.Generic;
using UnityEngine;

using static ItemStats;

public class ShopAI : MonoBehaviour
{
    [SerializeField] private float _discardRate = 0.3f;
    private Dictionary<Stats, float> _weights;

    [SerializeField] private HeroData _heroData;
    private Shop _shop;

    private void Start()
    {
        _shop = GetComponent<Shop>();
        Stats[] allStats = (Stats[]) System.Enum.GetValues(typeof(Stats));
        _weights = new Dictionary<Stats, float>();
        foreach (Stats stat in allStats)
        {
            _weights.Add(stat, Random.value < _discardRate ? 0 : Random.Range(-1f, 1f));
        }
    }

    float GetValue(ItemObject item) {
        float value = 0;
        
        foreach (ItemObject.Stat stat in item.stats)
        {
            if (_weights.ContainsKey(stat.statName))
                value += stat.statValue * _weights[stat.statName];
        }

        return value;
    }

    public void BuyItems() {
        List<ItemObject> items = new List<ItemObject>();
        foreach (ItemObject item in _shop.CurrentItemsInShop)
        {
            if (GetValue(item) > 0 && _heroData.money >= item.price)
                items.Add(item);
        }

        items.Sort((a, b) => GetValue(b).CompareTo(GetValue(a)));

        int money = _heroData.money;
        foreach (ItemObject item in items)
        {
            if (money >= item.price)
            {
                money -= item.price;
                _heroData.acquiredItems.Add(item);
            }
            else
            {
                break;
            }
        }

        _heroData.money = money;
    }
}
