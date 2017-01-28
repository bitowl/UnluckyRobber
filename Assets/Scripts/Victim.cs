using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victim : MonoBehaviour
{

    public Rigidbody Hip;
    private bool _throw;
    private Vector3 _throwForce;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    void FixedUpdate()
    {
        if (_throw)
        {
            Debug.Log("THROOOOW FORCE " +_throwForce);
            Hip.AddForce(_throwForce);
            _throw = false;
        }
    }

    public void Throw(Vector3 force)
    {
        setLayerRecursively(gameObject, LayerMask.NameToLayer("Victims"));
        _throwForce = force;
        _throw = true;
    }

    public void PickUp(Transform followTransform)
    {
        setLayerRecursively(gameObject, LayerMask.NameToLayer("NoCollision"));
    }

    private void setLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            setLayerRecursively(child.gameObject, newLayer);
        }
    }

}
