using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class speechBubble : MonoBehaviour
{
    [SerializeField] TextMeshPro text;
    public string[] texts;
    void Start()
    {
        text.text = texts[Random.Range(0, texts.Length)];
    }
}
