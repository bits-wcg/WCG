using TMPro;
using UnityEngine;


public class BalanceSheetPanel : MonoBehaviour
{
    public TMP_Text CashAndCashEquivalents;
    public TMP_Text AccountsReceivable;
    public TMP_Text Inventory;
    public TMP_Text FixedCost;
    public TMP_Text TotalCurrentAssets;
    public TMP_Text OperatingProfit;
    

    public TMP_Text CurrentLiabilities;
    public TMP_Text Equity;
    public TMP_Text Dept;
    public TMP_Text TotalCurrentLiabilities;
    public TMP_Text OperatingLoss;

    public void FillData(BalanceSheet b)
    {
        CashAndCashEquivalents.text = b.CashAndCashEquivalents.ToString();
        AccountsReceivable.text = b.AccountsReceivable.ToString();
        Inventory.text = b.Inventory.ToString();
        FixedCost.text = b.FixedAsset.ToString();
        TotalCurrentAssets.text = b.TotalCurrentAssets.ToString();
        CurrentLiabilities.text = b.CurrentLiabilities.ToString();
        Equity.text = b.Equity.ToString();
        Dept.text = b.Debt.ToString();
        TotalCurrentLiabilities.text = b.TotalCurrentLiabilities.ToString();
        var p = b.TotalCurrentAssets - b.TotalCurrentLiabilities;
        switch (p)
        {
            case < 0: // In loss
                OperatingLoss.text = p.ToString();
                OperatingLoss.gameObject.transform.parent.gameObject.SetActive(true);
                OperatingProfit.gameObject.transform.parent.gameObject.SetActive(false);
                break;
            case > 0: // In Profit
                OperatingProfit.text = p.ToString();
                OperatingLoss.gameObject.transform.parent.gameObject.SetActive(false);
                OperatingProfit.gameObject.transform.parent.gameObject.SetActive(true);
                break;
            default: // Break even
                OperatingLoss.gameObject.transform.parent.gameObject.SetActive(false);
                OperatingProfit.gameObject.transform.parent.gameObject.SetActive(false);
                break;
        }
    }
}