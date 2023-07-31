 
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FactoryManager : MonoBehaviour
{
    public bool isFactoryOperational=true;
    public bool isRawMaterialAvailable;
    public bool isInventoryFull;

   // public TMP_Text CyclePercent;
    public TMP_Text AvailableRawMaterial;
    public TMP_Text AvailableFinishedGoods;
    public TMP_Text RunningSince;
    public TMP_Text UnitsPerCycle;
    public TMP_Text PerCycleCost;
    public TMP_Text DeliveryMade;
    public GameObject RotateBar;
    
   

    public static FactoryManager Instance;

    private void Start()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        AvailableRawMaterial.text =$" {GameManager.Instance.gameParameters.AvailableRawMaterial} / {GameManager.Instance.gameParameters.MaximumRawMaterial}";
        AvailableFinishedGoods.text = $" {GameManager.Instance.gameParameters.AvailableFinishedGoods} / {GameManager.Instance.gameParameters.MaximumFinishedGoods}";

        UnitsPerCycle.text = GameManager.Instance.gameParameters.UnitsProducedPerCycle.ToString();
        PerCycleCost.text = GameManager.Instance.gameParameters.ProductionCostPerCycle.ToString();
        DeliveryMade.text = GameManager.Instance.gameParameters.LifeTimeDelivery.ToString();
        RunningSince.text = "2022";

        if (Timer.Instance.startTimer && isFactoryOperational)
            RotateBar.GetComponent<Animator>().enabled = true;
        else
        {
            RotateBar.GetComponent<Animator>().enabled = false;
        }
    }
}
