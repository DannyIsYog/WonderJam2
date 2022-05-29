using System;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Objects/Enemy")]
public class EnemyObject : StructureObject
{
    public string enemyName;
    public int maxHealth = 50;
    public int baseDamage = 5;
    [Tooltip("Seconds between each attack")]
    public float attackSpeed = 1;
    public AnimationCurve experienceWhenKilled;
    public int CurrentExperienceWhenKilled => (int)experienceWhenKilled.Evaluate(heroData.currentLevel);
    public static int DroppedMoney => Random.Range(1, 20);

    private void Awake()
    {
        type = Type.Enemy;
    }

    [CustomEditor(typeof(EnemyObject))]
    private class HeroDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EnemyObject enemyObject = (EnemyObject)target;
            if (GUILayout.Button("Show XP When Killed Table"))
            {
                ExperienceWindow expWindow = (ExperienceWindow)EditorWindow.GetWindow(typeof(ExperienceWindow));
                expWindow.Init(enemyObject.experienceWhenKilled,
                    (int)enemyObject.experienceWhenKilled[enemyObject.experienceWhenKilled.length - 1].time);
            }
        }
    }
}