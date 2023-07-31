using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BalanceSheetCalc : MonoBehaviour
{
    public static BalanceSheetCalc Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void CalculateBalanceSheet(Dictionary<int, MonthStatement> BalanceSheetStatement_V,
        List<string> OptionDataList, List<string> OptionDataListYear, TMP_Dropdown DropdownYear, TMP_Dropdown Dropdown)
    {
        var g = GameManager.Instance.gameParameters;
        // Debug.Log("Balance Sheet "+ g.InventoryValue);
        // Debug.Log("Balance Sheet R"+ GameManager.Instance.gameParameters.InventoryValue);
        // Debug.Log("Balance Sheet R"+ GameManager.Instance.gameParameters.TotalRawMaterialValue);
        // Debug.Log("Balance Sheet R"+ GameManager.Instance.gameParameters.TotalFinishedGoodsValue);
        var plm = PlayerDataManager.Instance._PlayerData.ProfitLossStatement_V[Timer.Instance.year]
            .ProfitLostMonthStatement[Timer.Instance.month];
        var p = plm.ProfitAfterTax;
        var cof = g.Equity + p;
        g.Equity = cof;
        var currentLiabilities = (plm.ProfitBeforeTax - plm.ProfitAfterTax) + g.AccountPayable /*+ g.currentLiability*/;
        g.currentLiability = currentLiabilities;
        var balanceSheet = new BalanceSheet()
        {
            CashAndCashEquivalents = (int) (g.AmountInBank),
            Inventory = (int) (g.TotalRawMaterialValue + g.TotalFinishedGoodsValue),
            AccountsReceivable = (int) g.AccountReceivable,
            FixedAsset = (int) g.CostOfTheFactory,
            TotalCurrentAssets = (int) ((g.AmountInBank + (g.TotalRawMaterialValue + g.TotalFinishedGoodsValue) +
                                         g.AccountReceivable + g.CostOfTheFactory)),
            //Adding to equal the detucted next month processing and production charges charges


            CurrentLiabilities = (int) currentLiabilities,
            Debt = (int) g.Debt,
            Equity = (int) (cof),

            TotalCurrentLiabilities = (int) (currentLiabilities +
                                             g.Debt +
                                             cof)
        };

//         Debug.Log("Balance Sheet RR"+ GameManager.Instance.gameParameters.AmountInBank);
//         Debug.Log("Balance Sheet R  R" + balanceSheet.CashAndCashEquivalents);
//         Debug.Log("Balance Sheet F"+ GameManager.Instance.gameParameters.TotalFinishedGoodsValue);
// Debug.Log("Balance Sheet "+ balanceSheet.Inventory);
        var date = new DateTime(Timer.Instance.year, Timer.Instance.month, Timer.Instance.date-1).ToString("MMMM");

        if (BalanceSheetStatement_V.ContainsKey(Timer.Instance.year)) //Year Present
        {
            if (BalanceSheetStatement_V[Timer.Instance.year].ProfitLostMonthStatement.ContainsKey(Timer.Instance.month))
            {
                BalanceSheetStatement_V[Timer.Instance.year].ProfitLostMonthStatement[Timer.Instance.month] =
                    balanceSheet;
            }
            else
                BalanceSheetStatement_V[Timer.Instance.year].ProfitLostMonthStatement
                    .Add(Timer.Instance.month, balanceSheet);
        }
        else //Year Not Present
        {
            var m = new MonthStatement()
            {
                ProfitLostMonthStatement = new Dictionary<int, BalanceSheet>
                    {{Timer.Instance.month, balanceSheet}}
            };
            OptionDataListYear.Add(Timer.Instance.year.ToString());

            DropdownYear.ClearOptions();
            DropdownYear.AddOptions(OptionDataListYear);

            Dropdown.value = OptionDataList.Count - 1;

            BalanceSheetStatement_V.Add(Timer.Instance.year, m);
        }

        if (OptionDataList.Count < 12)
        {
            OptionDataList.Add(date);
            Dropdown.ClearOptions();
            Dropdown.AddOptions(OptionDataList);
        }

        Dropdown.value = Timer.Instance.month - 1;
        BalanceSheetManager.Instance.PLPrefab.SetActive(true);
        Debug.Log("Balance Sheet " + balanceSheet.Inventory);
        BalanceSheetManager.Instance.openSheet(Dropdown.value);
    }
}