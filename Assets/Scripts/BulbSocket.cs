using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulbsocketState
{
    Broken,
    Empty,
    Repaired
}

public class BulbSocket : MonoBehaviour
{
    [Header("Параметры патрона лампочки")]
    public BulbsocketState socketState;
    public int socketId;
    public float secondsToScrew;

    [Header("Визуальное изменение спрайта")]
    public SpriteRenderer bulbSpriteRenderer;
    public Sprite brokenSprite;
    public Sprite emptySprite = null;
    public Sprite repairedSprite;

    private void Start()
    {
        ChangeBulbVisual();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var collider = collision.GetComponent<Collider2D>();

        if (collider.CompareTag("Wrist"))
        {
            if(socketState == BulbsocketState.Broken)
            {
                StartCoroutine(UnscrewBulb());
            }
            else if(socketState == BulbsocketState.Empty)
            {
                StartCoroutine(ScrewInBulb());
            }
            else if(socketState == BulbsocketState.Repaired)
            {
                BreakBulb();
            }
        }
    }

    private IEnumerator UnscrewBulb()
    {
        SoundsPlayer.singleton.PlaySoundBulbScrewIn();
        yield return new WaitForSeconds(secondsToScrew);
        socketState = BulbsocketState.Empty;
        ChangeBulbVisual();
    }

    private IEnumerator ScrewInBulb()
    {
        SoundsPlayer.singleton.PlaySoundBulbScrewIn();
        yield return new WaitForSeconds(secondsToScrew);
        socketState = BulbsocketState.Repaired;
        ChangeBulbVisual();
        _MinigamesState.singleton.OnBulbRepaired(socketId, socketState);
    }

    public void BreakBulb()
    {
        SoundsPlayer.singleton.PlaySoundBurp();
        SoundsPlayer.singleton.PlaySoundBreakBulb();
        socketState = BulbsocketState.Broken;
        ChangeBulbVisual();
    }

    void ChangeBulbVisual()
    {
        switch (socketState)
        {
            case BulbsocketState.Broken:
                bulbSpriteRenderer.sprite = brokenSprite;
                break;
            case BulbsocketState.Empty:
                bulbSpriteRenderer.sprite = emptySprite;
                break;
            case BulbsocketState.Repaired:
                bulbSpriteRenderer.sprite = repairedSprite;
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var collider = collision.GetComponent<Collider2D>();

        if (collider.CompareTag("Wrist"))
        {
            StopAllCoroutines();
            SoundsPlayer.singleton.StopSoundBulbScrewIn();
        }
    }
}
