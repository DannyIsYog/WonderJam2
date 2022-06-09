using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class NumberPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private float lifetime = 0.6f;
    [SerializeField] private float minDist = 2f;
    [SerializeField] private float maxDist = 2f;

    private Transform _transform;
    private Vector3 _startPos;
    private Vector3 _targetPos;
    private float _timer;

    private void Awake()
    {
        _transform = transform;
        _transform.LookAt(2 * _transform.position - Camera.main.transform.position);

        float direction = Random.rotation.eulerAngles.z;
        _startPos = _transform.position;
        float dist = Random.Range(minDist, maxDist);
        _targetPos = _startPos + Quaternion.Euler(0, 0, direction) * new Vector3(dist, dist, 0f);
        _transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        float _halfLifetime = lifetime / 2;

        if (_timer > lifetime) Destroy(gameObject);
        else if (_timer > _halfLifetime)
            text.color = Color.Lerp(text.color, Color.clear, (_timer - _halfLifetime) / (lifetime - _halfLifetime));

        _transform.position = Vector3.Lerp(_startPos, _targetPos, Mathf.Sin(_timer / lifetime));
        _transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, Mathf.Sin(_timer / lifetime));
    }

    public void SetText(string txt, float fontSize = 1f, Color color = default)
    {
        text.text = txt;
        text.fontSize = fontSize;
        text.color = color;
    }
}