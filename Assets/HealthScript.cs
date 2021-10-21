using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
 
    private int health;
    [SerializeField]
    private int Maxhealth;
    [SerializeField]
    private Material material;
    [SerializeField]
    private Material originalMaterial;

    private void Start()
    {
        health = Maxhealth;
    }

    // Update is called once per frame
    void Update()
    {
     
            if (health <= 0)
        {
            //death animation and delay
            Destroy(gameObject);
        }
    }

    //damage handler
    public void takeDamage(int x)
    {
        health -= x;
        StartCoroutine(damagedMaterial()); 
        damagedMaterial();
        
    }

    public void regainHealth(int x) {
        if (Maxhealth - health > 0 && x <= Maxhealth - health) {
            health += x;
        }
        if (Maxhealth - health > 0 && x > Maxhealth - health)
        {
            health = Maxhealth;
        }



        Debug.Log("REGAINED" + x + " HEALTH,  MAX IS NOW " + health);
    }

    private IEnumerator damagedMaterial()
    {
        gameObject.GetComponent<MeshRenderer>().material = material;
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<MeshRenderer>().material = originalMaterial;
    }
}
