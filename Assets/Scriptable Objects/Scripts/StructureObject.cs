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
        Hospital,
        Shop
    }

    public HeroData heroData;
    public Type type;
    public GameObject prefab;
    public GameEvent OnHeroCollision;
    public Sprite cardSprite;
}