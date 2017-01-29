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

    public Vector3 InitialPosition;

    public string Throw = "P1 B";

    // Use this for initialization
    void Start()
    {
        _movementController = GetComponent<MovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(Throw))
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
        if (Input.GetButtonUp(Throw))
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
                rb.AddForce(new Vector3((_movementController.LookingRight ? 1 : -1) * PunchForce.x, PunchForce.y,
                    PunchForce.z));
            }
        }
        Debug.Log("punching " + colliders.Length);
    }

    public void Die()
    {

        foreach (var body in gameObject.GetComponentsInChildren<Rigidbody>())
        {
            Debug.Log("kill " + body);
            body.isKinematic = false;
        }

        foreach (var collider in gameObject.GetComponentsInChildren<Collider>())
        {
            collider.enabled = true;
        }
        gameObject.GetComponentInChildren<Animator>().enabled = false;

        // gameObject.transform.position = new Vector3(0, 0, 0);
        if (MountVictim.IsCarrying)
        {
            MountVictim.DropVictim();
        }
        GameManager.instance.GameOver = true;
    }

    public void ResetPlayer()
    {
        transform.position = InitialPosition;

        foreach (var body in gameObject.GetComponentsInChildren<Rigidbody>())
        {
            Debug.Log("kill " + body);
            body.isKinematic = true;
        }

        foreach (var collider in gameObject.GetComponentsInChildren<Collider>())
        {
            collider.enabled = false;
        }
        gameObject.GetComponentInChildren<Animator>().enabled = true;

        // enable everything on the player themself
        foreach (var body in gameObject.GetComponents<Rigidbody>())
        {
            body.isKinematic = false;
        }

        foreach (var collider in gameObject.GetComponents<Collider>())
        {
            collider.enabled = true;
        }

    }
}