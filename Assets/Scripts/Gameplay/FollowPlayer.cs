using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Change this to following a target to enable overriding control easily
public class FollowPlayer : PlayerController
{
    [SerializeField] float walkSpeed = 3;
    [SerializeField] float isometricValue = 0.7f;

    Vector2 facingDirection = new Vector2(0,-1);

    [SerializeField] public bool followMouse = true;
    [SerializeField] private Vector3 mousePositionReadout = Vector3.zero;

    [SerializeField] Animator playerAnim;
    Rigidbody2D rb2D;
    [SerializeField] ScalableObject scaleComponent;

    // Start is called before the first frame update
    void Start()
    {
        scaleComponent = GetComponentInChildren<ScalableObject>();
        playerAnim = GetComponentInChildren<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        overrideTargetLocation = Vector3.zero;
        //scaleComponent = GetComponent<ScalableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerEnabled && !followMouse)
        {
            rb2D.velocity = Vector3.zero;
            return;
        }
            


        Vector3 worldTarget = Vector3.zero;
        if (followMouse)
        {
            worldTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePositionReadout = worldTarget;
        }
        else
        {
            worldTarget = overrideTargetLocation;
        }

        worldTarget.z = 0;


        var movementDirection = (worldTarget - transform.position);
        Vector3 normalizedDir = movementDirection.normalized;

        if (movementDirection.magnitude > 0.2f)
        {
            normalizedDir = new Vector3(normalizedDir.x, normalizedDir.y * isometricValue * scaleComponent.GetScaleValue());

            rb2D.velocity = normalizedDir * (walkSpeed * scaleComponent.GetScaleValue());
            facingDirection = movementDirection.normalized;

            playerAnim.SetFloat("DirectionY", facingDirection.y);
            playerAnim.SetFloat("DirectionX", facingDirection.x);
        }
        else
        {
            rb2D.velocity = Vector3.zero;
        }
    }

    public override void EnableDisablePlayer(bool newState, bool cinematicDisable = false)
    {
        playerEnabled = newState;
        followMouse = !cinematicDisable;
    }
}
