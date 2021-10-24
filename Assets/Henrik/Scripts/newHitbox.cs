using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Main Author: Henrik Rud�n

public class newHitbox : MonoBehaviour
{
    [SerializeField]
    private float swingTime;
    [SerializeField]
    private int damage ;
    [SerializeField]
    private string target;

    private bool invisibility = false;

    private float deathTimer;


    private void OnTriggerEnter(Collider other)
    {

        if (target == "Player")
        {
            //invisibility = other.gameObject.GetComponent<CharacterController>().GetInvisibility();
        }

        if (other.gameObject.tag == target && invisibility == false)
        {
            Debug.Log("Hit " + target);
            other.GetComponent<HealthScript>().takeDamage(damage);
            Debug.Log("Dealt " + damage + " damage");

            Destroy(this.gameObject);
        }
    }

    private void Awake()
    {
        deathTimer = 0f;
    }


    private void Update()
    {
        DeathTimer(swingTime);
    }


    private void DeathTimer(float seconds)
    {

        deathTimer += Time.deltaTime;

        if (deathTimer >= seconds)
        {

            Destroy(this.gameObject);


        }

    }
}
