using System;
using UnityEngine;
[Serializable]
public  class Customer
{
    public string Name="Customer";
    public int Quantity;
    public int ProductionCost;
    public int Cost;
    [Range(1,5)]
    public int Rating=1;
    public int CreditPeriod=30;
    public enum OrderType
    {
        Cash,
        Credit,
    };
    
    public OrderType orderType;
    public int CreditValue;// Need to load the Credit days from list its more like a array number
    


}