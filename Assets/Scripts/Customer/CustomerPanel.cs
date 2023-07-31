using Factory;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class CustomerPanel : MonoBehaviour
{
    public TMP_Text Name;
    public TMP_Text Quantity;
    public TMP_Text Cost;
    public TMP_Text CostPerUnit;
    public TMP_Text OfferPricePerUnit;
    public GameObject[] RatingStar;
    public Customer customer;

    public TMP_Text orderType_Text;
    public UnityEvent onOrderFailed;

    public CustomerManager customerManager;

    public GameObject PurchaseComplete;
    public TMP_Text PurchaseText;


    private void Awake()
    {
        var orderType = customer.orderType;
        foreach (var star in RatingStar)
        {
            star.SetActive(false);
        }

        for (var i = 0; i < customer.Rating; i++)
        {
            RatingStar[i].SetActive(true);
        }

//        Debug.Log(customer.CreditPeriod);
        
    }

    public void FillData(Customer _customer)
    {
        customer = _customer;
        Name.text = customer.Name;
        Quantity.text = customer.Quantity.ToString();
        Invoke(nameof(customUpdate), 2f); //GameManager.Instance.gameParameters.CostPerUnit.ToString();
        for (var i = 0; i < customer.Rating; i++)
        {
            RatingStar[i].SetActive(true);
        }

        orderType_Text.text = customer.orderType == Customer.OrderType.Cash ? $"*This is a cash order " : $"*This is a {customer.CreditPeriod} days credit order ";
    }

    private void customUpdate() //Remove
    {
        CostPerUnit.text = (GameManager.Instance.gameParameters.FixedRawMaterialPrice +
                            GameManager.Instance.gameParameters.ProductionCostPerCycle /
                            GameManager.Instance.gameParameters.UnitsProducedPerCycle).ToString();
        customer.ProductionCost=GameManager.Instance.gameParameters.FixedFinishedGoodPrice;
        var fx = GameManager.Instance.gameParameters.CustomerOfferPricingIncreaseBys[customer.Rating-1].min;
       // fx += fx / (Random.Range(
        //    GameManager.Instance.gameParameters.CustomerOfferPricingIncreaseBys[customer.Rating-1].min,
         //   GameManager.Instance.gameParameters.CustomerOfferPricingIncreaseBys[customer.Rating-1].max));
       // OfferPricePerUnit.text = fx.ToString();
       var offerPrice = Random.Range(GameManager.Instance.gameParameters.FinishedGoodsMinPrice,
           GameManager.Instance.gameParameters.FinishedGoodsMaxPrice);
       OfferPricePerUnit.text = offerPrice.ToString();
        customer.Cost = offerPrice * customer.Quantity;
        Cost.text = customer.Cost.ToString();
    }

    public void AcceptOrder()
    {
        if ( !TutorialManager.Instance.isTutorialCompleted) return;

        var FG = customer.Quantity;
        if (GameManager.Instance.gameParameters.AvailableFinishedGoods >= FG)
        {
            if (customer.orderType == Customer.OrderType.Cash)
            {
                //  GameManager.Instance.gameParameters.AvailableFinishedGoods -= FG;
                FinishedGoodManager.Instance.SellFinishedGoods(FG);
                FinancialManager.Instance.Credit(customer.Cost, customer.Name, customer.Quantity);
                GameManager.Instance.gameParameters.AmountInBank += customer.Cost;
                GameManager.Instance.gameParameters.LifeTimeDelivery += FG;

                OrderAction("ORDER COMPLETE WITH CASH");
                Invoke(nameof(TurnOff), 3f);
                GameManager.Instance.StartCoolDownTimer(gameObject,
                    GameManager.Instance.gameParameters.SupplierSpawnTime[Random.Range(0, 3)]);
                customerManager.OrderComplete(customer);
            }
            else
            {
                GameManager.Instance.gameParameters.AccountReceivable += customer.Cost;
                FinishedGoodManager.Instance.SellFinishedGoods(FG);
                FinancialManager.Instance.Credit(customer.Cost, customer.Name, customer.Quantity);
                GameManager.Instance.gameParameters.LifeTimeDelivery += FG;
                var o = gameObject;

                var creditTransaction = new CreditTransaction()
                {
                    customer = customer,
                };
                AccountReceivableStatement_Manager.Instance.AddToStatement(creditTransaction);
                customerManager.OrderComplete(customer);
                GameManager.Instance.StartCoolDownTimer(o,
                    GameManager.Instance.gameParameters.SupplierSpawnTime[Random.Range(0, 3)]);

                OrderAction("ORDER COMPLETE WITH CREDIT");
                Invoke(nameof(TurnOff), 3f);
            }
        }
        else
        {
            UI_Manager.Instance.LowWarning_FG_Panel.SetActive(true);
            onOrderFailed?.Invoke();
        }
    }

    public void RejectOrder()
    {
        if ( !TutorialManager.Instance.isTutorialCompleted) return;

        GameManager.Instance.StartCoolDownTimer(gameObject,
            GameManager.Instance.gameParameters.CustomerSpawnTime[Random.Range(0, 3)]);
        Invoke(nameof(TurnOff), 3f);
        OrderAction("ORDER REJECTED");
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
}