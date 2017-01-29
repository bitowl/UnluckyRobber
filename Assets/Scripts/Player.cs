using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MovementController))]
public class Player : MonoBehaviour
{
    protected MovementController _movementController;
    protected internal SoundPlayer _soundPlayer;
    public GameObject Ragdoll;

    public MountVictim MountVictim;
    public Transform PunchPoint;
    public float PunchRadius;
    public LayerMask PunchableLayerMask;
    public Vector3 PunchForce;
    public Animator Animator;

    public Vector3 InitialPosition;

    public string Throw = "P1 B";

    public ParticleSystem[] FireParticles;

    // Use this for initialization
    void Start()
    {
        _movementController = GetComponent<MovementController>();
        _soundPlayer = GetComponent<SoundPlayer>();
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
        _soundPlayer.Punch();
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

    public void Die(string message)
    {
        if (GameManager.instance.GameOver)
        {
            return; // We cannot die twice
        }

        _soundPlayer.Death();
        // TODO: print
        Debug.Log("DEATH BY: " + message);

        foreach (var body in Ragdoll.GetComponentsInChildren<Rigidbody>())
        {
            body.isKinematic = false;
        }

        foreach (var collider in Ragdoll.GetComponentsInChildren<Collider>())
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

        foreach (var body in Ragdoll.GetComponentsInChildren<Rigidbody>())
        {
            body.isKinematic = true;
        }

        foreach (var collider in Ragdoll.GetComponentsInChildren<Collider>())
        {
            collider.enabled = false;
        }
        gameObject.GetComponentInChildren<Animator>().enabled = true;



    }

    public void Burn()
    {
        _soundPlayer.Hurt();
        foreach (var fireParticle in FireParticles)
        {
            fireParticle.Play();
        }
    }

}