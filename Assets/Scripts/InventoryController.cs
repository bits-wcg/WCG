using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private float percentage;

    private float CurrentStockCount;

    [SerializeField] private List<SpriteRenderer> InventoryBox = new List<SpriteRenderer>();

    private void Start()
    {
        InvokeRepeating(nameof(CalculatePercentage),1,5);
    }

    private void CalculatePercentage()
    {
       
    if(GameManager.Instance==null)
     return;
         
    percentage = (GameManager.Instance.gameParameters.AvailableRawMaterial /
                      GameManager.Instance.gameParameters.MaximumRawMaterial) * 100;

    CurrentStockCount = InventoryBox.Count;
    
        var BoxCount = (int)Mathf.Round(CurrentStockCount / 100f * percentage);
        

        for (var i = 0; i < BoxCount; i++)
        {
            InventoryBox[i].gameObject.SetActive(true);
        }

       
    }


}