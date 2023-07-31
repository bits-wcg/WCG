using System;
using System.Collections;
using System.Collections.Generic;
using JeffreyLanters.WebRequests;
using UnityEngine;
[AddComponentMenu ("Web Request/Tests/Web Request Tests")]
public class WebTest : MonoBehaviour
{
   
  [System.Serializable]
        private class User {
            public string id;
            public string name;
            public string email;
        }

        private void Awake()
        {
            var request = new WebRequest ("http://socialslate.in/SetData.php") {
                method = RequestMethod.Post,
                body = "Hello, World!"
            };
        }

        public async void Start () {
            try {
                
             
               var request = new WebRequest ("http://socialslate.in/GetData.php");
               var response = await request.Send ();
               var text = response.Text ();
                     Debug.Log(text);
            } catch (WebRequestException exception) {
                Debug.Log ($"Error while getting data from {exception.url}, error {exception.httpStatus}");
            }
        }
    }

