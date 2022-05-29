using System;
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

    private void OnEnable()
    {
        moveSpeed = -5;
        currentHealth = maxHealth;
        currentDamage = baseDamage;
        currentAttackSpeed = baseAttackSpeed;
        currentExperience = currentLevel = _previousExperience = 0;
        _maxLevel = (int)experienceLevelCurve[experienceLevelCurve.length - 1].time;
        acquiredItems.Clear();
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
}