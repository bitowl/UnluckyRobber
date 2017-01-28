using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class Player : MonoBehaviour
{
    protected MovementController _movementController;

    public MountVictim MountVictim;
    public Transform PunchPoint;
    public float PunchRadius;
    public LayerMask PunchableLayerMask;
    public Vector3 PunchForce;
    public Animator Animator;

	// Use this for initialization
	void Start ()
	{
	    _movementController = GetComponent<MovementController>();
	}
	
	// Update is called once per frame
	void Update () {
    
        if (Input.GetButtonDown("Fire1"))
        {
            if (MountVictim.CanThrowSometing)
            {
                MountVictim.ThrowingButtonDown();
            }
            else
            {
                Punch();
            }

        }
        if (Input.GetButtonUp("Fire1"))
        {
            if (MountVictim.CanThrowSometing)
            {
                MountVictim.ThrowingButtonUp();
            }
        }

    }

    public void Punch()
    {
        Animator.SetTrigger("attack");
        var colliders = Physics.OverlapSphere(PunchPoint.position, PunchRadius, PunchableLayerMask);
        foreach (var collider in colliders)
        {
            var rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(new Vector3((_movementController.LookingRight ? 1:-1) * PunchForce.x, PunchForce.y, PunchForce.z) );
            }
        }
        Debug.Log("punching " + colliders.Length);
    }

    public void Die()
    {
        gameObject.transform.position = new Vector3(0, 0, 0);
        if (MountVictim.IsCarrying)
        {
            MountVictim.DropVictim();
        }
    }
}
