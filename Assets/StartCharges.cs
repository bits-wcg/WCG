using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartCharges : MonoBehaviour
{
    public TMP_Text Message;


    private void Start()
    {
        var g = GameManager.Instance.gameParameters;
        Message.text =
            $"Following will be charged/deducted on starting the factory \n\n Monthly Processing Charges : {g.FixedOperatingCharge} INR \n Monthly Production Cost : {g.ProductionCostPerCycle * g.CyclesPerMonth} INR \n Raw Material Required for Current Month : {g.UnitsProducedPerCycle} UNITS";
    }
}
