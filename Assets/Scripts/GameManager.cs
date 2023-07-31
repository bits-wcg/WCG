using System;
using System.Collections;
using Factory;
using RawMaterial;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{
    public string dummyData;
    public static GameManager Instance;
    public GameParameters gameParameters;

    public GameObject RawMaterialDialogue;

    public int AvailableSuppliers;

    public DateTime startTime;
    public int startValueOfTheFirm;

    public string evaluationBankBalance="NA";
    public string evaluationFirmValue="NA";

    public string ResultURL = "https://docs.google.com/spreadsheets/d/1wztwZXUz-YrJFW__8iXEKPYg-jS-Z87Pf5nODvnB7m8/edit?usp=sharing";
    private void RunningOutOfRawMaterials()
    {
        RawMaterialDialogue.SetActive(true);
    }

    public void OpenResult()
    {
        Application.OpenURL(ResultURL);
    }

    private void Awake()
    {
        Instance = this;
    }

    private bool downloadFailed;

    public IEnumerator Start()
    {
        //ServerManager.instance.SaveData(dummyData,() => { Debug.Log("Upload Success"); },() => { Debug.Log("Upload Failed"); });

        ServerManager.instance.FetchData((() => { Debug.Log("Download Success"); }),
            (() =>
            {
                Debug.Log("Download Failed");
                downloadFailed = true;
            }));

        while (!ServerManager.instance.Received)
        {
            Debug.Log("Waiting for Server Data");
            yield return null;
        }

        if (ServerManager.instance.ReceivedData == "{}" || downloadFailed)
        {
            Debug.Log("Found no data in server uploading dummy");
            ServerManager.instance.SaveData(dummyData, () => { Debug.Log("Download Success"); },
                () => { Debug.Log("Download Success"); });
            var n = JsonUtility.FromJson<GameParameters>(dummyData);
            gameParameters = n;
        }
        else
        {
            // dummyData = ServerManager.instance.ReceivedData;
            var n = JsonUtility.FromJson<GameParameters>(ServerManager.instance.ReceivedData);
            gameParameters = n;
        }


        Debug.Log(ServerManager.instance.ReceivedData);


        FinishedGoodManager.Instance.AddNewFinishedGoods(0);
#if UNITY_EDITOR
        //  var FRM = 100;
#else
        //    var FRM = Random.Range(gameParameters.RawMaterialMinPrice, gameParameters.RawMaterialMaxPrice);
        //    var FRM = 100;
#endif


        // Debug.Log("FIXED RM COST " + FRM);
        //  gameParameters.FixedRawMaterialPrice = FRM;
        var FFG = Random.Range(gameParameters.FinishedGoodsMinPrice, gameParameters.FinishedGoodsMaxPrice);
        Debug.Log("FIXED FG COST " + FFG);
        gameParameters.FixedFinishedGoodPrice = FFG;
        //var debt=(gameParameters.CostOfTheFactory / 100) * gameParameters.Initial_DE_Ratio;
        gameParameters.Debt = (gameParameters.CostOfTheFactory * gameParameters.Initial_DE_Ratio / 100);
        gameParameters.Equity = 100 - gameParameters.Initial_DE_Ratio;

        UI_Manager.Instance.RefreshSupplierPanel();
        UI_Manager.Instance.RefreshCustomerPanel();
        Timer.Instance.year = gameParameters.startYear;

        ProfitLossManager.Instance.sheetYear = gameParameters.startYear;
        ProfitLossManager.Instance.sheetYearF = gameParameters.startYear;
        Timer.Instance.SetTimer();
    }

    public bool isStarted;

    public void StartFactory()
    {
        startTime = DateTime.Now;

        RawMaterialManager.Instance.AddRawMaterial((int)gameParameters.InitialRawMaterial,
            gameParameters.InitialRawMaterialPrice, Timer.Instance.currentGameDate, "Default John", true);

        Timer.Instance.startTimer = true;


        RawMaterialManager.Instance.RemoveRawMaterial(gameParameters.UnitsProducedPerCycle);
        gameParameters.Equity = gameParameters.AmountInBank +
                                (gameParameters.CostOfTheFactory * (100 - gameParameters.Initial_DE_Ratio) / 100) +
                                gameParameters.TotalRawMaterialValue + gameParameters.TotalFinishedGoodsValue;

        StartMonthsCycle();
        UI_Manager.Instance.TurnOnGameHUD();

        isStarted = true;
       
    }

    private bool monthDone;

    public void MonthsCycleComplete()
    {
        Debug.Log("Cycle Completed " + FactoryManager.Instance.isFactoryOperational);
        monthDone = true;
        Debug.Log(
            $"Month complete: No.of.cycles:{gameParameters.CyclesPerMonth} Good Produced this Month:{gameParameters.UnitsProducedPerCycle * gameParameters.CyclesPerMonth}");
    }

    public void StartMonthsCycle()
    {
        CalculateCycleDays();
        FinancialManager.Instance.MonthlyCharges();
    }

    public int CycleDays;

    public void CalculateCycleDays()
    {
        var n = DateTime.DaysInMonth(Timer.Instance.year, Timer.Instance.month);
        CycleDays = (int)(n / (gameParameters.CyclesPerMonth));
        Debug.Log(CycleDays + "Cycle Days");
        DaysUntilNextCycle = CycleDays;
    }

    public void CycleCompleted()
    {
        Debug.Log("Cycle Completed " + FactoryManager.Instance.isFactoryOperational);

        if (FactoryManager.Instance.isFactoryOperational)
        {
            {
                UI_Manager.Instance.Notification(
                    $"Completed Cycle Produced {gameParameters.UnitsProducedPerCycle} Finished Goods",
                    UI_Manager.NotificationTypes.PaymentDone);
                FinishedGoodManager.Instance.AddNewFinishedGoods(gameParameters.UnitsProducedPerCycle);
                if (monthDone)
                {
                    monthDone = false;
                }
            }
        }

        Debug.Log(
            $"Month complete: No.of.cycles:{gameParameters.CyclesPerMonth} Good Produced this Month:{gameParameters.UnitsProducedPerCycle * gameParameters.CyclesPerMonth}");

        StartNextCycle();
    }

    private void StartNextCycle()
    {
        if (gameParameters.AmountInBank >= gameParameters.ProductionCostPerCycle +
            gameParameters.FixedOperatingCharge / gameParameters.CyclesPerMonth)
        {
            if (gameParameters.AvailableRawMaterial >=
                gameParameters.UnitsProducedPerCycle)
            {
                UI_Manager.Instance.LowWarningRawMaterialPanel.SetActive(false);
                Debug.Log(
                    $"<color=yellow> Currently Available RawMaterial : {gameParameters.AvailableRawMaterial} </color>\n " +
                    $"Required RawMaterial for Month {Timer.Instance.currentGameDate}: {gameParameters.UnitsProducedPerCycle * gameParameters.CyclesPerMonth} ");
            }
            else
            {
                PauseTimer();
             //   Debug.Log($"<color=yellow> Currently Required RawMaterial : {gameParameters.UnitsProducedPerCycle} </color>\n ");
                UI_Manager.Instance.LowRawMaterialWarning($"Low on Raw Material \n <size=20>Required Raw material : {gameParameters.UnitsProducedPerCycle}</size>");
              //  GameOverInv();
                FactoryManager.Instance.isFactoryOperational = false;
              lowRaw=  StartCoroutine(WaitForRawMaterialPurchase());
                return;
            }
        }
        else
        {
            PauseTimer();
            UI_Manager.Instance.Notification("Bankrupt you have not maintained the cash flow",
                UI_Manager.NotificationTypes.lowWarning);
            UI_Manager.Instance.GameOver("You have not maintained the cash flow");
            FactoryManager.Instance.isFactoryOperational = false;
            return;
        }

        FinancialManager.Instance.Debit(
            (int)(gameParameters.ProductionCostPerCycle), "production cycle charges");

        RawMaterialManager.Instance.RemoveRawMaterial(gameParameters.UnitsProducedPerCycle);
        FactoryManager.Instance.isFactoryOperational = true;
    }

    private Coroutine lowRaw;
    private IEnumerator WaitForRawMaterialPurchase()
    {
        float i=0;
        while (gameParameters.AvailableRawMaterial <
               gameParameters.UnitsProducedPerCycle)
        {
            PauseTimer();
           // UI_Manager.Instance.LowWarningRawMaterialPanel.SetActive(true);
            i += 1 * Time.deltaTime;

            if (i > 20)
            {
                StopCoroutine(lowRaw);
                GameOverInv();
                break;
            }
            if (AvailableSuppliers <= 0)
            {
              //  UI_Manager.Instance.Notification("Game Over Failed to maintain the Inventory",UI_Manager.NotificationTypes.lowWarning);
                // UI_Manager.Instance.GameOver("You have not maintained the raw material supply");
                //PlayerDataManager.Instance.onGameComplete("Inventory Failed");
              //  break;
            }

            //  Debug.Log("Waiting for Purchase");
            yield return null;
        }

        ResumeTimer();
        StartNextCycle();
    }


    public void PauseTimer()
    {
        Timer.Instance.startTimer = false;
        UI_Manager.Instance.LowWarningCashPanel.SetActive(false);
    }

    public void ResumeTimer()
    {
        CancelInvoke(nameof(ResumeTimer));
        Timer.Instance.startTimer = true;
        UI_Manager.Instance.LowWarningCashPanel.SetActive(false);
    }

    public void TempResume()
    {
        Invoke(nameof(ResumeTimer), 5f);
    }

    public void MissionYearsCompleted()
    {
        UI_Manager.Instance.GameWin();
    }

    public void StartCoolDownTimer(GameObject _gameObject, float delay)
    {
        StartCoroutine(ActivateAfterATime(_gameObject, delay));
    }

    private IEnumerator ActivateAfterATime(GameObject _gameObject, float day)
    {
        var d = Timer.Instance.dateTimeRef.AddDays(day);

        while (d != Timer.Instance.dateTimeRef)
        {
            var n = (Timer.Instance.dateTimeRef - d).TotalDays;
            //Debug.Log($"Available Again in {n}");
            yield return null;
        }

        _gameObject.SetActive(true);
    }

    public void AddAgain(GameObject _gameObject, float delay, PaymentDueManager paymentDueManager, Supplier supplier)
    {
        StartCoroutine(ActivateAfterATime(_gameObject, delay, paymentDueManager));
    }

    private IEnumerator ActivateAfterATime(GameObject _gameObject, float day, PaymentDueManager paymentDueManager)
    {
        var d = Timer.Instance.dateTimeRef.AddDays(day);
        FinancialManager.Instance.RecordCredit(paymentDueManager);
        ResumeTimer();

        while (d != Timer.Instance.dateTimeRef)
        {
            //Debug.Log(d+" "+" "+Timer.Instance.dateTimeRef+" " + (d == Timer.Instance.dateTimeRef));
            yield return null;
        }

        if (FinancialManager.Instance.UpcomingDueList.Contains(paymentDueManager))
        {
            FinancialManager.Instance.UpcomingDueList.Remove(paymentDueManager);
        }

        UI_Manager.Instance.Notification($"Payments is in Due ",
            UI_Manager.NotificationTypes.PaymentDone);
        FinancialManager.Instance.ActiveDueList.Add(paymentDueManager);

        _gameObject.SetActive(true);
    }


    public TMP_Text Transaction;


    public GameObject paymentDuePrefab;
    public GameObject paymentDue_Parent;


    private static readonly int Add = Animator.StringToHash("Add");
    public int DaysUntilNextCycle = 6;

    private void FixedUpdate()
    {
//       Debug.Log(gameParameters.FixedFinishedGoodPrice);
        if (FinancialManager.Instance.ActiveDueList.Count >= 1)
        {
            PauseTimer();
        }

        if (!wcgComplete)
        {
            if (gameParameters.AmountInBank + gameParameters.AccountReceivable +
                gameParameters.TotalFinishedGoodsValue <
                gameParameters.AccountPayable)
            {
                if (!UI_Manager.Instance.GameOverPanel.activeSelf)
                {
                    GameOver();
                    wcgComplete = true;
                }
            }
        }

        if (!FactoryManager.Instance.isFactoryOperational)
        {
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public bool wcgComplete;
    public bool isGameOver;

    private void GameOver()
    {
        isGameOver = true;
        UI_Manager.Instance.GameOver("Your company has ran out of money");
        PlayerDataManager.Instance.onGameComplete("Bankrupt");
    }
    private void GameOverInv()
    {
        UI_Manager.Instance.GameOver("Inventory not maintained");
        PlayerDataManager.Instance.onGameComplete("Inventory Failed");
    }

    public void GameWin()
    {
        UI_Manager.Instance.GameWin();
        PlayerDataManager.Instance.onGameComplete("Successful");
    }
}