using UnityEditor;
using UnityEngine;

public class ExperienceWindow : EditorWindow
{
    private AnimationCurve _curve;
    private Vector2 _scroll;
    private int _maxLevel;

    public void Init(AnimationCurve curve, int maxLevel)
    {
        _curve = curve;
        _maxLevel = maxLevel;
    }

    private void OnGUI()
    {
        _scroll = EditorGUILayout.BeginScrollView(_scroll);

        for (int i = 1; i <= _maxLevel; i++)
        {
            EditorGUILayout.BeginHorizontal("box");
            EditorGUILayout.LabelField("Level " + i);
            EditorGUILayout.LabelField((int)_curve.Evaluate(i) + " XP");
            EditorGUILayout.EndHorizontal();
        }
            
        EditorGUILayout.EndScrollView();
    }
}