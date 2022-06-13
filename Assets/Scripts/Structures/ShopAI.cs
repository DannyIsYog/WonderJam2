using System.Collections.Generic;
using UnityEngine;

using static ItemStats;
using Random = UnityEngine.Random;

public class ShopAI : MonoBehaviour
{
    [SerializeField] private float _discardRate = 0.3f;
    [SerializeField] private HeroData _heroData;

    public static ShopAI instance;

    private readonly Dictionary<Stats, float> _weights = new();

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        Stats[] allStats = (Stats[])System.Enum.GetValues(typeof(Stats));
        foreach (Stats stat in allStats)
            _weights.Add(stat, Random.value < _discardRate ? 0 : Random.Range(-1f, 1f));
    }

    private float GetValue(ItemObject item)
    {
        float value = 0;

        foreach (ItemObject.Stat stat in item.stats)
        {
            if (_weights.ContainsKey(stat.statType))
                value += stat.statValue * _weights[stat.statType];
        }

        return value;
    }

    public void BuyItems(List<ItemObject> itemsInShop)
    {
        List<ItemObject> items = new();
        foreach (ItemObject item in itemsInShop)
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
                _heroData.AcquireItem(item);
            }
            else break;
        }

        _heroData.money = money;
        GameManager.instance.UpdateMoney();
    }
}
