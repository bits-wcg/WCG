using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class ProfitLossManager : SerializedMonoBehaviour
{
    public static ProfitLossManager Instance;

    public GameObject PLPrefab;

    public TMP_Dropdown Dropdown;
    public TMP_Dropdown DropdownYear;

    private void Awake()
    {
        Instance = this;
    }

    public void CalculatePLSheet()
    {
        var t = new List<Transaction>();

        t = Timer.Instance.month switch
        {
            1 => TransactionManager.Instance.TransactionStatements[Timer.Instance.year].Quarterly1.January,
            2 => TransactionManager.Instance.TransactionStatements[Timer.Instance.year].Quarterly1.Feb,
            3 => TransactionManager.Instance.TransactionStatements[Timer.Instance.year].Quarterly1.March,
            4 => TransactionManager.Instance.TransactionStatements[Timer.Instance.year].Quarterly2.April,
            5 => TransactionManager.Instance.TransactionStatements[Timer.Instance.year].Quarterly2.May,
            6 => TransactionManager.Instance.TransactionStatements[Timer.Instance.year].Quarterly2.June,
            7 => TransactionManager.Instance.TransactionStatements[Timer.Instance.year].Quarterly3.July,
            8 => TransactionManager.Instance.TransactionStatements[Timer.Instance.year].Quarterly3.August,
            9 => TransactionManager.Instance.TransactionStatements[Timer.Instance.year].Quarterly3.Sep,
            10 => TransactionManager.Instance.TransactionStatements[Timer.Instance.year].Quarterly4.October,
            11 => TransactionManager.Instance.TransactionStatements[Timer.Instance.year].Quarterly4.November,
            12 => TransactionManager.Instance.TransactionStatements[Timer.Instance.year].Quarterly4.December,
            _ => t
        };

        var profitLossSheet = new ProfitLossSheet();
        float amountOf_FG_Sold = 0;
        // var expense = 0;
        var revenue = 0;
        foreach (var transaction in t)
        {
            Debug.Log("Going Through Transactions");
            switch (transaction.Type)
            {
                case Transaction.TransactionType.Credit:
                    amountOf_FG_Sold += transaction.quantity;
                    revenue += transaction.TransactionAmount;
                    break;
                case Transaction.TransactionType.Debit:
                    // expense += transaction.TransactionAmount;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        Debug.Log("Found Sales "+amountOf_FG_Sold);
        var g = GameManager.Instance.gameParameters;

        var COGS = amountOf_FG_Sold * g.FixedFinishedGoodPrice;

        profitLossSheet.Revenue = revenue;
        profitLossSheet.CostOfGoodsSold = (int) COGS;
        var difference = g.FixedFinishedGoodPrice-g.InitialRawMaterialPrice;
        difference *= g.UnitsProducedPerCycle * g.CyclesPerMonth;
        
        profitLossSheet.ChangeInTheValueOfTheInventory = difference;
        var grossProfit = (int) (profitLossSheet.Revenue - COGS + difference);
        profitLossSheet.GrossProfit = grossProfit;
        var productionExpense = (int) g.ProductionCostPerCycle * g.CyclesPerMonth;
        profitLossSheet.ProductionExpense = productionExpense;
        profitLossSheet.OperatingCost = (int) g.FixedOperatingCharge;
        var Dep = (int) (g.CostOfTheFactory * (g.Depreciation / 100) / 12);
        profitLossSheet.Depreciation = Dep;
        var interestExpenses = (int) (((g.CostOfTheFactory * g.Initial_DE_Ratio / 100) * g.InterestRateOnLoan / 100) / 12);
        profitLossSheet.interestExpenses = interestExpenses;

        var PBT = (int) (grossProfit - interestExpenses - Dep - productionExpense -
                         g.FixedOperatingCharge);
        profitLossSheet.ProfitBeforeTax =PBT;
        profitLossSheet.ProfitAfterTax = (int) (PBT - (PBT * g.TaxRate / 100));
        GameManager.Instance.gameParameters.CostOfTheFactory -= Dep;

        Debug.Log("Completed PL Calculations" +profitLossSheet.CostOfGoodsSold);
        var date = new DateTime(Timer.Instance.year, Timer.Instance.month, Timer.Instance.date-1).ToString("MMMM");


        if (PlayerDataManager.Instance._PlayerData.ProfitLossStatement_V.ContainsKey(Timer.Instance.year))
        {
            if (PlayerDataManager.Instance._PlayerData.ProfitLossStatement_V[Timer.Instance.year].ProfitLostMonthStatement.ContainsKey(Timer.Instance.month))
            {
                PlayerDataManager.Instance._PlayerData.ProfitLossStatement_V[Timer.Instance.year].ProfitLostMonthStatement[Timer.Instance.month] =
                    profitLossSheet;
            }
            else
                PlayerDataManager.Instance._PlayerData.ProfitLossStatement_V[Timer.Instance.year].ProfitLostMonthStatement
                    .Add(Timer.Instance.month, profitLossSheet);
        }
        else
        {
            var m = new MonthStatement_V()
            {
                ProfitLostMonthStatement = new Dictionary<int, ProfitLossSheet>
                    {{Timer.Instance.month, profitLossSheet}}
            };
            OptionDataListYear.Add(Timer.Instance.year.ToString());

            DropdownYear.ClearOptions();
            DropdownYear.AddOptions(OptionDataListYear);

            Dropdown.value = OptionDataList.Count - 1;

            PlayerDataManager.Instance._PlayerData.ProfitLossStatement_V.Add(Timer.Instance.year, m);
        }

        if (OptionDataList.Count < 12)
        {
            OptionDataList.Add(date);
            Dropdown.ClearOptions();
            Dropdown.AddOptions(OptionDataList);
        }

        Dropdown.value = Timer.Instance.month - 1;
        PLPrefab.SetActive(true);
        openSheet(Dropdown.value);
    }

    // public void NewCal()
    // {
    //     foreach (KeyValuePair<int, TransactionStatement> tt in TransactionManager.Instance.TransactionStatements)
    //     {
    //         var t = new List<Transaction>();
    //
    //         var m = tt.Value;
    //         for (int i = 1; i <= 12; i++)
    //         {
    //             t = i switch
    //             {
    //                 1 => m.Quarterly1.January,
    //                 2 => m.Quarterly1.Feb,
    //                 3 => m.Quarterly1.March,
    //                 4 => m.Quarterly2.April,
    //                 5 => m.Quarterly2.May,
    //                 6 => m.Quarterly2.June,
    //                 7 => m.Quarterly3.July,
    //                 8 => m.Quarterly3.August,
    //                 9 => m.Quarterly3.Sep,
    //                 10 => m.Quarterly4.October,
    //                 11 => m.Quarterly4.November,
    //                 12 => m.Quarterly4.December,
    //                 _ => t
    //             };
    //             var b = new ProfitLossSheet();
    //             foreach (var transaction in t)
    //             {
    //                 switch (transaction.Type)
    //                 {
    //                     case Transaction.TransactionType.Credit:
    //                         b.Revenue += transaction.TransactionAmount;
    //                         break;
    //                     case Transaction.TransactionType.Debit:
    //                         b.ProductionExpense += transaction.TransactionAmount;
    //                         break;
    //                     default:
    //                         throw new ArgumentOutOfRangeException();
    //                 }
    //             }
    //
    //             b.ProfitBeforeTax = b.Revenue - b.ProductionExpense;
    //             var date = new DateTime(Timer.Instance.year, Timer.Instance.month, Timer.Instance.date)
    //                 .ToString("MMMM");
    //         }
    //     }
    // }

    public int sheetYear = 2022;
    public int sheetYearF = 2022;

    public void SheetYearDD(int n)
    {
        Debug.Log("Received Valur:" + n);
        var nn = sheetYear;
        nn += n;
        sheetYearF = nn;
        Dropdown.value = 0;
        openSheet(0);
        Debug.Log("Sheet Year" + sheetYearF);
    }

    public void openSheet(int n)
    {
        if (PlayerDataManager.Instance._PlayerData.ProfitLossStatement_V[sheetYearF].ProfitLostMonthStatement.ContainsKey(n + 1))
        {
            PLPrefab.SetActive(true);

            Debug.Log("Loading new data " + n);
            if (currentBalanceSheetPrefab_V == null)
            {
                currentBalanceSheetPrefab_V = PLPrefab;
                currentBalanceSheetPrefab_V.GetComponent<ProfitLossPanel>()
                    .printSheet(PlayerDataManager.Instance._PlayerData.ProfitLossStatement_V[sheetYearF].ProfitLostMonthStatement[n + 1]);
            }
            else
            {
                currentBalanceSheetPrefab_V.GetComponent<ProfitLossPanel>()
                    .printSheet(PlayerDataManager.Instance._PlayerData.ProfitLossStatement_V[sheetYearF].ProfitLostMonthStatement[n + 1], sheetYearF, n + 1);
            }
        }
    }


    public GameObject currentBalanceSheetPrefab_V;

   


    public List<string> OptionDataList;
    public List<string> OptionDataListYear;
}