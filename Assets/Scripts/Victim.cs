using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Victim : MonoBehaviour
{

    public Rigidbody Hip;
    private bool _throw;
    private Vector3 _throwForce;
    public Player Thrower;

	// Use this for initialization
	void Start () {
		GameManager.instance.RegisterVictim();
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

    public void Throw(Vector3 force, Player thrower)
    {
        setLayerRecursively(gameObject, LayerMask.NameToLayer("Victims"));
        _throwForce = force;
        _throw = true;
        Thrower = thrower;
    }

    public void Drop()
    {
        setLayerRecursively(gameObject, LayerMask.NameToLayer("Victims"));
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
