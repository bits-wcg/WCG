    using UnityEngine;

[System.Serializable]
public class Supplier
{
    public string Name="Supplier";
    public int RawMaterials;
    public int Cost;
    [Range(1,5)]
    public int Rating=1;
    public int costPerUnity;
    public int CreditPeriod=0;
    public enum OrderType
    {
        Cash,
        Credit,
    };

    public int CreditValue;
    public OrderType orderType;
}