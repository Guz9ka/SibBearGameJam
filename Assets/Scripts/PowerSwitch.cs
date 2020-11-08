using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSwitch : MonoBehaviour
{
    public bool isTurnedOff;

    public SpriteRenderer switchSpriteRenderer;
    public Sprite switchOnSprite;
    public Sprite switchOffSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collider = collision.GetComponent<Collider2D>();

        if (collider.CompareTag("Wrist") && _MinigamesState.singleton.standState != ElecricityStandState.Closed )
        {
            SwitchSwitch();
        }
    }

    public void SwitchSwitch()
    {
        isTurnedOff = !isTurnedOff;
        Debug.Log("switch");
        _MinigamesState.singleton.switchSwitcher(isTurnedOff);

        ChangeVisual();
        if (isTurnedOff) { SoundsPlayer.singleton.PlaySoundSwitchOffSwitcher(); }
        else { SoundsPlayer.singleton.PlaySoundSwitchOnSwitcher(); }
    }

    void ChangeVisual()
    {
        switch (isTurnedOff)
        {
            case true:
                switchSpriteRenderer.sprite = switchOffSprite;
                break;
            case false:
                switchSpriteRenderer.sprite = switchOnSprite;
                break;
        }
    }
}
