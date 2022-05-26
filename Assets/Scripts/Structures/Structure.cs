using UnityEngine;

public class Structure : MonoBehaviour
{
    private StructureObject _structureObject;
    private float _speed;
    private float _startPos;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _startPos = _transform.position.x;
    }

    private void Update()
    {
        _speed += GameManager.instance.HeroMoveSpeed * Time.deltaTime;
        _transform.position = new Vector3(_startPos + _speed, _transform.position.y, _transform.position.z);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Despawner"))
            Destroy(gameObject);
        if (col.CompareTag("Hero"))
            _structureObject.OnHeroCollision.Raise();
    }

    public void SetStructureObject(StructureObject structureObject) => _structureObject = structureObject;
}