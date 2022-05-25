using UnityEngine;
using UnityEngine.Pool;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float heroMoveSpeed = -5;
    [SerializeField] private GameObject structurePrefab;
    [SerializeField] private Transform structureSpawnPos;

    public ObjectPool<Structure> structurePool;
    public static GameManager instance { get; private set; }

    public float HeroMoveSpeed
    {
        get => heroMoveSpeed;
        set => heroMoveSpeed = value;
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        structurePool = new ObjectPool<Structure>(CreateStructure, OnTakeStructureFromPool, OnReturnStructureToPool);
    }

    public Structure CreateStructure()
    {
        Structure structure = Instantiate(structurePrefab, structureSpawnPos).GetComponent<Structure>();
        structure.SetPool(structurePool);
        return structure;
    }

    private void OnTakeStructureFromPool(Structure structure)
    {
        structure.gameObject.SetActive(true);
    }

    private void OnReturnStructureToPool(Structure structure)
    {
        structure.SetStartPos(structureSpawnPos.position);
        structure.gameObject.SetActive(false);
    }

    public void CreateShop()
    {
        structurePool.Get();
    }
}