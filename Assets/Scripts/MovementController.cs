using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public SoundPlayer SoundPlayer;
    public Transform GroundCheck;
    public float GroundDistance;
    //    public float GroundCheckRadius = 0.15f;
    public LayerMask GroundLayerMask;
    public Animator Animator;


    public float JumpVelocity = 5;
    public float MaxSpeed = 8;
    public float MaxSpeedCarrying = 5;

    private Rigidbody _rigidbody;

    public bool _isGrounded = false;
    private bool _doJump = false;
    private bool _lookingRight = true;
    
    private bool _doubleJumpAvailable;

    private Player _player;

    public float HurtTime;
    private float _hurtTimeLeft = 0;


    public bool LookingRight
    {
        get
        {
            return _lookingRight;
        }
    }

    // Use this for initialization
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((_isGrounded || _doubleJumpAvailable) && !_player.MountVictim.IsCarrying)
        {
            foreach (var JumpButton in _player.Jump)
            {

                if (Input.GetButtonDown(JumpButton))
                {
                    _doJump = true;
                }
            }
        }

    }

    void FixedUpdate()
    {
        // Debug.Log("grounded:" +_isGrounded + " jump: " + _doJump);
        if (GameManager.instance.GameOver || _hurtTimeLeft > 0 || _player._respawnTimeLeft > 0)
        {
            if (_hurtTimeLeft > 0)
            {
                _hurtTimeLeft -= Time.deltaTime;
                if (_hurtTimeLeft < 0)
                {
                    _hurtTimeLeft = 0;
                }
            }
            return; // No player updates 
        }
        // check if is grounded
        //_isGrounded = Physics.OverlapSphere(GroundCheck.position, GroundCheckRadius, GroundLayerMask);
        _isGrounded = Physics.Raycast(GroundCheck.position, -Vector3.up, GroundDistance + 0.1f);
        if (_isGrounded)
        {
            _doubleJumpAvailable = true;
        }

        float horizontal = Input.GetAxis(_player.Horizontal);

        var speed = _player.MountVictim.IsCarrying ? MaxSpeedCarrying : MaxSpeed;

        _rigidbody.velocity = new Vector3(horizontal * speed, _rigidbody.velocity.y, _rigidbody.velocity.z);

        if ((horizontal > 0 && !_lookingRight) || (horizontal < 0 && _lookingRight))
        {
            Flip();
        }

        if (_doJump)
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, JumpVelocity, _rigidbody.velocity.z);
            if (!_isGrounded)
            {
                _doubleJumpAvailable = false;
                SoundPlayer.DoubleJump();
            }
            else
            {
                SoundPlayer.Jump();

            }
            _doJump = false;
        }


        Animator.SetFloat("speed", Mathf.Abs(horizontal));
        Animator.SetFloat("ySpeed", _rigidbody.velocity.y);
        Animator.SetBool("grounded", _isGrounded);
    }

    public void Flip()
    {
        _lookingRight = !_lookingRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    public void HurtFromPunch()
    {
        _hurtTimeLeft = HurtTime;
    }
}
