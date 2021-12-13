using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvents : MonoBehaviour
{
    [SerializeField]
    private GameObject heavyAttackHitbox;

    [SerializeField]
    private GameObject lightAttackHitbox;

    [SerializeField]
    private GameObject arrow;

    private int comboNumber;

    private float playerMoveSpeed;

    private bool allowMovement = true;

   


    public void instantiateLightHitbox() {
     
        var newHitbox = Instantiate(lightAttackHitbox, transform.position + (transform.rotation * new Vector3(0, 0.5f, 1.7f)), transform.rotation);

        newHitbox.transform.parent = gameObject.transform;
    }

    public void instantiateHeavyHitbox()
    {

        var newHitbox = Instantiate(heavyAttackHitbox, transform.position + (transform.rotation * new Vector3(0, 0.5f, 1.7f)), transform.rotation);

        newHitbox.transform.parent = gameObject.transform;
    }


    public void InstantiateArrow()
    {
        GameObject arrowPrefab = Instantiate(arrow, transform.position + (transform.rotation * new Vector3(0, 1.5f, 1.5f)), transform.rotation);
    }


    public void SetPlayerMoveSpeed(float f) {
        playerMoveSpeed = f;
    }
    public float GetPlayerMoveSpeed() {
        return playerMoveSpeed;
    }


    public void SetComboNumber(int i)
    {
        comboNumber = i;
    }
    public int GetComboNumber()
    {
        return comboNumber;
    }


    public void SetAllowMovement(bool b)
    {
        allowMovement= b;
    }
    public bool GetAllowMovement()
    {
        return allowMovement;
    }


}
