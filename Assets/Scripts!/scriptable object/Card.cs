using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject {
    public int cardCost;     //how much coins it takes to deploy it
    public int cardLevel;    //defines how strong/good the card is

    public int Slot;
    public CardTypes Type;
}
