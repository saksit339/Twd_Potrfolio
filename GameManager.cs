using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public Transform spawPoit;   
    public Transform[] walkTarget;   
    public List<EnermyWave> enermyWave;
    public float currentCoid;
    public float coidPerSecon = 1;


    public float houseHP = 100;
    private bool isHouseDestroyed = false;   
    private int waveIndex = 0;  
    private bool isCoudown = false;
    private EnermyWave currentWave;
    private bool isEnd = false;

    public GameObject[] soldier = new GameObject[4];
    public int soldierSelector = 0;
    private void Start()
    {
        currentWave = enermyWave[waveIndex];
    }
    private void Update()
    {
        

        if (houseHP < 1)
        {
            isHouseDestroyed = true;
        }


        if(isHouseDestroyed)
        {
            Debug.Log("HouseDestroyed");
        }
        else
        {
            SpawnEnermyLogic();
            selectSoldier();
            Creatsoldier();
        }
    }

    void SpawnEnermyLogic()
    {
        if (!isCoudown)
        {
            if (isEnd)
            {
                //Debug.Log("end wave");
            }
            else
            {
                StartCoroutine("SpawnEnermy");
            }

        }
    }

    IEnumerator SpawnEnermy()
    {
        isCoudown=true;

        for (int i = 0; i < currentWave.enermy.Count; i++)
        {
            yield return new WaitForSeconds(2);
            GameObject newEnermy = Instantiate(currentWave.enermy[i],spawPoit.position,Quaternion.identity);
            newEnermy.GetComponent<Enermy>().SetWalkTarget(walkTarget);
        }
        if(waveIndex < enermyWave.Count-1)
        {
            waveIndex++;
        }else
        {
            isEnd = true;
        }
        currentWave = enermyWave[waveIndex];

        //befor next wave, wait10s
        yield return new WaitForSeconds(10);
        isCoudown = false;
    }

    public void HouseTakeDamage(float damage)
    {
        houseHP -= damage;
    }

    void Creatsoldier()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.tag);

                if (hit.collider.CompareTag("Groundwork"))
                {
                    hit.collider.gameObject.GetComponent<Groundwork>().SetSoldier(soldier[soldierSelector]);

                }
            }
        }
    }

    void selectSoldier()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            soldierSelector = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            soldierSelector = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            soldierSelector = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            soldierSelector = 3;
        }       
    }
}


