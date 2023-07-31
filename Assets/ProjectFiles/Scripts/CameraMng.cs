using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMng : MonoBehaviour
{
    private float SensiX;
    private float SensiY;

    public int MinDist = -100;
    public int MaxDist = 500;
    public float zoom = 50f;


    public Transform target;
     Vector3  moveDirection;

   

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hits;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //if (!rockTarget)
            //{
            //    if (Physics.Raycast(ray, out hits, 10000))
            //    {
            //        MousePosV3 = new Vector3(hits.point.x, hits.point.y, hits.point.z);
            //    }
            //}

            float distance = Vector3.Distance(target.transform.position, transform.position);
            SensiX = distance * 0.02f;
            SensiY = distance * 0.02f;
        }

        float deltaX = Input.GetAxis("Mouse X") * SensiX;
        float deltaY = Input.GetAxis("Mouse Y") * SensiY;

        if (Input.GetMouseButton(0))
        {
            transform.position += deltaX * transform.right * -1;
            transform.position += deltaY * transform.up * -1;
        }

        moveDirection = new Vector3(0, 0, Input.GetAxis("Mouse ScrollWheel") * zoom);

            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (Mathf.Floor(transform.position.y + moveDirection.y) > MinDist)
                {
                    transform.Translate(moveDirection, Space.Self);
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (Mathf.Floor(transform.position.y + moveDirection.y) < MaxDist)
                {
                    transform.Translate(moveDirection, Space.Self);
                }
            }

        if (Input.GetMouseButton(2))
        {
            transform.RotateAround(target.transform.position, Vector3.up, Input.GetAxis("Mouse X") * 2);
         //   transform.RotateAround(target.transform.position, transform.right * -1, Input.GetAxis("Mouse Y") * 2);
        }
        }
}
