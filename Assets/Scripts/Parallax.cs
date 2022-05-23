using System;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float moveSpeed = 1;
    //[SerializeField] private Sprite sprite;

    private void Update()
    {
        transform.Translate(moveSpeed / 6 * Vector2.left);
    }
}
