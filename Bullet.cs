using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private ParticleSystem hitFX;

    private float damege;    
    private Vector3 targetAttack;


    void Update()
    {
        transform.LookAt(targetAttack);
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
        Destroy(gameObject,5);
    }

    public void SetBulletAttack(Vector3 target,float rangeDamage)
    {
        targetAttack = target;
        damege = rangeDamage;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enermy"))
        {           
            hitFX.Play();
            other.GetComponent<Enermy>().TakeDamage(damege);
            Destroy(gameObject);
        }
    }


    


}
