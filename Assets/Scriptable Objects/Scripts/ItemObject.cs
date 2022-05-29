using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Item", menuName = "Objects/Item")]
public class ItemObject : ScriptableObject
{
    public enum Type
    {
        Armor,
        Weapon
    }
    
    public string itemName;
    public Sprite sprite;
    public Type type;
    public Stat[] stats;
    public int price;

    [Serializable]
    public class Stat
    {
        public ItemStats.Stats statName;
        public float statValue;

        public float GenRandomStatVal(float min, float max) => Random.Range(min, max);
    } 
}