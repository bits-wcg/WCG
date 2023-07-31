using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SupplierManager : MonoBehaviour
{
    public static SupplierManager Instance;
    public GameManager gameManager;
    public UI_Manager uiManager;
    public Supplier newSupplier;

    private void Awake()
    {
        Instance = this;
    }


    // public void AddNewSupplier(Supplier supplier)
    // {
    //     if (supplier.Name == "Default Supplier")
    //     {
    //         supplier.Name = "Default Supplier " + (gameManager.AvailableSupplierList.Count + 1);
    //         var n = Random.Range(100, 300);
    //         supplier.RawMaterials = n;
    //         supplier.Cost = n * gameManager.gameParameters.FixedRawMaterialPrice;
    //         supplier.Rating = 5;
    //     }
    //
    //     uiManager.RefreshSupplierPanel(); //Remove
    //
    //     var check = gameManager.gameParameters.AvailableSupplierList.Any(vSupplier => vSupplier.Name == supplier.Name);
    //
    //     if (check)
    //     {
    //         Debug.Log(
    //             $"<color=red>Supplier already available with {supplier.Name} {supplier.RawMaterials} {supplier.Cost} {supplier.Rating} </color>");
    //         //newSupplier = new Supplier();
    //     }
    //     else
    //     {
    //         Debug.Log(
    //             $"<color=green>Adding new Supplier {supplier.Name} {supplier.RawMaterials} {supplier.Cost} {supplier.Rating} </color>");
    //         gameManager.AvailableSupplierList.Add(supplier);
    //
    //         uiManager.RefreshSupplierPanel();
    //         //newSupplier = new Supplier();
    //     }
    // }

    public void AddSupplier(Supplier supplier)
    {
    }

    public void ClearAll()
    {
        //  gameManager.AvailableSupplierList.Clear();
        uiManager.RefreshSupplierPanel();
    }

    public void OrderComplete(Supplier supplier)
    {
//      SupplierStatementManager.Instance.AddToStatement(supplier);
        Debug.Log("Adding Purchase  ");
        PurchaseStatementManager.Instance.AddToStatement(supplier);
    }

    public void OrderRejected(Supplier supplier)
    {
        uiManager.RefreshSupplierPanel();
    }
}