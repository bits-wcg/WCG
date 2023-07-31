using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[SerializeField]
public  class PlayerData
{
   
    public  string PlayerName;
    public string Result;
    
    public GameParameters resultParameters;
  
    #region Balance Sheet Data

    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.ExpandedFoldout)]
    public  Dictionary<int, MonthStatement> BalanceSheetStatement_V = new Dictionary<int, MonthStatement>();
    #endregion

    #region ProfitLossSheetData


    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
    public  Dictionary<int, MonthStatement_V> ProfitLossStatement_V = new Dictionary<int, MonthStatement_V>();
    #endregion
}

[System.Serializable]
public class MonthStatement_V
{
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
    public Dictionary<int, ProfitLossSheet> ProfitLostMonthStatement = new Dictionary<int, ProfitLossSheet>();
    //     public List<BSandPL> BSandPls = new List<BSandPL>();
}

[System.Serializable]
public class MonthStatement
{
    [DictionaryDrawerSettings(DisplayMode= DictionaryDisplayOptions.CollapsedFoldout)]
    public Dictionary<int, BalanceSheet> ProfitLostMonthStatement = new Dictionary<int, BalanceSheet>();
}
