using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class PlayerController : MonoBehaviour
{
    public bool playerEnabled = true;
    //Used to force the player type to a set location
    public Vector3 overrideTargetLocation;

    public abstract void EnableDisablePlayer(bool newState, bool cinematicDisable = false);
}
