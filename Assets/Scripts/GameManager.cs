using System.Collections.Generic;
using CustomArrayExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform structureSpawnPos;
    [SerializeField] private StructureObject[] structures;
    [SerializeField] private EnemyObject[] enemies;

    [SerializeField] private HeroData heroData;
    [SerializeField] private TextMeshProUGUI goldText;

    public int enemyLevel = 1;

    public HP_BAR hp_bar;

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

        InvokeRepeating(nameof(CreateEnemy), 2, 2);
        heroData.SetHealthBar(hp_bar);
        UpdateMoney();
    }

    public void CreateBlacksmith() => CreateStructure(StructureObject.Type.Blacksmith);

    public void CreateBoss() => CreateStructure(StructureObject.Type.Boss);
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void CreateEnemy()
    {
        if (Mathf.Approximately(heroData.moveSpeed, 0f)) return;
        CreateStructure(enemies.GetRandom());
    }

    public void CreateHorde() => CreateStructure(StructureObject.Type.Horde);

    public void CreateShop() => CreateStructure(StructureObject.Type.Shop);

    public void CreateHospital() => CreateStructure(StructureObject.Type.Hospital);

    public void CreateStructure(StructureObject.Type type) => CreateStructure(_structureDic[type]);

    private void CreateStructure(StructureObject obj)
    {
        Structure structure = Instantiate(obj.prefab, structureSpawnPos).GetComponent<Structure>();
        structure.StructureObject = obj;
    }

    public void UpdateMoney() => goldText.text = heroData.money + " $";

    public void increaseEnemyLevel(int i)
    {
        enemyLevel += i;
    }
}

namespace CustomArrayExtensions
{
    public static class ArrayExtensions
    {
        public static T GetRandom<T>(this T[] array) => array[Random.Range(0, array.Length)];
    }
}