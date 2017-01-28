using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller2 : MonoBehaviour {
	public float maxspeed = 8;
	public float jumpforce = 550;
	public Transform groundCheck;
	public LayerMask whatIsGround;
	public Animator anim;

	[HideInInspector]
	public bool lookingRight = true;

	private Rigidbody2D rb2d;
	private bool isGrounded = false;
	private bool jump = false;




	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		//anim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Jump") && isGrounded)
			jump = true;
	}

	void FixedUpdate () {
		float hor = Input.GetAxis ("Horizontal");
		rb2d.velocity = new Vector2 (hor * maxspeed, rb2d.velocity.y);

		isGrounded = Physics2D.OverlapCircle (groundCheck.position,0.15f,whatIsGround);

		if((hor > 0 && !lookingRight)||(hor < 0 && lookingRight))
			Flip();

		if (jump) {

			rb2d.AddForce (new Vector2(0,jumpforce));
			jump = false;

		}
		if(hor < 0)
		anim.SetFloat ("speed",hor * -1);

		if (hor > 0)
			anim.SetFloat ("speed", hor);

		anim.SetFloat ("ySpeed", rb2d.velocity.y);
		anim.SetBool ("grounded", isGrounded);

	}

	public void Flip(){

		lookingRight = !lookingRight;
		Vector3 myScale = transform.localScale;
		myScale.x *= -1;
		transform.localScale = myScale;
	}
}
