using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfitLossPanel : MonoBehaviour
{
    
  public TMP_Text Month_Text;
  public TMP_Text Year_Text;
  public TMP_Text Revenue_Text;
  public TMP_Text COGS_Text;
  public TMP_Text CIVI_Text;
  public TMP_Text GrossProfit_Text;
  public TMP_Text OperatingCost_Text;
  public TMP_Text ProductionCost_Text;
  public TMP_Text interestExpense_Text;
  public TMP_Text Depreciation_Text;
  public TMP_Text Profit_BT_Text;
  public TMP_Text Profit_AT_Text;
  
  public TMP_Text Expense_Text;

  public TMP_Text netEarning;
  public ProfitLossSheet currentSheet;
 public void printSheet(ProfitLossSheet b)
 {
     currentSheet = b;
      Year_Text.text = Timer.Instance.year.ToString();
      Month_Text.text = Timer.Instance.month.ToString();

      Revenue_Text.text = b.Revenue.ToString(); 
      COGS_Text.text = b.CostOfGoodsSold.ToString();
      CIVI_Text.text = b.ChangeInTheValueOfTheInventory.ToString();
      GrossProfit_Text.text = b.GrossProfit.ToString();
      OperatingCost_Text.text = b.OperatingCost.ToString();
      ProductionCost_Text.text = b.ProductionExpense.ToString();
      interestExpense_Text.text = b.interestExpenses.ToString();
      Depreciation_Text.text = b.Depreciation.ToString();
      Profit_BT_Text.text = b.ProfitBeforeTax.ToString();
      Profit_AT_Text.text = b.ProfitAfterTax.ToString();
      Expense_Text.text = b.ProductionExpense.ToString();
     
     
      
      netEarning.color = b.ProfitBeforeTax<=0 ? Color.red : Color.green;
      netEarning.text = b.ProfitBeforeTax.ToString();
  }
 public void printSheet(ProfitLossSheet b,int year,int month)
 {
          currentSheet = b;
      Year_Text.text = year.ToString();
      Month_Text.text = month.ToString();

      Revenue_Text.text = b.Revenue.ToString(); 
      COGS_Text.text = b.CostOfGoodsSold.ToString();
      CIVI_Text.text = b.ChangeInTheValueOfTheInventory.ToString();
      GrossProfit_Text.text = b.GrossProfit.ToString();
      OperatingCost_Text.text = b.OperatingCost.ToString();
      ProductionCost_Text.text = b.ProductionExpense.ToString();
      interestExpense_Text.text = b.interestExpenses.ToString();
      Depreciation_Text.text = b.Depreciation.ToString();
      Profit_BT_Text.text = b.ProfitBeforeTax.ToString();
      Profit_AT_Text.text = b.ProfitAfterTax.ToString();
      Expense_Text.text = b.ProductionExpense.ToString();
     
     
      
      netEarning.color = b.ProfitBeforeTax<=0 ? Color.red : Color.green;
      netEarning.text = b.ProfitBeforeTax.ToString();
 }
}
