using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraYfollow : MonoBehaviour {
	public Transform player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.position = new Vector3 (player.position.x / 2, player.position.y + 1.5f, this.gameObject.transform.position.z);
	}
}
