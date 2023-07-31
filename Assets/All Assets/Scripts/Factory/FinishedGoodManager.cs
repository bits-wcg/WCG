using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Factory
{
    public class FinishedGoodManager : MonoBehaviour
    {
        public static FinishedGoodManager Instance;
        [DisableInEditorMode] public List<FinishedGoodsList> FinishedGoodsList = new List<FinishedGoodsList>();

        private void Start()
        {
            Instance = this;
            InvokeRepeating(nameof(CalculateValue),1,0.5f);
        }

        public void AddNewFinishedGoods()
        {
            var date = new DateTime(Timer.Instance.year, Timer.Instance.month, Timer.Instance.date).ToString("MMMM");
            var fga = new FinishedGoodsList()
            {
                ProductionDate = date,
                ProductionQuantity = GameManager.Instance.gameParameters.AvailableFinishedGoods,
                FinishedGoodsUnitCost =  GameManager.Instance.gameParameters.FixedFinishedGoodPrice,
                FinishedGoodsValue = (GameManager.Instance.gameParameters.AvailableFinishedGoods * GameManager.Instance.gameParameters.FixedFinishedGoodPrice)
            };
            FinishedGoodsList.Add(fga);
          //  StartCoroutine(ProductLife(fga));
        }

        private string date;
        public void AddNewFinishedGoods(int quantity)
        {
           // FixedRawMaterialPrice + FixedProcessingCharge / (UnitsProducedPerCycle * CyclesPerMonth) + ProductionCostPerCycle / UnitsProducedPerCycle
           var g = GameManager.Instance.gameParameters;
      
           // var FG_UnitCost=  g.FixedRawMaterialPrice + g.FixedOperatingCharge / (g.UnitsProducedPerCycle * g.CyclesPerMonth) +
           //     g.ProductionCostPerCycle / g.UnitsProducedPerCycle;
           
            g.AvailableFinishedGoods += quantity;
            try
            {
                 date = new DateTime(Timer.Instance.year, Timer.Instance.month, Timer.Instance.date).ToString("MMMM");

            }
            catch
            {
                Debug.Log("Date Exceeded");
                date = new DateTime(Timer.Instance.year, Timer.Instance.month, Timer.Instance.date-1).ToString("MMMM");

            }
            var fga = new FinishedGoodsList()
            {
                ProductionDate = date,
                ProductionQuantity = quantity,
                FinishedGoodsUnitCost =  GameManager.Instance.gameParameters.FixedFinishedGoodPrice,
                FinishedGoodsValue =quantity * GameManager.Instance.gameParameters.FixedFinishedGoodPrice
                
            };
            FinishedGoodsList.Add(fga);
         //   StartCoroutine(ProductLife(fga));
        }

        // private IEnumerator ProductLife(FinishedGoodsList fga)
        // {
        //    
        //     var d = Timer.Instance.dateTimeRef.AddMonths(GameManager.Instance.gameParameters
        //         .UsefulLife); // will wait for months and destroy the list.
        //
        //     while (d != Timer.Instance.dateTimeRef)
        //     {
        //         var n = (Timer.Instance.dateTimeRef - d).TotalDays;
        //         fga.ExpiresInDays = -(int) (n);
        //         yield return null;
        //     }
        //
        //     if (fga.ProductionQuantity > 0)
        //     {
        //         UI_Manager.Instance.Notification($"Removing Expired Finished Goods of {fga.ProductionQuantity}",
        //             UI_Manager.NotificationTypes.lowWarning);
        //
        //         GameManager.Instance.gameParameters.AvailableFinishedGoods -= fga.ProductionQuantity;
        //     }
        //
        //     FinishedGoodsList.Remove(fga);
        // }

        public void SellFinishedGoods(int PurchaseQuantity)
        {
            //  Debug.Log("Attempting to Sell Goods");
            GameManager.Instance.gameParameters.AvailableFinishedGoods -= PurchaseQuantity;
            //  Debug.Log("Sold Goods");
            //   Debug.Log("Starting to Update Inventory");
            foreach (var finishedGoodsList in FinishedGoodsList)
            {
            
                //  Debug.Log($"Checking {finishedGoodsList.ProductionDate}  {finishedGoodsList.ProductionAmount} <= {PurchaseQuantity} .....{finishedGoodsList.ProductionAmount <= PurchaseQuantity}");
                if (finishedGoodsList.ProductionQuantity >= PurchaseQuantity)
                {
                           Debug.Log("Removing Sold Fg from Inventory Data");
                    finishedGoodsList.ProductionQuantity -= PurchaseQuantity;
                    break;
                }
                else
                {
                       Debug.Log("Removing partial Good from current data");
                    PurchaseQuantity -= finishedGoodsList.ProductionQuantity;
                    finishedGoodsList.ProductionQuantity = 0;
                }
      
            }
            
            for (var i = FinishedGoodsList.Count-1; i >=0 ; i--)
            {
                if (FinishedGoodsList[i].ProductionQuantity == 0)
                {
                    FinishedGoodsList.Remove(FinishedGoodsList[i]);
                }
            }
        }
        public int totalProductionQuantity = 0;
        public int totalGoodsValue = 0;
        public void CalculateValue()
        {
             totalProductionQuantity = 0;
             totalGoodsValue = 0;
            foreach (var FG in FinishedGoodsList)
            {
        
                totalProductionQuantity += FG.ProductionQuantity;
                totalGoodsValue += FG.ProductionQuantity * FG.FinishedGoodsUnitCost;
            }
         //   GameManager.Instance.gameParameters.TotalFinishedGoodsValue =totalGoodsValue;
            GameManager.Instance.gameParameters.TotalFinishedGoodsValue =totalProductionQuantity*GameManager.Instance.gameParameters.FixedFinishedGoodPrice;
            GameManager.Instance.gameParameters.AvailableFinishedGoods =totalProductionQuantity;
        }
    }

    [System.Serializable]
    public class FinishedGoodsList
    {
        public string ProductionDate;
        public int ProductionQuantity;
        //public int ExpiresInDays;
        public int FinishedGoodsValue;
        public int FinishedGoodsUnitCost;
    }
    

}