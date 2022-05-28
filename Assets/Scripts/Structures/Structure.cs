using UnityEngine;

public class Structure : MonoBehaviour
{
    [SerializeField] private HeroData heroData;

    private float _speed;
    private float _startPos;
    private Transform _transform;

    public StructureObject StructureObject { get; set; }

    private void Awake()
    {
        _transform = transform;
        _startPos = _transform.position.x;
    }

    private void Update()
    {
        _speed += heroData.moveSpeed * Time.deltaTime;
        _transform.position = new Vector3(_startPos + _speed, _transform.position.y, _transform.position.z);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Despawner"))
            Destroy(gameObject);
        if (col.CompareTag("Hero"))
            StructureObject.OnHeroCollision.Raise();
    }
}