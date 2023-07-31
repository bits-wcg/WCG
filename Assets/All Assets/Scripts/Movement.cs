using System;
using UnityEngine;


    public class Movement : MonoBehaviour
    {
       
      
        public Vector3 CustomStart;
        public Vector3 CustomEnd;
        public float speed;
        public float timeToReach = 12;
       
        private void Start()
        {
            var transform1 = transform;
            
            transform1.localPosition = CustomStart;
            Debug.Log(transform1.localPosition);
            var d = Vector3.Distance(transform1.localPosition, CustomEnd);

            speed = d / timeToReach;
        }

        private float n;
        private void FixedUpdate()
        {
          
                var d = Vector3.Distance(transform.localPosition, CustomEnd);
                if (d > 0)
                {
                    n += Time.fixedDeltaTime;
                  //  Debug.Log(n);
                }

                if (n <= timeToReach)
                    transform.localPosition =
                        Vector3.MoveTowards(transform.localPosition, CustomEnd, speed * Time.fixedDeltaTime);
            
       
        }
    }
