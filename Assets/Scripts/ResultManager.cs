using System.Collections;
using UnityEngine;

public class ResultManager : MonoBehaviour
{
    public static ResultManager Instance;
    public GameObject resultParent;
    public GameObject resultPanel;
    [SerializeField]
    private CompleteResultData completeResultData = new();

    public string BaseURL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSdF8sXeVGkbKSbww9h-KzjKSvsFEg6AIgLYl6GeNKmcfMx1Iw/formResponse";
    private void Start()
    {
        Instance = this;
    }

    public void saveResultData(ResultData playerData)
    {
        Debug.Log(JsonUtility.ToJson(playerData));
       // StartCoroutine(_saveResultData(playerData));
        StartCoroutine(SaveToGoogleSheet(playerData));
    }

    public IEnumerator SaveToGoogleSheet(ResultData playerData)
    {
        WWWForm wwwForm = new WWWForm();
        wwwForm.AddField("entry.735653671",playerData.PlayerName);
        wwwForm.AddField("entry.203635028",playerData.LoginTime);
        wwwForm.AddField("entry.480133505",playerData.TotalTimePlayed);
        wwwForm.AddField("entry.1517759352",playerData.GameStartDate);
        wwwForm.AddField("entry.1584553486",playerData.GameStartBankBalance);
        wwwForm.AddField("entry.1108390272",playerData.GameStartCompanyValue);
        wwwForm.AddField("entry.1008617325",playerData.GameEndDate);
        wwwForm.AddField("entry.834891881",playerData.GameEndBankBalance);
        wwwForm.AddField("entry.1000546472",playerData.GameEndCompanyValue);
        wwwForm.AddField("entry.1906488817",playerData.EvaluationBankBalance);
        wwwForm.AddField("entry.928446399",playerData.EvaluationFirmValue);

        byte[] raw = wwwForm.data;
        WWW www = new WWW(BaseURL, raw);
        yield return www;
    }
    public IEnumerator _saveResultData(ResultData playerData)
    {
        Debug.Log("1");
        ServerManager.instance.FetchResultData(() => { Debug.Log("Download Success"); },
            () => { Debug.Log("Upload Failed"); });
        while (!ServerManager.instance.ReceivedResult)
        {
            Debug.Log("Waiting for Server Data");
            yield return null;
        }

        Debug.Log("2");

        if (ServerManager.instance.ReceivedResultData == "")
        {
            Debug.Log("3");

            Debug.Log("No Data Yet");
            completeResultData.resultDataList.Add(playerData);

            var s = JsonUtility.ToJson(completeResultData);
            Debug.Log("UPLOAD DATA :" + s);
            ServerManager.instance.SaveResultData(s, () => { Debug.Log("Result Upload Success"); },
                () => { Debug.Log("Result Upload Failed"); });
        }
        else
        {
            Debug.Log("4");

            completeResultData = JsonUtility.FromJson<CompleteResultData>(ServerManager.instance.ReceivedResultData);

            completeResultData.resultDataList.Add(playerData);

            var s = JsonUtility.ToJson(completeResultData);
            Debug.Log("UPLOAD DATA :" + s);
            ServerManager.instance.SaveResultData(s, () => { Debug.Log("Upload Success"); },
                () => { Debug.Log("Upload Failed"); });
        }
    }

    public IEnumerator GetResult()
    {
        ServerManager.instance.FetchResultData(() => { Debug.Log("Results Download Success"); },
            () => { Debug.Log("Results Download Failed"); });
        while (!ServerManager.instance.ReceivedResult)
        {
            Debug.Log("Waiting for Result Data");
            yield return null;
        }

        Debug.Log(ServerManager.instance.ReceivedResultData);
        
        completeResultData = JsonUtility.FromJson<CompleteResultData>(ServerManager.instance.ReceivedResultData);
      
        if (completeResultData.resultDataList.Count > 0)
            foreach (var result in completeResultData.resultDataList)
            {
                var r = Instantiate(resultPanel, resultParent.transform);
                r.GetComponent<ResultPanel>().FillData(result);
            }
        else
            Debug.Log("no result found");
    }

    public void DeleteResult(ResultData result)
    {
        completeResultData.resultDataList.Remove(result);
        var s = JsonUtility.ToJson(completeResultData);
        Debug.Log("UPLOAD DATA :" + s);
        ServerManager.instance.SaveResultData(s, () => { Debug.Log("Upload Success"); },
            () => { Debug.Log("Upload Failed"); });
    }
}