using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class GameParameters
{
    #region FinancialParameters
    
    public int startYear = 2022;
  
    public float TotalRawMaterialValue; 
    public float TotalFinishedGoodsValue;
    public float InventoryValue; 
    
    public float AccountPayable;
    public float AccountReceivable;
    
    public int ValueOfTheFirm;
  
    public int CreditPeriodSupplier = 30;

    #endregion

    #region Modifiyable Parameters
    public float AmountInBank = 40000;
    public float FixedOperatingCharge = 1000    ;  //Monthly once
    public float CostOfTheFactory = 50000;
    public int CyclesPerMonth = 6;
   
    public int UsefulLife = 2; //years
    public float InitialRawMaterial = 700;
    public float InitialBankBalance = 40000;
    public float InitialFinishedGoods = 500;
    public float MaximumRawMaterial = 1000;
    public float MaximumFinishedGoods = 1000;
    public float ProductionCostPerCycle = 100;
   
    public int UnitsProducedPerCycle = 50;
    public int AvailableRawMaterial = 700;
    public int AvailableFinishedGoods = 500;

    #endregion
    #region Factory Parameters

   
    #endregion

    #region Inventory Parameters


    public int LifeTimeDelivery;
    public float Initial_DE_Ratio = 10;
    public float LoanInterest = 10;
    public float Debt;
    public float Equity;
    public float currentLiability;
    
    #endregion

    #region Raw Material

    public int RawMaterialMinPrice = 100;
    public int RawMaterialMaxPrice = 150;
    public int InitialRawMaterialPrice = 10;
    public int FixedRawMaterialPrice = 12;
    public int FixedFinishedGoodPrice=1000;
    public int FinishedGoodsMinPrice = 100;
    public int InitialFinishedGoodsPrice = 100;
    public int FinishedGoodsMaxPrice = 150;
    [System.Serializable]
    public class RawMaterialRating
    {
        public int min, max;
    }
    [System.Serializable]
    public class CustomerOfferPricingIncreaseBy
    {
        public int min, max;
    }
    public List<RawMaterialRating> RW_Ratings_MinMax = new List<RawMaterialRating>()
    {
        new RawMaterialRating() {min = 1, max = 5},
        new RawMaterialRating() {min = 10, max = 15},
        new RawMaterialRating() {min = 20, max = 25},
        new RawMaterialRating() {min = 30, max = 35},
        new RawMaterialRating() {min = 40, max = 45}
    };
    public List<CustomerOfferPricingIncreaseBy> CustomerOfferPricingIncreaseBys = new List<CustomerOfferPricingIncreaseBy>()
    {
        new CustomerOfferPricingIncreaseBy() {min = 1, max = 5},
        new CustomerOfferPricingIncreaseBy() {min = 10, max = 15},
        new CustomerOfferPricingIncreaseBy() {min = 20, max = 25},
        new CustomerOfferPricingIncreaseBy() {min = 30, max = 35},
        new CustomerOfferPricingIncreaseBy() {min = 40, max = 45}
    };
    [FormerlySerializedAs("RatingPercentage")]
    public int[] RM_RatingPercentage = new int[5];
    
    #endregion

    

    #region GameMode

    public int Fast = 1000; // 1 min = ~365 days
    public int Medium = 500; // 1 min = ~180 days
    public int Slow = 200;    // 1 min = ~73 days

    public int GameLength = 10; //Years

    #endregion
    public List<Customer> AvailableCustomerList;
    public List<Supplier> AvailableSupplierList;
    public int[] CustomerSpawnTime ={30, 60, 90};
    public int[] SupplierSpawnTime ={30, 60, 90};

    #region Others
    public float CurrentLiabilities; // Not Defined Yet
    public float InterestRateOnLoan = 10; 
    public float TaxRate = 30;
    public float Depreciation = 5;

    #endregion

    /*
     Supplier Example
     Quantity - 10
     Cost - 100
     Interest 30 days (10%)
   
    Rating | Cash Purchase  |  Credit Purchase
      5            100           100 + %Interest =110
      4       100+(10R%)=110     110 + %Interest =121
      3       100+(20R%)=120     120 + %Interest =132
      2       100+(30R%)=130     130 + %Interest =143
      1       100+(40R%)=140     140 + %Interest =154 
     
     remove the interest for delay payment
     */

    /*
     * Fixed Raw material value
     * 100 = cost + operation cost - FG cost = profit
     *
     * 
     * sup 1  100  $10
     * sup 2  100  $11
     * sup 3  100  $12
     * sup 4  100  $13
     *
     * 250
     * 
     * 27$+10$ = 40$
     */

    /*
     * Finished Goods cost is calculated through
     * Fixed Raw Material cost + processing cost/no of goods produced per cycle
     *
     * (10*10)+(100/10)=100+10 = 110
     */
}