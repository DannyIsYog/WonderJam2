using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

public class DragDropCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameManager _gameManager;
    private StructureObject _structureObj;
    private RectTransform _rectTransform;
    private Vector3 _startPos, _offset;
    private bool _isDragging, _isHoverDeleteBtn, _isHoverDeployZone, _isCardMoving;

    private void Awake()
    {
        _gameManager = GameManager.instance;
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Init(StructureObject structureObj, Vector3 position)
    {
        _structureObj = structureObj;
        GetComponent<Image>().sprite = structureObj.cardSprite;
        transform.position = position;
        _startPos = position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, Input.mousePosition,
                eventData.enterEventCamera, out Vector3 worldPos))
            _offset = transform.position - worldPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = _offset + Input.mousePosition;
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
        else transform.position = _startPos;
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