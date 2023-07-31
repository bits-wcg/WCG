using TMPro;
using UnityEngine;

public class FullScreenResult : MonoBehaviour
{
    public static FullScreenResult Instance;
    public TMP_Text PlayerEmail;
    public TMP_Text PlayTime;
    public TMP_Text LoginTime;

    public TMP_Text GameStartDate;
    public TMP_Text GameEndDate;

    public TMP_Text GameStartBankBalance;
    public TMP_Text GameEndBankBalance;

    public TMP_Text GameStartCompanyValue;
    public TMP_Text GameEndCompanyValue;
    public TMP_Text Result;

    public GameObject panel;
    private void Start()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void InitializeResultData(ResultData resultData)
    {
        panel.SetActive(true);
        PlayerEmail.text = resultData.PlayerName;
        LoginTime.text = resultData.LoginTime;
        PlayTime.text = resultData.TotalTimePlayed;
        GameStartDate.text = resultData.GameStartDate;
        GameEndDate.text = resultData.GameEndDate;
        GameStartBankBalance.text = resultData.GameStartBankBalance;
        GameEndBankBalance.text = resultData.GameEndBankBalance;
        GameStartCompanyValue.text = resultData.GameStartCompanyValue;
        GameEndCompanyValue.text = resultData.GameEndCompanyValue;
        Result.text = resultData.Result;
    }
}
