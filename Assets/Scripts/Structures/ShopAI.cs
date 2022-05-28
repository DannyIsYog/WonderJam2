using System.Collections.Generic;
using UnityEngine;

using static ItemStats;

public class ShopAI : MonoBehaviour
{
    [SerializeField] private float _discardRate = 0.3f;
    private Dictionary<Stats, float> _weights;

    private void OnEnable()
    {
        Stats[] allStats = (Stats[]) System.Enum.GetValues(typeof(Stats));
        _weights = new Dictionary<Stats, float>();
        foreach (Stats stat in allStats)
        {
            _weights.Add(stat, Random.value < _discardRate ? 0 : Random.Range(-1f, 1f));
        }

        // Print the name of each stat and its weight
        foreach (KeyValuePair<Stats, float> stat in _weights)
        {
            Debug.Log(stat.Key + ": " + stat.Value);
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
