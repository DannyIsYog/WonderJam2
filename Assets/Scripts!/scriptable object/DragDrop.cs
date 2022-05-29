using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    private bool isDragging;
    private Vector3 startPos;
    int hover = 0;
    int Moved = 0, deploy = 0;
    int n;
    private void Start()
    {
        //managerino = FindObjectOfType<managerino>;
        //n = GameObject.FindObjectOfType<Card>.Slot;
        startPos = transform.position;
    }

    public void OnMouseDown()
    {
        isDragging = true;
    }

    public void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {   
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);

            Moved = 1;
        }  
        else
        {
            if (hover == 1)
            {
                //managerino.disableCS(n);
                Destroy(gameObject);
            }
            else if (deploy == 1)
            {
                if (GetComponent<CardDisplay>().Type == CardTypes.SHOP)
                    GameManager.instance.CreateShop();
                else if (GetComponent<CardDisplay>().Type == CardTypes.HORDE)
                    GameManager.instance.CreateHorde();
                //else if (GetComponent<CardDisplay>().Type == CardTypes.HOSPITAL)
                    //GameManager.instance.CreateHospital();
                //managerino.disableCS(n);
                Destroy(gameObject);
            }
            else if (Moved == 1)
                transform.position = startPos;
            Moved = 0;
        }
    }

    
    private void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.transform.gameObject.name == "deleteButton")
            hover = 1;
        if (hit.transform.gameObject.name == "DeployCard")
            deploy = 1;
    }
    private void OnTriggerExit2D(Collider2D hit)
    {
        if (hit.transform.gameObject.name == "deleteButton")
            hover = 0;
        if (hit.transform.gameObject.name == "DeployCard")
            deploy = 0;        
    }
    
}
