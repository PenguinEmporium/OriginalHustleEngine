using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneDoor : MonoBehaviour
{
    public int entryNumber = 0;

    float timeBeforeTransition = 1f;
    float timeBeforeRegainControl = 1f;

    public string targetScene;
    public int targetEntry;

    public Transform exitPoint;
    public Transform leavePoint;

    public UnityEvent onArrive;
    public UnityEvent onExit;

    bool doorEntryCheck = true;

    void Awake()
    {
        if(exitPoint == null)
        {
            Debug.LogError("No Exit point found for: " + gameObject);
        }
    }

    public virtual void SetTransitionWaitTime(float newTime)
    {
        timeBeforeTransition = newTime;
    }

    public virtual void SetEntryControlTime(float newTime)
    {
        timeBeforeRegainControl = newTime;
    }


    public virtual void LeftDoor()
    {
        //Handle any visuals for arriving
        onArrive?.Invoke();
    }

    public virtual void EnteredDoor()
    {
        onExit?.Invoke();
        FindObjectOfType<GameManager>().LoadLevel(targetScene,targetEntry);
    }

    public void DisableReEntry()
    {
        doorEntryCheck = false;
    }

    public void EnableReEntry()
    {
        doorEntryCheck = true;
    }

    //Get's a reference to the player to handle animations here
    public float HandleEnter(PlayerController player)
    {
        player.overrideTargetLocation = leavePoint.position;

        return timeBeforeTransition;
    }

    public float HandleExit(PlayerController player)
    {
        player.overrideTargetLocation = exitPoint.position;

        return timeBeforeRegainControl;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && doorEntryCheck)
            EnteredDoor();
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            doorEntryCheck = true;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(exitPoint.position, 0.2f);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(leavePoint.position, 0.2f);
    }
}
