using TMPro;
using UnityEngine;

 [CreateAssetMenu(fileName = "New Card", menuName = "Objects/Card")]
public class Card : ScriptableObject {
    public int cardCost;
    public int cardLevel;
    public Sprite sprite;
}
