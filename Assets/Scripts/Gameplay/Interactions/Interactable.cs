using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    [SerializeField] bool canInteract = true;
    //Use this to manually assign something
    [SerializeField] UnityEvent onInteractDirect;
    //Use this in other components on the same object to automatically assign listeners
    [SerializeField] public Action onInteract;

    public virtual bool CanInteract()
    {
        return canInteract;
    }

    public virtual void Interact()
    {
        if (!canInteract)
            return;

        Debug.Log("Interacting with " + gameObject.name);

        onInteract?.Invoke();

        onInteractDirect?.Invoke();
    }


}
