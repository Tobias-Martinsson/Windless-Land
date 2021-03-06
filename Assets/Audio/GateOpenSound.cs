using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpenSound : MonoBehaviour
{
    private FMOD.Studio.EventInstance GateOpen;

    public void PlayGateSound()
    {
        GateOpen = FMODUnity.RuntimeManager.CreateInstance("event:/Environment/GateOpen");
        GateOpen.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        GateOpen.start();
        GateOpen.release();
    }
}
