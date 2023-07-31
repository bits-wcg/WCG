using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class TransactionManager : SerializedMonoBehaviour
{
    public static TransactionManager Instance;
    
    [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.ExpandedFoldout)]
    public  Dictionary<int, TransactionStatement> TransactionStatements = new Dictionary<int, TransactionStatement>();

    public List<Transaction> DummyRef;
    public GameObject Transaction_Template;
    public GameObject Transaction_parent;
    private void Awake()
    {
        Instance = this;
        AddNewYearStatement();
    }

    public void AddTransaction(Transaction transaction)
    {
        if (TransactionStatements.ContainsKey(Timer.Instance.year))
        {
            
        }
        else
        {
            AddNewYearStatement();
        }
        
        var t = Instantiate(Transaction_Template, Transaction_parent.transform);
        DummyRef.Add(transaction);
        t.GetComponent<TransactionStatementManager>().CreateTransaction(transaction,DummyRef.Count);
        t.transform.SetSiblingIndex(0);
  

        switch (Timer.Instance.month)
        {
            case 1:
                TransactionStatements[Timer.Instance.year].Quarterly1.January.Add(transaction);
                break;
            case 2:
                TransactionStatements[Timer.Instance.year].Quarterly1.Feb.Add(transaction);
                break;
            case 3:
                TransactionStatements[Timer.Instance.year].Quarterly1.March.Add(transaction);
                break;
            case 4:
                TransactionStatements[Timer.Instance.year].Quarterly2.April.Add(transaction);
                break;
            case 5:
                TransactionStatements[Timer.Instance.year].Quarterly2.May.Add(transaction);
                break;
            case 6:
                TransactionStatements[Timer.Instance.year].Quarterly2.June.Add(transaction);
                break;
            case 7:
                TransactionStatements[Timer.Instance.year].Quarterly3.July.Add(transaction);
                break;
            case 8:
                TransactionStatements[Timer.Instance.year].Quarterly3.August.Add(transaction);
                break;
            case 9:
                TransactionStatements[Timer.Instance.year].Quarterly3.Sep.Add(transaction);
                break;
            case 10:
                TransactionStatements[Timer.Instance.year].Quarterly4.October.Add(transaction);
                break;
            case 11:
                TransactionStatements[Timer.Instance.year].Quarterly4.November.Add(transaction);
                break;
            case 12:
                TransactionStatements[Timer.Instance.year].Quarterly4.December.Add(transaction);
                break;
        }
       // BalanceSheetManager.Instance.CalculateBalanceSheet();
    }

    private void AddNewYearStatement()
    {
        TransactionStatements.Add(Timer.Instance.year, new TransactionStatement());
    }
}