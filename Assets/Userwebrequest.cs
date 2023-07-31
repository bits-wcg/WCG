using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Userwebrequest : MonoBehaviour
{

    void Start()
    {
        StartCoroutine(PostCrt());
    }

    IEnumerator PostCrt()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", "dummyuser1@wilp.bits-pilani.ac.in");
        form.AddField("password", "dummyuser1");

        using (UnityWebRequest www = UnityWebRequest.Post("https://elearn.bits-pilani.ac.in/user/vcredentials/", form))
        {
            www.downloadHandler = new DownloadHandlerBuffer();
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Connected to server");
                
                Debug.Log("Verifying user role");
               
              var n=  www.GetResponseHeaders();
              var role="";
              foreach (var s in n.Select(t => t.ToString()))
              {
                  if (s.Contains("student"))
                  {
                      role = "student";
                  }
                  else    if (s.Contains("staff"))
                  {
                      role = "staff";  
                  }
                  else
                  {
                      role = "Unknown";
                  }
              }

              if (role.Contains("student"))
              {
                  
              }
              else  if (role.Contains("staff"))
              {
                  
              }
              
            }
        }
    }
    
    
}
