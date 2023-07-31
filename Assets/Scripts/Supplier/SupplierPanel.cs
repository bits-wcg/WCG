using System;
using RawMaterial;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SupplierPanel : MonoBehaviour
{
    public TMP_Text Name;
    public TMP_Text Quantity;
    public TMP_Text Cost;
    public TMP_Text CostPerUnit;
    public GameObject[] RatingStar;
    [HideInInspector] public Supplier supplier;
    public Button CreditPurchase_Btn;
    public Button CashPurchase_Btn;
    public TMP_Text Note_Text;
    public UnityEvent onOrderFailed;

    public SupplierManager supplierManager;

    public GameObject PurchaseComplete;
    public TMP_Text PurchaseText;

    private void OnEnable()
    {
        foreach (var star in RatingStar)
        {
            star.SetActive(false);
        }

        for (var i = 0; i < supplier.Rating; i++)
        {
            RatingStar[i].SetActive(true);
        }

        FillData(supplier);
        GameManager.Instance.AvailableSuppliers++;
    }


    public void FillData(Supplier _supplier)
    {
        supplier = _supplier;
        Name.text = supplier.Name;

        Quantity.text = supplier.RawMaterials.ToString();
        Invoke(nameof(customUpdate), 2f);
        if (supplier.orderType == Supplier.OrderType.Cash)
        {
            CreditPurchase_Btn.gameObject.SetActive(false);
            Note_Text.text = "";
        }
        else
        {
            Note_Text.text = $"CREDIT PERIOD : {supplier.CreditPeriod} DAYS";
            CashPurchase_Btn.gameObject.SetActive(false);
        }

        for (var i = 0; i < supplier.Rating; i++)
        {
            RatingStar[i].SetActive(true);
        }
    }

    private void customUpdate()
    {
        var CPU = Random.Range(GameManager.Instance.gameParameters.RawMaterialMinPrice,
            GameManager.Instance.gameParameters.RawMaterialMaxPrice);
        Debug.Log("Customer "+CPU);
        var min = 1;
        var max = 1;
        min = GameManager.Instance.gameParameters.RW_Ratings_MinMax[supplier.Rating - 1].min == 0
            ? 1
            : GameManager.Instance.gameParameters.RW_Ratings_MinMax[supplier.Rating - 1].min;
        max = GameManager.Instance.gameParameters.RW_Ratings_MinMax[supplier.Rating - 1].max == 0
            ? 1
            : GameManager.Instance.gameParameters.RW_Ratings_MinMax[supplier.Rating - 1].max;
        Debug.Log(min+" Customer "+max);
        var r = Random.Range(min, max);
        Debug.Log(" Customer R"+r);
        //CPU = CPU * r/100;
        Debug.Log("Customer "+CPU);
        supplier.costPerUnity = CPU;
        CostPerUnit.text = CPU.ToString();

        supplier.Cost = supplier.RawMaterials * CPU;
        Cost.text = supplier.Cost.ToString();
    }

    public void customUpdateLatePay()
    {
        if (supplier.Rating > 1)
            supplier.Rating -= 1;

        foreach (var star in RatingStar)
        {
            star.SetActive(false);
        }

        for (var i = 0; i < supplier.Rating; i++)
        {
            RatingStar[i].SetActive(true);
        }

        var CPU = GameManager.Instance.gameParameters.FixedRawMaterialPrice;
        if(   GameManager.Instance.gameParameters.CustomerOfferPricingIncreaseBys[supplier.Rating - 1].max!=0)
        {
            CPU += CPU / (Random.Range(
                GameManager.Instance.gameParameters.CustomerOfferPricingIncreaseBys[supplier.Rating - 1].min,
                GameManager.Instance.gameParameters.CustomerOfferPricingIncreaseBys[supplier.Rating - 1].max));
          
        }
        supplier.costPerUnity = CPU;
        CostPerUnit.text = CPU.ToString();

        supplier.Cost = supplier.RawMaterials * CPU;
        Cost.text = supplier.Cost.ToString();
    }


    public void BuyOnCash()
    {
        if ( !TutorialManager.Instance.isTutorialCompleted) return;
      

       
        Debug.Log("Attempting to Buy on cash");
        var BB = supplier.Cost;

        if ((GameManager.Instance.gameParameters.MaximumRawMaterial -
             GameManager.Instance.gameParameters.AvailableRawMaterial) >= supplier.RawMaterials)
        {
            if (GameManager.Instance.gameParameters.AmountInBank >= BB)
            {
                FinancialManager.Instance.Debit(BB, supplier.Name);
                
                var g = GameManager.Instance.gameParameters;
                var a = g.AvailableRawMaterial * g.FixedRawMaterialPrice;
                Debug.Log("NEW "+a);
                var b= supplier.RawMaterials * supplier.costPerUnity;
                Debug.Log("NEW "+b);
                var c=g.AvailableRawMaterial+supplier.RawMaterials;
                Debug.Log("NEW "+c);
                g.FixedRawMaterialPrice =
                    ((g.AvailableRawMaterial * g.FixedRawMaterialPrice) +
                     (supplier.RawMaterials * supplier.costPerUnity)) / (g.AvailableRawMaterial+supplier.RawMaterials);
                RawMaterialManager.Instance.AddRawMaterial(supplier.RawMaterials, supplier.costPerUnity,
                    Timer.Instance.currentGameDate, supplier.Name,false);
                GameManager.Instance.gameParameters.AvailableRawMaterial += supplier.RawMaterials;

                OrderAction("Purchased Using Cash");
                Invoke(nameof(TurnOff), 3f);
                GameManager.Instance.StartCoolDownTimer(gameObject,
                    GameManager.Instance.gameParameters.CustomerSpawnTime[Random.Range(0, 3)]);
                supplier.orderType = Supplier.OrderType.Cash;
                supplierManager.OrderComplete(supplier);
                Debug.Log("Purchased raw material on cash");
      
        
            }
            else
            {
                Debug.Log("Purchasing on cash Failed: insufficient Balance");
                UI_Manager.Instance.LowOnCashNotification();
                onOrderFailed?.Invoke();
            }
            
        }
        else
        {
            UI_Manager.Instance.FullWarning_RM_Panel.SetActive(true);
        }
    }

    public void BuyOnCredit()
    {
        if ( !TutorialManager.Instance.isTutorialCompleted) return;

        if ((GameManager.Instance.gameParameters.MaximumRawMaterial -
             GameManager.Instance.gameParameters.AvailableRawMaterial) >= supplier.RawMaterials)
        {
            Debug.Log("Attempting to Buy on credit");
            GameManager.Instance.gameParameters.AvailableRawMaterial += supplier.RawMaterials;
            GameManager.Instance.gameParameters.AccountPayable += supplier.Cost;

            OrderAction("Purchased Using Credit");
            Invoke(nameof(TurnOff), 3f);
            AccountPayableStatementPanel panel = AccountPayableStatementManager.Instance.AddToStatement(supplier);
            GameManager.Instance.StartCoolDownTimer(gameObject,
                GameManager.Instance.gameParameters.CustomerSpawnTime[Random.Range(0, 3)]);

            supplier.orderType = Supplier.OrderType.Credit;
            supplierManager.OrderComplete(supplier);
            FinancialManager.Instance.PaymentDueC(GameManager.Instance.gameParameters.CreditPeriodSupplier, supplier,
                panel, this.gameObject);

            var g = GameManager.Instance.gameParameters;
            var a = g.AvailableRawMaterial * g.FixedRawMaterialPrice;
            Debug.Log("NEW "+a);
            var b= supplier.RawMaterials * supplier.costPerUnity;
            Debug.Log("NEW "+b);
            var c=g.AvailableRawMaterial+supplier.RawMaterials;
            Debug.Log("NEW "+c);
            g.FixedRawMaterialPrice =
                ((g.AvailableRawMaterial * g.FixedRawMaterialPrice) +
                 (supplier.RawMaterials * supplier.costPerUnity)) / (g.AvailableRawMaterial+supplier.RawMaterials);
            RawMaterialManager.Instance.AddRawMaterial(supplier.RawMaterials, supplier.costPerUnity,
                Timer.Instance.currentGameDate, supplier.Name,false );
        }
        else
        {
            UI_Manager.Instance.FullWarning_RM_Panel.SetActive(true);
        }
    }

    private void OrderAction(string message)
    {
        PurchaseComplete.SetActive(true);
        PurchaseText.text = message;
    }

    public void TurnOff()
    {
        PurchaseComplete.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        GameManager.Instance.AvailableSuppliers--;
    }
}