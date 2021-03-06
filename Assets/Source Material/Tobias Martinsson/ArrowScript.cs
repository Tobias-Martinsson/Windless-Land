using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public GameObject arrowPrefab;
    public Transform arrowSpawnPosition;
    public float shootForce = 10f;
    public float arrowLifeTime;

    private FMOD.Studio.EventInstance ArrowShot;

    public void shootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPosition.position, arrowSpawnPosition.rotation);

        arrow.GetComponent<Rigidbody>().velocity = arrowSpawnPosition.transform.forward * shootForce;

        ArrowShot = FMODUnity.RuntimeManager.CreateInstance("event:/Objects/ArrowShot");
        ArrowShot.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        ArrowShot.start();
        ArrowShot.release();

        Destroy(arrow, arrowLifeTime);
    }
}
