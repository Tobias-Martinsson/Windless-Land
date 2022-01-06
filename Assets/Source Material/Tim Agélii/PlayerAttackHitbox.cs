﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Main Author: Henrik Rud�n
//secondary Author: Tim Ag�lii
public class PlayerAttackHitbox : MonoBehaviour
{
    [SerializeField]
    private GameObject hitboxObject;
    [SerializeField]
    private float swingTime;
    [SerializeField]
    private int damage;
    [SerializeField]
    private string target;
    [SerializeField]
    private int manaPerHit;

    [SerializeField] private string Barrel;


    private float deathTimer;

    private Collider hitboxCollider;

    private void Start()
    {
        hitboxCollider = GetComponent<Collider>();
        Debug.Log(damage);
    }
    private void OnTriggerEnter(Collider other)
    {

       


        

        if (other.gameObject.tag == target)
        {
            other.GetComponent<EnemyHealthScript>().takeDamage(damage);
           
            GetComponentInParent<CharacterController>().ManaIncreased(manaPerHit);
            Debug.Log("gained " + manaPerHit);

        }

        if(other.gameObject.tag == Barrel)
        {
            Debug.Log("ASDASD");
            other.GetComponent<DestroyBarrels>().destroyBarrel();
        }

        

    }

    private void Awake()
    {
        deathTimer = 0f;
        updateDamage();
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
            if(hitboxObject != null)
            {
                hitboxObject.SetActive(false);
            }
           
            gameObject.transform.parent = null;
        }

        if(deathTimer >= seconds * 6)
        {
            Destroy(gameObject);
        }

    }


    public void setConfig(int newmanaPerHit)
    {
        manaPerHit = newmanaPerHit;
    }

    public int getManaPerHit()
    {
        return manaPerHit;
    }

    public void setDamage(int newDamage)
    {
            damage = newDamage;
    }

    public void updateDamage()
    {


        INIParser ini = new INIParser();
        ini.Open(Application.persistentDataPath + "ProtoConfig.ini");
        damage = ini.ReadValue("Henrik", "moreDamage;", 1);
        ini.Close();


        //autoAim = x;
    }


}
