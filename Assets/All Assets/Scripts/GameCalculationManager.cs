using System;
using RawMaterial;
using UnityEngine;

public class GameCalculationManager : MonoBehaviour
{
    public static GameCalculationManager Instance;
    private void Start()
    {
        Instance = this;
        var parameters = GameManager.Instance.gameParameters;
        //Initial TOTAL RAW MATERIAL VALUE CALCULATION
       
      //  InvokeRepeating(nameof(Calculate), 0.1f, 0.1f);
    }

    public void Update()
    {
        if (!Timer.Instance.startTimer || GameManager.Instance.isGameOver)
            return;
        
       
        var parameters = GameManager.Instance.gameParameters;

        // TOTAL RAW MATERIAL VALUE CALCULATION


        // TOTAL FINISHED GOODS VALUE CALCULATION


        // INVENTORY VALUE = TOTAL FINISHED GOODS VALUE +  TOTAL RAW MATERIAL VALUE
        parameters.InventoryValue = parameters.TotalFinishedGoodsValue + parameters.TotalRawMaterialValue;

        // VALUE OF THE FIRM = AMOUNT IN BANK + ACCOUNT RECEIVABLE + INITIAL COST + INVENTORY VALUE
        parameters.ValueOfTheFirm = (int)(parameters.AmountInBank +
            parameters.AccountReceivable +
            parameters.CostOfTheFactory +
            parameters.InventoryValue - (parameters.currentLiability) );

        
        // FINISHED GOODS PRICE =  RAW MATERIAL COST + PROCESSING COST PER RAW MATERIAL + PRODUCTION COST PER RAW MATERIAL
        // PROCESSING COST PER RAW MATERIAL = FIXED PROCESSING CHARGE / (AMOUNT OF RAW MATERIAL PER CYCLE * CYCLES PER MONTH)
        // PRODUCTION COST PER RAW MATERIAL = PRODUCTION COST /  AMOUNT OF RAW MATERIAL PER CYCLE
        parameters.FixedFinishedGoodPrice = (int)(parameters.FixedRawMaterialPrice +
                                                  parameters.FixedOperatingCharge /
                                                  (parameters.UnitsProducedPerCycle *
                                                   parameters.CyclesPerMonth) + parameters.ProductionCostPerCycle /
                                                  parameters.UnitsProducedPerCycle);
       // Debug.Log("FF " + parameters.FixedFinishedGoodPrice);
        //Debug.Log($"{parameters.FixedRawMaterialPrice} {parameters.FixedOperatingCharge}{parameters.UnitsProducedPerCycle} *{parameters.CyclesPerMonth} + {parameters.ProductionCostPerCycle} /{parameters.UnitsProducedPerCycle}");
        
        if(GameManager.Instance.startValueOfTheFirm<=0)
        GameManager.Instance.startValueOfTheFirm = parameters.ValueOfTheFirm;   
    }




  
}

public static class FF
{
    public enum transactionType
    {
        Credit,
        Debit
    }

    public static transactionType TransactionType;
}