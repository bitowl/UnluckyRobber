using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountVictim : MonoBehaviour
{
    public Player Player;
    public MovementController MovementController;

    public HingeJoint MountJoint;
    public Transform MountPoint;

    private Victim _victim;
    private Victim _victimInReach;


    // throw strength
    public Vector3 MinThrowForce;
    public Vector3 MaxThrowForce;
    private bool _beginThrow;
    private float _throwTime;
    public float MaxThrowTime;

    public bool CanThrowSometing
    {
        get { return _victim != null || _victimInReach != null; }
    }

    public bool IsCarrying
    {
        get { return _victim != null; }
    }

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
        if (_beginThrow)
        {
            _throwTime += Time.deltaTime;
        }
        Player.Animator.SetBool("carry", IsCarrying);
    }


    public void ThrowingButtonDown()
    {
        if (_victim != null)
        {
            _beginThrow = true;
            _throwTime = 0;
        }
        else if (_victimInReach != null)
        {
            _beginThrow = false;
            PickUpVictim();
        }

   
    }

    public void ThrowingButtonUp()
    {
        if (_victim != null && _beginThrow)
        {
            _beginThrow = false;
            var factor = Mathf.Min(_throwTime / MaxThrowTime, 1);
            ThrowVictim(factor * (MaxThrowForce - MinThrowForce) + MinThrowForce);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (_victim == null && other.gameObject.layer == 9 && other.GetComponent<Victim>() != null) // victim
        {
            //Debug.Log("victim in reach " + other);
           _victimInReach = other.GetComponent<Victim>();

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_victimInReach != null && other.gameObject == _victimInReach.gameObject)
        {
            //Debug.Log("victim out of reach");
            _victimInReach = null;
        }
    }

    private Rigidbody _ToConnect;

    void FixedUpdate()
    {
        if (_ToConnect != null)
        {
            bool inv = false;
            if (Player.transform.localScale.x < 0) // HACKY HACK HACK: The joint does not behave well, if the player is scaled wrongly
            {
                Player.transform.localScale = new Vector3(-Player.transform.localScale.x, Player.transform.localScale.y, Player.transform.localScale.z);
                inv = true;
            }

            _ToConnect.transform.position = MountPoint.transform.position;
            MountJoint.connectedBody = _ToConnect;
            _ToConnect = null;

            if (inv)
            {
                Player.transform.localScale = new Vector3(-Player.transform.localScale.x, Player.transform.localScale.y, Player.transform.localScale.z);
            }
        }
    }

    private void PickUpVictim()
    {
        Player._soundPlayer.PickUp();
        Debug.Log("pick up victim");
        _victim = _victimInReach;


        _ToConnect = _victim.Hip;
       // MountJoint.connectedBody = _victim.Hip;

        _victim.PickUp(MountPoint);
        // _victim.transform.position = Vector3.zero;
        //   _victim.transform.SetParent(transform, false);
    }

    private void ThrowVictim(Vector3 ThrowForce)
    {
        Player._soundPlayer.Throw();
        Player.Animator.SetTrigger("throw");
//        _victim.transform.SetParent(gameWorld);
        _victim.Throw(new Vector3((MovementController.LookingRight ? 1:-1) * ThrowForce.x, ThrowForce.y, ThrowForce.z), Player);
        MountJoint.connectedBody = null;
        _victim = null;
    }

    public void DropVictim()
    {
        _victim.Drop();
        MountJoint.connectedBody = null;
        _victim = null;
    }
}
