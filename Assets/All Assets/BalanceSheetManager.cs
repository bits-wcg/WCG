using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;


public class BalanceSheetManager : MonoBehaviour
{
   public static BalanceSheetManager Instance;
    public GameObject PLPrefab;
    public GameObject PLParent;

    public TMP_Dropdown Dropdown;
    public TMP_Dropdown DropdownYear;



    public List<string> OptionDataList;
    public List<string> OptionDataListYear;

    public GameObject currentBalanceSheetPrefab_V;



    private void Awake()
    {
        Instance = this;
    }

    public void CalculateBalanceSheet()
    {
        BalanceSheetCalc.Instance.CalculateBalanceSheet(PlayerDataManager.Instance._PlayerData.BalanceSheetStatement_V,OptionDataList,OptionDataListYear,DropdownYear,Dropdown);
    }
   

    private int sheetYear = 2022;
    private int sheetYearF = 2022;

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
        Debug.Log("Loading new data " + n);
        if (PlayerDataManager.Instance._PlayerData.BalanceSheetStatement_V[sheetYearF].ProfitLostMonthStatement.ContainsKey(n + 1))
        {
            if (currentBalanceSheetPrefab_V == null)
            {
                currentBalanceSheetPrefab_V = PLPrefab;
                currentBalanceSheetPrefab_V.GetComponent<BalanceSheetPanel>()
                    .FillData(PlayerDataManager.Instance._PlayerData.BalanceSheetStatement_V[sheetYearF].ProfitLostMonthStatement[n + 1]);
            }
            else
            {
                currentBalanceSheetPrefab_V.GetComponent<BalanceSheetPanel>()
                    .FillData(PlayerDataManager.Instance._PlayerData.BalanceSheetStatement_V[sheetYearF].ProfitLostMonthStatement[n + 1]);
            }
        }
    }
}