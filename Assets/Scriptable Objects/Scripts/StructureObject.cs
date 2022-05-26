using UnityEngine;

[CreateAssetMenu(fileName = "New Structure", menuName = "Objects/Structure")]
public class StructureObject : ScriptableObject
{
    public enum Type
    {
        Blacksmith,
        Boss,
        Enemy,
        Horde,
        Shop
    }

    public Type type;
    public GameObject prefab;
    public GameEvent OnHeroCollision;
    public float minDropRate;
    public float maxDropRate;
}