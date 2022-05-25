using UnityEngine;
using UnityEngine.Pool;

public class Structure : MonoBehaviour
{
    [SerializeField] private GameEvent OnHeroCollision;

    private float _speed;
    private float _startPos;
    private Transform _transform;
    private ObjectPool<Structure> _pool;

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
            _pool.Release(this);
        if (col.CompareTag("Hero")) 
            OnHeroCollision.Raise();
    }

    public void SetPool(ObjectPool<Structure> pool)
    {
        _pool = pool;
    }

    public void SetStartPos(Vector3 pos)
    {
        _transform.position = pos;
        _speed = 0;
        _startPos = pos.x;
    }
}