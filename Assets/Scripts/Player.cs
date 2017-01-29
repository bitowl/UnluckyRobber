using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Vector3 PunchForceAgainstPlayers;
    public Animator Animator;

    public Vector3 InitialPosition;

    public string[] Jump = {"P1 A", "P1 Y"};
    public string[] Throw = {"P1 B", "P1 X"};
    public string Horizontal = "P1 Horizontal";

    public ParticleSystem[] FireParticles;

    public float RespawnTime = 2;
    protected internal float _respawnTimeLeft = 0;

    // Use this for initialization
    void Start()
    {
        _movementController = GetComponent<MovementController>();
        _soundPlayer = GetComponent<SoundPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(Input.GetJoystickNames().Length);
        foreach (var ThrowButton in Throw)
        {
            if (Input.GetButtonDown(ThrowButton))
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
            if (Input.GetButtonUp(ThrowButton))
            {
                if (MountVictim.CanThrowSometing)
                {
                    MountVictim.ThrowingButtonUp();
                }
            }
        }

        if (_respawnTimeLeft > 0)
        {
            _respawnTimeLeft -= Time.deltaTime;
            if (_respawnTimeLeft <= 0)
            {
                ResetPlayer();
            }
        }
    }

    public void Punch()
    {
        Animator.SetTrigger("attack");
        _soundPlayer.Punch();
        var colliders = Physics.OverlapSphere(PunchPoint.position, PunchRadius);

        var force = PunchForce;
        foreach (var collider in colliders)
        {
            if (collider.tag == "Player")
            {
                var player = collider.GetComponent<Player>();
                if (player == this)
                {
                    continue;
                }
                if (player._movementController.StillHurt)
                {
                    continue;
                }
                player.HurtFromPunch();
                force = PunchForceAgainstPlayers;
//                Debug.Log("COLLIDER: " + collider);
                //              continue;
            }
            var rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(new Vector3((_movementController.LookingRight ? 1 : -1) * force.x, force.y,
                    force.z));
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

        if (GameManager.instance.Coop)
        {
            _respawnTimeLeft = RespawnTime;
        }
        else
        {
            GameManager.instance.GameOver = true;
        }
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

        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void Burn()
    {
        _soundPlayer.Hurt();
        foreach (var fireParticle in FireParticles)
        {
            fireParticle.Play();
        }
    }


    public void HurtFromPunch()
    {
        if (MountVictim.IsCarrying)
        {
            MountVictim.DropVictim();
        }
        _movementController.HurtFromPunch();
    }
}