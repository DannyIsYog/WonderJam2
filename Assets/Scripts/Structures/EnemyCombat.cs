using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    [SerializeField] GameEvent onCombatEnded;
    // Start is called before the first frame update

    // enemy stats

    int HP;
    int Armour;
    int Damage;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void enemyDied()
    {
        onCombatEnded.Raise();
        //enemy dies
    }
}
