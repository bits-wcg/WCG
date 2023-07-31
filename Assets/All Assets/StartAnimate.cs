using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartAnimate : MonoBehaviour
{
    public UnityEvent startKick = new UnityEvent();
    public Vector3 v;
    public bool t;
    public Vector3 v2;
    public float speed;
    void OnEnable()
    {
       // transform.position = new Vector3(v.x, transform.position.y, transform.position.z);
        // var transform1 = transform;
        // var position = transform1.localPosition;
        // position = new Vector3(v.x,position.y,position.z);
        // transform1.position = position;
        //startKick?.Invoke();    
    }

    // Update is called once per frame
    void Update()
    {
        // if (t)
        // {
        //     transform.position = Vector3.MoveTowards(transform.position,
        //         new Vector3(v2.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
        //
        //     Debug.Log(transform.position+" "+ new Vector3(v2.x, transform.position.y, transform.position.z));
        // }

    }
}
