using System;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;
    
    public PlayerData _PlayerData=new PlayerData();
    public ResultData _resultData=new ResultData();

    private void Awake()
    {
        Instance = this;
       
    }

    public void onGameComplete (string gameResult)
    {
        TimeSpan playTime = DateTime.Now - GameManager.Instance.  startTime;  
     
        Debug.Log("Saving Result");
        Debug.Log("Total Time player "+playTime);
        
  //      Debug.Log(JsonUtility.ToJson(_PlayerData));
        _resultData.PlayerName = PlayerPrefs.GetString("username", "DefaultPlayer");
        
        _resultData.Result = gameResult;

        _resultData.TotalTimePlayed = playTime.ToString();
        
        _resultData.LoginTime = Timer.Instance.LoginTime;

        _resultData.GameStartDate = Timer.Instance.startDate;
        _resultData.GameEndDate = Timer.Instance.currentGameDate;

        _resultData.GameStartBankBalance = GameManager.Instance.gameParameters.InitialBankBalance.ToString();
        _resultData.GameEndBankBalance = GameManager.Instance.gameParameters.AmountInBank.ToString();
        _resultData.GameStartCompanyValue = GameManager.Instance.startValueOfTheFirm.ToString();
        _resultData.GameEndCompanyValue = GameManager.Instance.gameParameters.ValueOfTheFirm.ToString();

        _resultData.valueOfFirm = GameManager.Instance.gameParameters.ValueOfTheFirm;

        ResultManager.Instance.saveResultData(_resultData);

        Debug.Log("Saving Result Complete");
    }
}
