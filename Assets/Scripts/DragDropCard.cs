using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

public class DragDropCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler,
    IPointerExitHandler, IPointerUpHandler, IPointerDownHandler
{
    private GameManager _gameManager;
    private Transform _transform;
    private StructureObject _structureObj;
    private RectTransform _rectTransform;
    private Vector3 _startPos, _offset, _startScale;
    private bool _isDragging, _isHoverDeleteBtn, _isHoverDeployZone, _isCardMoving;

    private void Awake()
    {
        _gameManager = GameManager.instance;
        _rectTransform = GetComponent<RectTransform>();
        _transform = transform;
    }

    public void Init(StructureObject structureObj, Vector3 position)
    {
        _structureObj = structureObj;
        GetComponent<Image>().sprite = structureObj.cardSprite;
        _transform.position = position;
        _startPos = position;
        _startScale = _transform.localScale;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, Input.mousePosition,
                eventData.enterEventCamera, out Vector3 worldPos))
            _offset = _transform.position - worldPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _transform.position = _offset + Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_isHoverDeleteBtn)
            Destroy(gameObject);
        else if (_isHoverDeployZone)
        {
            _gameManager.CreateStructure(_structureObj.type);
            Destroy(gameObject);
        }
        else _transform.position = _startPos;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _transform.position += Vector3.up * 20;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _transform.position = _startPos;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _transform.localScale = _startScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _transform.localScale = _startScale * 1.2f;
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.CompareTag("DeleteBtn"))
            _isHoverDeleteBtn = true;
        if (hit.CompareTag("DeployZone"))
            _isHoverDeployZone = true;
    }

    private void OnTriggerExit2D(Collider2D hit)
    {
        if (hit.CompareTag("DeleteBtn"))
            _isHoverDeleteBtn = false;
        if (hit.CompareTag("DeployZone"))
            _isHoverDeployZone = false;
    }
}