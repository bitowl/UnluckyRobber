using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charactercontroller : MonoBehaviour {
	public Rigidbody player;
	public float speed = 10;
	public float jumpStrength = 80;
	private float jumpspeed = 0;
	private float XspeedCalc = 0;
	// Use this for initialization
	void Start () {
		player = gameObject.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey ("left")) {
			XspeedCalc = speed * -1;
			playerSpeed ();
		}
		if (Input.GetKeyUp ("left")) {
			XspeedCalc = 0;
			playerSpeed ();
		}
		if (Input.GetKey ("right")) {
			XspeedCalc = speed;
			playerSpeed ();
		}
		if (Input.GetKeyUp ("right")) {
			XspeedCalc = 0;
			playerSpeed ();
		}
		if (Input.GetKeyDown ("space")) {
			jumpspeed = jumpStrength;
			playerSpeed ();
			jumpspeed = 0;
		}


	}

	void playerSpeed(){

		player.velocity = new Vector3 (XspeedCalc,player.velocity.y + jumpspeed,0);


	}
}
