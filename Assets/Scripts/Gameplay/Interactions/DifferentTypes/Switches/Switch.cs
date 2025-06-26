using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : Interactable
{
    public UnityEvent onSwitchOn;
    public UnityEvent onSwitchOff;
    public bool switchOn = false;

    public Sprite onState;
    public Sprite offState;

    SpriteRenderer rend;

    void Awake()
    {
        rend = GetComponent<SpriteRenderer>();

        if(switchOn)
        {
            rend.sprite = onState;
        }
        else
        {
            rend.sprite = offState;
        }
    }

    private void OnEnable()
    {
        base.onInteract += Toggle;
    }

    private void OnDisable()
    {
        base.onInteract -= Toggle;
    }

    void Toggle()
    {
        if (!switchOn)
        {
            SwitchOn();
        }
        else
        {
            SwitchOff();
        }
    }


    public void SwitchOn()
    {
        switchOn = true;
        rend.sprite = onState;
        onSwitchOn?.Invoke();
    }

    public void SwitchOff()
    {
        switchOn = false;
        rend.sprite = offState;
        onSwitchOff?.Invoke();
    }
}
