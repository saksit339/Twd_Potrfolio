using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> seeEnermy;
    [SerializeField]
    private float rotSpeed = 12;
    [SerializeField]
    private float attackRate = 1;
    [SerializeField]
    private bool isRangedAttack = false;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float meleeDamage = 50;
    [SerializeField]
    private float rangeDamage = 50;
    [SerializeField]
    private Transform rangeAttackPosirion;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float price;
    

    private bool attackReady = true;
    private bool isAttack = false;
    // Start is called before the first frame update   
    // Update is called once per frame
    void Update()
    {
        if(animator != null)
        {
            animator.SetBool("IsAttack", isAttack);
        }
        if (seeEnermy.Count > 0)
        {

            //remove enermy who dead out of list(seeEnermy)
            for (int i = 0; i < seeEnermy.Count; i++)
            {
                if (seeEnermy[i].GetComponent<Enermy>().IsDead())
                {                    
                    seeEnermy.Remove(seeEnermy[i]);
                }


                if(!isRangedAttack)
                {
                    if(seeEnermy[i].GetComponent<Enermy>().IsFlyingEnermy() == true && seeEnermy.Any())
                    {
                        seeEnermy.Remove(seeEnermy[i]);
                    }
                }
            }

            //rotate to frist enermy then see
            if(seeEnermy.Any())
            {
                Vector3 fristEnermy = seeEnermy[0].transform.position;
                Vector3 target = fristEnermy - transform.position;
                target.y = 0;
                Quaternion TargetRot = Quaternion.LookRotation(target);
                transform.rotation = Quaternion.Lerp(transform.rotation, TargetRot, rotSpeed * Time.deltaTime);
            }


            //attack frist enermy
            if(attackReady)
            {
                StartCoroutine("Attack");
            }

        }
    }

    IEnumerator Attack()
    {
        attackReady = false;
        isAttack = true;
        yield return new WaitForSeconds(attackRate);

        if (isRangedAttack)
        {
            if (seeEnermy.Any())
            {
                GameObject newBullet = Instantiate(bullet,rangeAttackPosirion.position, transform.rotation);
                newBullet.GetComponent<Bullet>().SetBulletAttack(seeEnermy[0].transform.position,rangeDamage);
            }
            
            
        }
        else
        {
            if(seeEnermy.Count>0)
            {
                seeEnermy[0].GetComponent<Enermy>().TakeDamage(meleeDamage);
            }
            
            
        }
        isAttack = false;
        attackReady = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enermy"))
        {
            seeEnermy.Add(other.gameObject);            
        }
    }
    private void OnTriggerExit(Collider other)
    {       
        if (other.gameObject.CompareTag("Enermy"))
        {
            seeEnermy.Remove(other.gameObject);
        }
    }

    
}
