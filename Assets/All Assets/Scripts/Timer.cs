using System;
using Factory;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public static Timer Instance;
    public TMP_Text clockText;
    public TMP_Text GameClock;

    private float RawTime;
    private float ClockHR;
    private float ClockMN;
    public string ClockAMPM = "AM";
    public int ClockSpeedMultiplier = 1;

    public int date = 1;
    public int month = 1;
    public int year = 2022;
    public bool addDay = true;
    public int dayCount;
    public int dayCount1;
    public float timeDelt;
    public bool startTimer;
    
    public string currentGameDate;
    public string startDate;

    public int GameYears;

    public DateTime dateTimeRef;

    public DateTime dob;

    // Start is called before the first frame update
    public Button FastBTN;
    public Button MediumBTN;
    public Button SlowBTN;

    public Color Selected;
    public Color UnSelected;
    
    public string LoginTime;
    
    private void Awake()
    {
        Instance = this;
        dob = new DateTime(year, month, date);
        LoginTime = DateTime.Now.ToString("f");
        startDate = dob.ToString("dd/MM/yyyy");
        dateTimeRef = dob;
        var dateFormats = dob.GetDateTimeFormats('D');
        GameClock.text = dateFormats[1];

#if UNITY_EDITOR
        ClockSpeedMultiplier = 10000;
#endif
    }

    private bool monthStart = true;

    public void SetTimer()
    {
        FastBTN.onClick.AddListener(() =>
        {
            ClockSpeedMultiplier = GameManager.Instance.gameParameters.Fast * 24;
            FastBTN.image.color = Selected;
            MediumBTN.image.color = UnSelected;
            SlowBTN.image.color = UnSelected;
        });
        MediumBTN.onClick.AddListener(() =>
        {
            ClockSpeedMultiplier = GameManager.Instance.gameParameters.Medium * 24;
            FastBTN.image.color = UnSelected;
            MediumBTN.image.color = Selected;
            SlowBTN.image.color = UnSelected;
        });
        SlowBTN.onClick.AddListener(() =>
        {
            ClockSpeedMultiplier = GameManager.Instance.gameParameters.Slow * 24;
            FastBTN.image.color = UnSelected;
            MediumBTN.image.color = UnSelected;
            SlowBTN.image.color = Selected;
        });

        FastBTN.onClick?.Invoke();
    }

    private float mincheck;
    private float daycheck;

    DateTime aDayAfterAMonth;

    public int EvaluationMonth;
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!startTimer || GameManager.Instance.isGameOver)
            return;
        mincheck += Time.deltaTime;

        timeDelt += Time.deltaTime;
        // Debug.Log($"New {date}-{month}-{year}");
        if (date <= DateTime.DaysInMonth(year, month))
            dob = new DateTime(year, month, date);
        dateTimeRef = dob;
        currentGameDate = dob.ToString("dd/MM/yyyy");

        RawTime += Time.fixedDeltaTime * ClockSpeedMultiplier;

        // ReSharper disable once PossibleLossOfFraction
        ClockHR = (int)RawTime / 60;
        ClockMN = (int)RawTime - (int)ClockHR * 60;

        if (RawTime >= 1440)
        {
            RawTime = 0;
        }

        if (RawTime >= 720)
        {
            ClockAMPM = "PM";
            ClockHR -= 12;
            addDay = true;
        }
        else
        {
            ClockAMPM = "AM";
            if (addDay)
            {
                addDay = false;

                //  DateTime aDayAfterAMonth;
                if (!monthStart)
                {
                    var aMonth = new TimeSpan(1, 0, 0, 0);
                    aDayAfterAMonth = dob.Add(aMonth);
                }
                else
                {
                    monthStart = false;

                    aDayAfterAMonth = dob;
                }

                var dateFormats = aDayAfterAMonth.GetDateTimeFormats('D');
//                Debug.Log(dateFormats[1]);


                date++;
                Debug.Log("DayComplete");
                GameManager.Instance.DaysUntilNextCycle -= 1;
                if (timeDelt < 10)
                {
                    dayCount++;
                }
                else
                {
                    dayCount1 = dayCount * 6;
                }

                dateTimeRef = dob;

                var n = DateTime.DaysInMonth(year, month);
                if (GameManager.Instance.DaysUntilNextCycle == 0 && GameManager.Instance.isStarted)
                {
                    GameManager.Instance.DaysUntilNextCycle = GameManager.Instance.CycleDays;
                    GameManager.Instance.CycleCompleted();
                }

                if (date > n)
                {
                    GameManager.Instance.MonthsCycleComplete();
                    if (month < 12)
                    {
                        Debug.Log("Month End Calculating PLSHEET AND BLSHEET" + Timer.Instance.month);
                        FinishedGoodManager.Instance.CalculateValue();
                        ProfitLossManager.Instance.CalculatePLSheet();
                        BalanceSheetManager.Instance.CalculateBalanceSheet();
                        month++;
                        EvaluationMonth++;
                    }
                    else
                    {
                        Debug.Log("Month End Calculating PLSHEET" + Timer.Instance.month);
                        FinishedGoodManager.Instance.CalculateValue();
                        ProfitLossManager.Instance.CalculatePLSheet();
                        BalanceSheetManager.Instance.CalculateBalanceSheet();
                        month = 1;
                        year++;
                        GameYears++;
                        if (GameYears >= GameManager.Instance.gameParameters.GameLength)
                        {
                            GameManager.Instance.GameWin();
                        }
                    }

                    GameManager.Instance.StartMonthsCycle();
                    monthStart = true;
                    date = 1;
                }

                clockText.text = $"{ClockHR}:{ClockMN} {ClockAMPM}";
                //GameClock.text = dateFormats[1];
                string fullMonthName = new DateTime(year, month, date).ToString("MMMM");

                GameClock.text = dateFormats[1];
                if (date == 2)
                {
                    GameClock.text = date + " " + fullMonthName + " " + year;
                }

                if (EvaluationMonth == 6)
                {
                    GameManager.Instance.evaluationBankBalance = GameManager.Instance.gameParameters.AmountInBank.ToString();
                    GameManager.Instance.evaluationFirmValue = GameManager.Instance.gameParameters.ValueOfTheFirm.ToString();
                }
            }
        }

        //  Debug.Log((aDayAfterAMonth.ToString("MMMM dd")));
        if (ClockHR < 1)
        {
            ClockHR = 12;
        }
    }
}