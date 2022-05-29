using System.Collections.Generic;
using CustomArrayExtensions;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform structureSpawnPos;
    [SerializeField] private StructureObject[] structures;
    [SerializeField] private EnemyObject[] enemies;

    [SerializeField] private HeroData heroData;

    public static GameManager instance { get; private set; }

    private readonly Dictionary<StructureObject.Type, StructureObject> _structureDic = new();

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (StructureObject structureObj in structures)
            _structureDic[structureObj.type] = structureObj;

        InvokeRepeating("CreateEnemy", 2, 2);
    }

    public void CreateBlacksmith() => CreateStructure(StructureObject.Type.Blacksmith);

    public void CreateBoss() => CreateStructure(StructureObject.Type.Boss);

    public void CreateEnemy()
    {
        if (Mathf.Approximately(heroData.moveSpeed, 0f)) return;
        CreateStructure(enemies.GetRandom());
    }

    public void CreateHorde() => CreateStructure(StructureObject.Type.Horde);

    public void CreateShop() => CreateStructure(StructureObject.Type.Shop);

    private void CreateStructure(StructureObject.Type type) => CreateStructure(_structureDic[type]);

    private void CreateStructure(StructureObject obj)
    {
        Structure structure = Instantiate(obj.prefab, structureSpawnPos).GetComponent<Structure>();
        structure.StructureObject = obj;
    }
}

namespace CustomArrayExtensions
{
    public static class ArrayExtensions
    {
        public static T GetRandom<T>(this T[] array) => array[Random.Range(0, array.Length)];
    }
}