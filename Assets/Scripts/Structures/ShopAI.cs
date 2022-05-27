using UnityEngine;
using System.Collections.Generic;
using static ItemStats;

public class ShopAI : MonoBehaviour
{
    [SerializeField] private Stats[] _importantStats;
    private Dictionary<Stats, float> _weights;

    private void OnEnable()
    {
        _weights = new Dictionary<Stats, float>();
        foreach (Stats stat in _importantStats)
            _weights.Add(stat, Random.value);

        int maxDiscard = _importantStats.Length / 2;
        int statsToDiscard = Random.Range(0, maxDiscard);
        Debug.Log("Discarding " + statsToDiscard + " stats");
        for (int i = 0; i < statsToDiscard; i++)
        {
            Stats key = _importantStats[Random.Range(0, _importantStats.Length)];
            _weights.Remove(key);
        }
    }

    public float GetValue(ItemObject item) {
        float value = 0;
        
        foreach (ItemObject.Stat stat in item.stats)
        {
            if (_weights.ContainsKey(stat.statName))
                value += stat.statValue * _weights[stat.statName];
        }

        return value;
    }
}
