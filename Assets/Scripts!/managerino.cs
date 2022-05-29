using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerino : MonoBehaviour
{
    public List<Card> Onfloor = new List<Card>();
    public Transform[] cardSlots;
    public bool[] availablecardSlots;

    public void disableCS(int n)
    {
        availablecardSlots[n] = false;
    }

    public void PickCard()
    {
        if (Onfloor.Count >= 1)
        {
            for ( int i = 0; i< availablecardSlots.Length; i++)
            {
                if (availablecardSlots[i])
                {
                    //cardOnFloor.gameObject.SetActive(true);
                    //cardOnFloor.transform.position = cardSlots[i].position;
                    availablecardSlots[i] = false;
                    //Onfloor.Remove(cardOnFloor);
                    return;
                }
            }
        }
    }
}
