using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Enermy : MonoBehaviour
{
    [SerializeField]
    private float maxHelt = 100;   
    [SerializeField]
    private float enermyDamage = 10;
    [SerializeField]
    private ParticleSystem getHitFX;
    [SerializeField]
    private bool flyingEnermy = false;

    private float enermyHelt;
    private Transform[] walkTarget; 
    private int walkTargetIndex = 0;   
    public bool attacked = false;
    private float speed = 2;   
    private bool isDead = false;

    private void Start()
    {
        enermyHelt = maxHelt;       
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead)
        {
            Destroy(gameObject,3);
        }
        else
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            transform.LookAt(walkTarget[walkTargetIndex]);
            if (Vector3.Distance(transform.position, walkTarget[walkTargetIndex].position) <= .2f)
            {
                if (walkTargetIndex == walkTarget.Length - 1)
                {
                    Debug.Log("attack");
                    GameObject.Find("GameManager").GetComponent<GameManager>().HouseTakeDamage(enermyDamage);
                    Destroy(gameObject);
                }
                if (walkTargetIndex < walkTarget.Length - 1)
                {
                    walkTargetIndex++;
                }
            }
        }

        if(enermyHelt < 1)
        {
            isDead = true;
        }
        
    }

    public bool IsDead()
    {
        return isDead;
    }

    public void SetWalkTarget(Transform[] target)
    {
        walkTarget = target;
    }

    public void TakeDamage(float damege)
    {
        enermyHelt -= damege;
        getHitFX.Play();
    }

    public bool IsFlyingEnermy()
    {
        return flyingEnermy;
    }



}
