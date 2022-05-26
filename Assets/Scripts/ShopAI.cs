using UnityEngine;
using System.Collections.Generic;

public class ShopAI : MonoBehaviour
{
    [SerializeField] private string[] _importantStats;
    private Dictionary<string, float> _weights;

    void OnEnable()
    {
        _weights = new Dictionary<string, float>();
        foreach (var stat in _importantStats)
        {
            _weights.Add(stat.ToLower(), Random.value);
        }

        int maxDiscard = _importantStats.Length / 2;
        int statsToDiscard = Random.Range(0, maxDiscard);
        Debug.Log("Discarding " + statsToDiscard + " stats");
        for (int i = 0; i < statsToDiscard; i++)
        {
            var key = _importantStats[Random.Range(0, _importantStats.Length)];
            _weights.Remove(key);
        }
    }

    public float GetValue(ItemObject item) {
        float value = 0;
        
        foreach (var stat in item.stats)
        {
            if (_weights.ContainsKey(stat.statName.ToLower()))
            {
                value += stat.statValue * _weights[stat.statName.ToLower()];
            }
        }

        return value;
    }
}
