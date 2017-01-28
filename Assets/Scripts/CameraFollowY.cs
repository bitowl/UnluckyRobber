using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowY : MonoBehaviour {
	public Transform Player;
    public Vector3 Offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.position = new Vector3 (Player.position.x / 2, Player.position.y, this.gameObject.transform.position.z) + Offset;
	}
}
