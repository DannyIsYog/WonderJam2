using UnityEngine;

public class SpawnNumberPopup : MonoBehaviour
{
    [SerializeField] private GameObject numberPopupPrefab;
    [SerializeField] private Color textColor;
    [SerializeField] private float fontSize = 1;

    public void Spawn(string text)
    {
        NumberPopup numberPopup = Instantiate(numberPopupPrefab, transform.position, Quaternion.identity)
            .GetComponent<NumberPopup>();
        numberPopup.SetText(text, fontSize, textColor);
    }
}