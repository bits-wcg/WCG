using System.Collections;
using TMPro;
using UnityEngine;

public class ParametersTextManager : MonoBehaviour
{
    [Header("GameHUD")] public TMP_Text AccountBalance_Text;

    [Space(10)] [Header("Factory Mini DashBoard")]
    public TMP_Text RawMaterial_Text;

    public TMP_Text FinishedGoods_Text;
    public TMP_Text Delivered_Text;

    [Space(10)] [Header("Financial Mini DashBoard")]
    public TMP_Text AccountPayable_Text;

    public TMP_Text AccountReceivable_Text;
    public TMP_Text ValueOfTheFirm_Text;

    [Space(10)] [Header("Financial DashBoard")]
   

    public TMP_Text ProductionCostPerCycle_Text;
    public TMP_Text AccountBalance_Dash_Text;
    public TMP_Text AccountPayable_Dash_Text;
    public TMP_Text AccountReceivable_Dash_Text;
    public TMP_Text ValueOfTheFirm_Dash_Text;
    public TMP_Text InventoryValue_Dash_Text;
    public TMP_Text RawMaterialValue_Dash_Text;
    public TMP_Text FGValue_Dash_Text;
    public TMP_Text ProductionCostPerCycle_Dash_Text;
    public TMP_Text FixedProcessingCharge_Dash_Text;
    public TMP_Text taxRate_Text;
    public TMP_Text InterestRateOnLoan_Text;
    public TMP_Text TransactionText;

    private IEnumerator UpdateValues()
    {
        while (true)
        {
            var GameParameters = GameManager.Instance.gameParameters;

            AccountBalance_Text.text = GameParameters.AmountInBank.ToString();
            AccountBalance_Dash_Text.text = GameParameters.AmountInBank.ToString();

            RawMaterial_Text.text = GameParameters.AvailableRawMaterial.ToString();
            if (GameParameters.AvailableFinishedGoods < 0)
                GameParameters.AvailableFinishedGoods = 0;
            FinishedGoods_Text.text = GameParameters.AvailableFinishedGoods.ToString();
            Delivered_Text.text = GameParameters.LifeTimeDelivery.ToString();

            AccountPayable_Text.text = GameParameters.AccountPayable.ToString();
            AccountPayable_Dash_Text.text = GameParameters.AccountPayable.ToString();

            AccountReceivable_Text.text = GameParameters.AccountReceivable.ToString();
            AccountReceivable_Dash_Text.text = GameParameters.AccountReceivable.ToString();


            RawMaterialValue_Dash_Text.text = GameParameters.TotalRawMaterialValue.ToString();

            FGValue_Dash_Text.text = GameParameters.TotalFinishedGoodsValue.ToString();

            InventoryValue_Dash_Text.text = GameParameters.InventoryValue.ToString();

            ValueOfTheFirm_Text.text = GameParameters.ValueOfTheFirm.ToString();
            ValueOfTheFirm_Dash_Text.text = GameParameters.ValueOfTheFirm.ToString();

            ProductionCostPerCycle_Text.text = GameParameters.ProductionCostPerCycle.ToString();
            ProductionCostPerCycle_Dash_Text.text = GameParameters.ProductionCostPerCycle.ToString();
            
            FixedProcessingCharge_Dash_Text.text = GameParameters.FixedOperatingCharge.ToString();

            taxRate_Text.text = $"TAX RATE : {GameParameters.TaxRate}%";
            InterestRateOnLoan_Text.text = $"LOAN RATE : {GameParameters.LoanInterest}%";
            
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Start()
    {
        StartCoroutine(UpdateValues());
    }
}