using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Creatsoldier : MonoBehaviour
{
    public GameObject soldier;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            
            if(Physics.Raycast(ray,out hit))
            {
                Debug.Log(hit.collider.gameObject.tag);

                if (hit.collider.CompareTag("Groundwork"))
                {
                    hit.collider.gameObject.GetComponent<Groundwork>().SetSoldier(soldier);
                    
                }
            }
        }
    }
}
