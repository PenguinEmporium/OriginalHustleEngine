using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacter : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Interactable>(out Interactable interactee))
        {
            interactee.Interact();
        }
    }
}
