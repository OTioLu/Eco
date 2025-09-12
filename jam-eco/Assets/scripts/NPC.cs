using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject npc;


    void Update()

    {

        if (Input.GetMouseButtonDown(0)) 
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10f; 

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            npc.transform.position = worldPos;
         
            npc.SetActive(true); 
        }

    }
}