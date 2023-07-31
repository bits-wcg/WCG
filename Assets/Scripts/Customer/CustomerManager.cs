using System;
using System.Linq;
using UnityEngine;
using UnityEngine;
using Random = UnityEngine.Random;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager Instance;
    public GameManager gameManager;
    public UI_Manager uiManager;
    public Customer newCustomer;

    private void Awake()
    {
        Instance = this;
    }

    public void AddNewCustomer(Customer customer)
    {
        
        // var check = gameManager.gameParameters.AvailableCustomerList.Any(vCustomer => vCustomer.Name == customer.Name);
        //
        // if (customer.Name == "Default Customer")
        // {
        //     customer.Name = "Default Customer " + (gameManager.gameParameters.AvailableCustomerList.Count+1);
        //     var n = Random.Range(100, 300);
        //     customer.Cost = n * gameManager.gameParameters.CostPerUnit;
        //     customer.Quantity = n;
        //     customer.Rating = 5;
        // }
        // if (check)
        // {
        //     Debug.Log(
        //         $"<color=red>customer already available with {customer.Name} {customer.Quantity} {customer.Cost} {customer.Rating} {customer.orderType}</color>");
        //     newCustomer = new Customer();
        // }
        // else
        // {
        //     Debug.Log(
        //         $"<color=green>Adding new customer {customer.Name} {customer.Quantity} {customer.Cost} {customer.Rating} {customer.orderType}</color>");
        //  
        //     gameManager.gameParameters.AvailableCustomerList.Add(customer);
        //
        //     uiManager.RefreshCustomerPanel();
        //     newCustomer = new Customer();
        // }
    }

    public void ClearAll()
    {
        uiManager.RefreshCustomerPanel();
    }

    public void OrderComplete(Customer customer)
    {
        CustomerStatement_Manager.Instance.AddToStatement(customer);
    }

    public void OrderRejected(Customer customer)
    {

    }
}