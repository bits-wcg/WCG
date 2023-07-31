using System;
using System.Collections.Generic;
using UnityEngine;
using static Custom_Func_4040;

public class CustomerStatement_Manager : MonoBehaviour
{
    public static CustomerStatement_Manager Instance;
    public List<CustomerStatement> CustomerStatements = new List<CustomerStatement>();
    public List<GameObject> CustomerStatementObjects_List;


    public GameObject customerStatement_Prefab;
    public GameObject customerStatement_Parent;

    private void Start()
    {
        Instance = this;
    }

    public void AddToStatement(Customer _customer)
    {
        var cst = new CustomerStatement
        {
            customer = _customer,
            OrderID = CustomerStatements.Count + 1,
            TransactionDate = Timer.Instance.currentGameDate
        };
        CustomerStatements.Add(cst);

        var t = Instantiate(customerStatement_Prefab, customerStatement_Parent.transform);
        t.GetComponent<CustomerStatementPanel>().CreateTransaction(cst);
    }

    public void AddToStatement(CustomerStatement cst)
    {
        CustomerStatements.Add(cst);

        var t = Instantiate(customerStatement_Prefab, customerStatement_Parent.transform);
        t.GetComponent<CustomerStatementPanel>().CreateTransaction(cst);
    }

    public void ClearStatement()
    {
        CustomerStatementObjects_List = GetChildren(customerStatement_Parent.transform);
        var header = CustomerStatementObjects_List[0];
        foreach (var statement in CustomerStatementObjects_List)
        {
            DestroyImmediate(statement);
        }

        CustomerStatements.Clear();
        CustomerStatementObjects_List.Add(header);
        CustomerStatementObjects_List = GetChildren(customerStatement_Parent.transform);
    }
}