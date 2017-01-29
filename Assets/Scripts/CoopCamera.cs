using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// http://answers.unity3d.com/answers/912763/view.html
public class CoopCamera : MonoBehaviour {


    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    private float MidX;
    private float MidY;
    private float MidZ;
    public Transform target1;
    public Transform target2;
    private Vector3 Midpoint;
    private Vector3 distance;
    private float camDistance;
    private float CamOffset;
    private float bounds;
    public float OffsetY = 1.5f;

    private Camera _camera;
    public float MinDistance = 10f;
    public float MaxDistance = 10f;

    public float MinY = -2;
    public float MinX;
    public float AdditionalDistance = 1;

    // Use this for initialization
    void Start()
    {
        camDistance = 5;
        bounds = 12.0f;
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = target1.position - target2.position;

        if (distance.x < 0)
            distance.x = distance.x * -1;
        if (distance.z < 0)
            distance.z = distance.z * -1;
/*        if (target1.position.x < (transform.position.x - bounds))
        {
            Vector3 pos = target1.position;
            pos.x = transform.position.x - bounds;
            target1.position = pos;
        }
        if (target2.position.x < (transform.position.x - bounds))
        {
            Vector3 pos = target2.position;
            pos.x = transform.position.x - bounds;
            target2.position = pos;
        }
        if (target1.position.x > (transform.position.x + bounds))
        {
            Vector3 pos = target1.position;
            pos.x = transform.position.x + bounds;
            target1.position = pos;
        }
        if (target2.position.x > (transform.position.x + bounds))
        {
            Vector3 pos = target2.position;
            pos.x = transform.position.x + bounds;
            target2.position = pos;
        }*/
        /*  if (distance.x > 15.0f)
          {
              CamOffset = distance.x * 0.3f;
              if (CamOffset >= 8.5f)
                  CamOffset = 8.5f;
          }
          else if (distance.x < 14.0f)
          {
              CamOffset = distance.x * 0.3f;
          }
          else if (distance.z < 14.0f)
          {
              CamOffset = distance.x * 0.3f;
          }*/

        camDistance = Mathf.Sqrt(distance.x*distance.x + distance.y*distance.y) + AdditionalDistance;

        if (camDistance >= MaxDistance)
            camDistance = MaxDistance;
        if (camDistance <= MinDistance)
            camDistance = MinDistance;

        MidX = (target2.position.x + target1.position.x) / 2;
        MidY = (target2.position.y + target1.position.y + OffsetY) / 2;

        if (MidY < camDistance / 2 + MinY)
        {
            MidY = camDistance / 2 + MinY;
        }

        if (MidX < camDistance / 2 + MinX)
        {
            MidX = camDistance / 2 + MinX;
        }


        MidZ = (target2.position.z + target1.position.z) / 2;
        Midpoint = new Vector3(MidX, MidY, MidZ);
        if (target1)
        {
            Vector3 point = _camera.WorldToViewportPoint(Midpoint);
            Vector3 delta = Midpoint - _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, camDistance + CamOffset)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }
}
