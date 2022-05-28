using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Objects/Enemy")]
public class EnemyObject : StructureObject
{
    public string enemyName;
    public int maxHealth = 50;
    public int baseDamage = 5;
    [Header("Seconds between each attack")]
    public int attackSpeed = 1;

    private void Awake()
    {
        type = Type.Enemy;
    }
}