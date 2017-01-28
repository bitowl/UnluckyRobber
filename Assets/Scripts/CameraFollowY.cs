using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowY : MonoBehaviour {
	public Transform Player;
    public Vector3 Offset;
    public float Damping = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 target = new Vector3(Player.position.x, Player.position.y, this.gameObject.transform.position.z) + Offset;

	    gameObject.transform.position = Smooth(gameObject.transform.position, target, Damping * Time.deltaTime);

		//this.gameObject.transform.position = new Vector3 (Player.position.x / 2, Player.position.y, this.gameObject.transform.position.z) + Offset;
	}

    private Vector3 Smooth(Vector3 position, Vector3 target, float t)
    {
        return new Vector3(
            Mathf.Lerp(position.x, target.x, t),
            Mathf.Lerp(position.y, target.y, t),
            Mathf.Lerp(position.z, target.z, t)
        );
    }
}
