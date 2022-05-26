using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float heroMoveSpeed = -5;
    [SerializeField] private Transform structureSpawnPos;
    [SerializeField] private StructureObject[] structures;

    public static GameManager instance { get; private set; }

    public float HeroMoveSpeed
    {
        get => heroMoveSpeed;
        set => heroMoveSpeed = value;
    }

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

        foreach (StructureObject structureObject in structures)
            _structureDic[structureObject.type] = structureObject;
    }

    private void CreateStructure(StructureObject.Type type)
    {
        StructureObject obj = _structureDic[type];
        Structure structure = Instantiate(obj.prefab, structureSpawnPos).GetComponent<Structure>();
        structure.SetStructureObject(obj);
    }

    public void CreateBlacksmith() => CreateStructure(StructureObject.Type.Blacksmith);
    public void CreateBoss() => CreateStructure(StructureObject.Type.Boss);
    public void CreateEnemy() => CreateStructure(StructureObject.Type.Enemy);
    public void CreateHorde() => CreateStructure(StructureObject.Type.Horde);
    public void CreateShop() => CreateStructure(StructureObject.Type.Shop);
}