using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Groundwork : MonoBehaviour
{
    
    [HideInInspector]
    public GameObject SoldierObject;


    [SerializeField]
    private Transform groundworkPoit;
    // Start is called before the first frame update
    

    public void SetSoldier(GameObject soldier)
    {        
        if(SoldierObject == null)
        {            
            SoldierObject = Instantiate(soldier, groundworkPoit.position, transform.rotation);
        }else
        {
            Destroy(SoldierObject);
            SoldierObject = Instantiate(soldier, groundworkPoit.position, transform.rotation);
        }
    }
}
