using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UploadBtnManager : MonoBehaviour
{
    public Button uploadButton;

    private void Start()
    {
        uploadButton = GetComponent<Button>();
        uploadButton.onClick.AddListener(Upload);
        InvokeRepeating(nameof(Tester),1,1);
    }

    public void Tester()
    {
        ServerManager.instance.FetchData((() =>
        {
            Debug.Log("Download Success");
           
        }), (() => { Debug.Log("Download Failed"); }));
        while (!ServerManager.instance.Received)
        {
            Debug.Log("Waiting for Server Data Student");
          
        }
        var n = JsonUtility.FromJson<GameParameters>(ServerManager.instance.ReceivedData);
 
        Debug.Log(AdminManager.Instance.AdminParameters == n);
    }

    public void Upload()
    {
        var s = JsonUtility.ToJson(AdminManager.Instance.AdminParameters);
        uploadButton.interactable = false;
        uploadButton.gameObject.GetComponentInChildren<TMP_Text>().text = "Uploading";
        StartCoroutine(Save(s));

    }
    IEnumerator Save(string value)
    {
        var URL = "http://socialslate.in/SetData.php" + "?Data=" + value;
        UnityWebRequest uwr = UnityWebRequest.Get(URL);
        yield return uwr.SendWebRequest();

        if (uwr.result==UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            uploadButton.interactable = false;
            uploadButton.gameObject.GetComponentInChildren<TMP_Text>().text = "Uploaded";
            Debug.Log("Received: " + uwr.downloadHandler.text); // Decide what to do after saving data here.
        }
        




    }
}
