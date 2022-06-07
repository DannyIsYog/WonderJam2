using System.Collections.Generic;
using CustomArrayExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Structures and Enemies")]
    [SerializeField] private Transform structureSpawnPos;
    [SerializeField] private StructureObject[] structures;
    [SerializeField] private EnemyObject[] enemies;
    
    [Header("Cards")]
    [SerializeField] private Transform[] cardSlotsPos;
    [SerializeField] private GameObject cardPrefab;

    [Header("Hero and Player")]
    [SerializeField] private HeroData heroData;
    [SerializeField] private TextMeshProUGUI goldText;

    public int enemyLevel = 1;

    public HP_BAR hp_bar;

    public static GameManager instance { get; private set; }
    public Dictionary<int, DragDropCard> Cards { get; } = new();

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

        for (int i = 0; i < cardSlotsPos.Length; i++)
            Cards.Add(i, null);

        InvokeRepeating(nameof(CreateEnemy), 2, 2);
        heroData.SetHealthBar(hp_bar);
        UpdateMoney();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool AddCard(StructureObject.Type type)
    {
        foreach (int slotIdx in Cards.Keys)
        {
            if (Cards[slotIdx] == null)
            {
                DragDropCard card = Instantiate(cardPrefab, cardSlotsPos[slotIdx]).GetComponent<DragDropCard>();
                card.Init(_structureDic[type], cardSlotsPos[slotIdx].position);
                Cards[slotIdx] = card;
                return true;
            }
        }

        Debug.Log("Card not added");
        return false;
    }

    public void AddShop()
    {
        AddCard(StructureObject.Type.Shop);
    }
    
    public void AddHospital()
    {
        AddCard(StructureObject.Type.Hospital);
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

    public void CreateHospital() => CreateStructure(StructureObject.Type.Hospital);

    public void CreateStructure(StructureObject.Type type) => CreateStructure(_structureDic[type]);

    private void CreateStructure(StructureObject structureObj)
    {
        Structure structure = Instantiate(structureObj.prefab, structureSpawnPos).GetComponent<Structure>();
        structure.StructureObject = structureObj;
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