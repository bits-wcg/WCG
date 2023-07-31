    using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    public TMP_Text playerName;
    public TMP_Text playTime;
    public TMP_Text DateOnGameOver;
    public TMP_Text GameResult;
    public ResultData data;

    public Button fullScreen;

    private void Start()
    {
        fullScreen.onClick.AddListener(OpenMoreDetails);
    }

    private void OpenMoreDetails()
    {
        FullScreenResult.Instance.InitializeResultData(data);
    }
    
    public void FillData(ResultData resultData)
    {
        data = resultData;
        playerName.text = resultData.PlayerName;
        var t = resultData.TotalTimePlayed;
        var m = t.Split('.');
        
        playTime.text =m[0];
        DateOnGameOver.text = resultData.GameEndDate;
        GameResult.text = resultData.Result;
    }

    public void Delete()
    {
        ResultManager.Instance.DeleteResult(data);
        Destroy(this.gameObject);
    }

}
