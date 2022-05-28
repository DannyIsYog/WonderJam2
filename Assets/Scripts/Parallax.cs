using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxFXSpeed = 1;
    [SerializeField] private HeroData heroData;

    private float _spriteLength;
    private float _startPos;
    private float _speed;
    private Transform _transform;

    private void Awake()
    {
        _spriteLength = GetComponent<SpriteRenderer>().bounds.size.x;
        _transform = transform;
        _startPos = _transform.position.x;
    }

    private void Update()
    {
        _speed += heroData.moveSpeed * parallaxFXSpeed * Time.deltaTime;
        _transform.position = new Vector3(_startPos + Mathf.Repeat(_speed, _spriteLength),
            _transform.position.y, _transform.position.z);
    }
}