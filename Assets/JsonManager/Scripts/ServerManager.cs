
using System;
using System.Collections;
using JeffreyLanters.WebRequests;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;


public class ServerManager : MonoBehaviour
{
    public TMP_Text test;
    public string Get_URL;
    public string Save_URL;
    
    public string Get_Result_URL;
    public string Save_Result_URL;
    [Space]
    public string FetchedValue;

    public bool Received;
    public bool ReceivedResult;
    public string ReceivedData;
    public string ReceivedResultData;
    public static ServerManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject SavingScreen;
    public GameObject FetchingScreen;

    // Call Whenever Data Is Needed. Stored in FetchedValue variable. Cann add event/callback here.
    public void FetchData(Action SuccessCallback,Action FailedCallback)
    {
        StartCoroutine(Fetch(SuccessCallback,FailedCallback));
        FetchingScreen.SetActive(true);
    }


    //Call along with the Json string variable
    public void SaveData(string data,Action SuccessCallback,Action FailedCallback)
    {
        Debug.Log(data);
        StartCoroutine(Save(data,SuccessCallback,FailedCallback));
        SavingScreen.SetActive(true);
    }


    private IEnumerator Fetch(Action SuccessCallback,Action FailedCallback)
    {
        var uwr = UnityWebRequest.Get(Get_URL);
        yield return uwr.SendWebRequest();

        if (uwr.result==UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            FailedCallback?.Invoke();
            FetchingScreen.SetActive(false);
            Received = true;
        }
        else
        {
            FetchedValue =  uwr.downloadHandler.text; // Decide what to do with the fetched data here.
//            Debug.Log(FetchedValue);
            Received = true;
            test.text = FetchedValue;
            ReceivedData = FetchedValue;
            SuccessCallback.Invoke();
            FetchingScreen.SetActive(false);
        }
    }

    private IEnumerator Save(string value,Action SuccessCallback,Action FailedCallback)
    {
        Debug.Log(value);
        var URL = Save_URL + "?Data=" + value;
        var uwr = UnityWebRequest.Get(URL);
        yield return uwr.SendWebRequest();

        if (uwr.result==UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            FailedCallback?.Invoke();
            SavingScreen.SetActive(false);
        }
        else
        {
            Debug.Log("Uploaded: " + uwr.downloadHandler.text); // Decide what to do after saving data here.
            SuccessCallback?.Invoke();
            SavingScreen.SetActive(false);
            //  FetchData();
        }
        
        // var request = new WebRequest ("http://socialslate.in/SetData.php") {
        //     method = RequestMethod.Post,
        //     contentType = ContentType.TextPlain,
        //     body = value
        // };
    }
    
    public void FetchResultData(Action SuccessCallback,Action FailedCallback)
    {
        StartCoroutine(FetchResult(SuccessCallback,FailedCallback));
        FetchingScreen.SetActive(true);
    }


    //Call along with the Json string variable
    public void SaveResultData(string data,Action SuccessCallback,Action FailedCallback)
    {
        Debug.Log(data);
        StartCoroutine(SaveResult(data,SuccessCallback,FailedCallback));
        SavingScreen.SetActive(true);
    }
    
    private IEnumerator FetchResult(Action SuccessCallback,Action FailedCallback)
    {
        var uwr = UnityWebRequest.Get(Get_Result_URL);
        yield return uwr.SendWebRequest();

        if (uwr.result==UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            FailedCallback?.Invoke();
            FetchingScreen.SetActive(false);
        }
        else
        {
            FetchedValue =  uwr.downloadHandler.text; // Decide what to do with the fetched data here.
            Debug.Log(FetchedValue);
            ReceivedResult = true;
            test.text = FetchedValue;
            ReceivedResultData = FetchedValue;
            SuccessCallback.Invoke();
            FetchingScreen.SetActive(false);
        }
    }

    private IEnumerator SaveResult(string value,Action SuccessCallback,Action FailedCallback)
    {
        var URL = Save_Result_URL + "?Data=" + value;
        var uwr = UnityWebRequest.Get(URL);
        yield return uwr.SendWebRequest();

        if (uwr.result==UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
            FailedCallback?.Invoke();
            SavingScreen.SetActive(false);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text); // Decide what to do after saving data here.
            SuccessCallback?.Invoke();
            SavingScreen.SetActive(false);
            //  FetchData();
        }
    }
}
