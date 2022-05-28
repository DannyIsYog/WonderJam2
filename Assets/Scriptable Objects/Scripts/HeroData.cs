using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Hero Data", menuName = "Objects/Hero Data")]
public class HeroData : ScriptableObject
{
    public float moveSpeed = -5;
    public int maxHealth = 100;
    public int baseDamage = 20;
    [Tooltip("Seconds between each attack")]
    public int baseAttackSpeed = 2;
    public List<ItemObject> acquiredItems;

    [Header("Runtime variables")]
    public int currentHealth;
    public int currentDamage;
    public int currentAttackSpeed;
    public int currentExperience;
    public int currentLevel;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        currentDamage = baseDamage;
        currentAttackSpeed = baseAttackSpeed;
        currentExperience = 0;
        currentLevel = 1;
        acquiredItems.Clear();
        ContinueMoving();
    }

    public void StopMoving() => moveSpeed = 0;
    public void ContinueMoving() => moveSpeed = -5;
}