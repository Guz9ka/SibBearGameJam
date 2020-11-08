using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsPlayer : MonoBehaviour
{
    public static SoundsPlayer singleton { get; private set; }

    private void Start()
    {
        singleton = this;
    }

    public AudioSource switcherSound;
    public AudioSource openElectricityStandSound;
    public AudioSource burpSound;
    public AudioSource breakBulbSound;
    public AudioSource plugInWireSound;
    public AudioSource screwInBulbSound;

    public void PlaySoundSwitchSwitcher() 
    {
        switcherSound.Play();
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

    public void PlaySoundPlugInWire()
    {
        plugInWireSound.Play();
    }
}
