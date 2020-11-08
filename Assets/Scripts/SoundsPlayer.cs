using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsPlayer : MonoBehaviour
{
    public static SoundsPlayer singleton { get; private set; }

    public AudioSource switcherOnSound;
    public AudioSource switcherOffSound;
    public AudioSource openElectricityStandSound;
    public AudioSource burpSound;
    public AudioSource breakBulbSound;
    public AudioSource plugInWireSound;
    public AudioSource screwInBulbSound;

    private void Start()
    {
        singleton = this;
    }

    public void PlaySoundSwitchOnSwitcher() 
    {
        switcherOnSound.Play();
        Debug.Log("switch on");
    }
    public void PlaySoundSwitchOffSwitcher()
    {
        switcherOffSound.Play();
        Debug.Log("switch off");
    }

    public void PlaySoundOpenElectricityStand()
    {
        openElectricityStandSound.Play();
    }

    public void PlaySoundBurp()
    {
        burpSound.Play();
    }

    public void PlaySoundBreakBulb()
    {
        breakBulbSound.Play();
    }

    public void PlaySoundBulbScrewIn()
    {
        screwInBulbSound.Play();
    }

    public void StopSoundBulbScrewIn()
    {
        screwInBulbSound.Stop();
    }

    public void PlaySoundPlugInWire()
    {
        plugInWireSound.Play();
    }
}
