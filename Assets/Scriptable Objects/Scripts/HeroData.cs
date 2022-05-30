using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hero Data", menuName = "Objects/Hero Data")]
public class HeroData : ScriptableObject
{
    public float moveSpeed = -5;
    public int maxHealth = 100;
    public int baseDamage = 20;
    [Tooltip("Seconds between each attack")]
    public float baseAttackSpeed = 2;
    public int money;
    public AnimationCurve experienceLevelCurve;
    public List<ItemObject> acquiredItems;

    [Header("Runtime variables")]
    public int currentHealth;
    public int currentDamage;
    public float currentAttackSpeed;
    public int currentExperience;
    public int remainingExperience;
    public int currentLevel;

    private int _maxLevel, _previousExperience, _nextExperience;

    public HP_BAR health_bar;

    private void OnEnable()
    {
        moveSpeed = -5;
        currentHealth = maxHealth;
        currentDamage = baseDamage;
        currentAttackSpeed = baseAttackSpeed;
        currentExperience = currentLevel = _previousExperience = 0;
        _maxLevel = (int)experienceLevelCurve[experienceLevelCurve.length - 1].time;
        money = 0;
        acquiredItems = new List<ItemObject>();
        UpdateExperience(0);
    }

    public void StopMoving() => moveSpeed = 0;
    public void ContinueMoving() => moveSpeed = -5;

    public void UpdateExperience(int value)
    {
        currentExperience += value;

        if (currentExperience - _previousExperience < 0) currentLevel--;
        if (_nextExperience - currentExperience < 0) currentLevel++;
        if (currentLevel < 0) currentLevel = 0;
        if (currentExperience < 0) currentExperience = 0;

        _previousExperience = (int)experienceLevelCurve.Evaluate(currentLevel);
        _nextExperience = (int)experienceLevelCurve.Evaluate(currentLevel + 1);
        remainingExperience = _nextExperience - currentExperience;
    }

    public void SetHealthBar(HP_BAR hp_bar)
    {
        health_bar = hp_bar;
        health_bar.SetMaxHealth(maxHealth);
    }

    public void HealHero(int value)
    {
        maxHealth = maxHealth + 10 * GameManager.instance.enemyLevel;
        currentHealth = Mathf.Clamp(value, 0, maxHealth);
        health_bar.SetHealth(currentHealth);
    }

    public void GetDamage(int damage)
    {
        currentHealth -= damage;
        health_bar.SetHealth(currentHealth);
    }

    public void AcquireItem(ItemObject item)
    {
        acquiredItems.Add(item);
        foreach (ItemObject.Stat stat in item.stats)
        {
            if (stat.statName == ItemStats.Stats.Damage) currentDamage += (int)stat.statValue;
        }

    }
#if UNITY_EDITOR
    [CustomEditor(typeof(HeroData))]
    private class HeroDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            HeroData heroData = (HeroData)target;
            if (GUILayout.Button("Show XP Table"))
            {
                ExperienceWindow expWindow = (ExperienceWindow)EditorWindow.GetWindow(typeof(ExperienceWindow));
                expWindow.Init(heroData.experienceLevelCurve, heroData._maxLevel);
            }
        }
    }
#endif
}