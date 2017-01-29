using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour {

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Victim")
        {
            Debug.Log("SCORE!");
            var rat = other.transform.parent.parent.gameObject.AddComponent<RemoveAfterTime>();
            rat.TimeLeft = 10;
//            Destroy(other.transform.parent.parent.gameObject);

            
            GameManager.instance.AddScore(other.GetComponent<Victim>(), 1);
        }
    }
}
