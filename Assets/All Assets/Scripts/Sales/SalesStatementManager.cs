using System.Collections.Generic;
using UnityEngine;


public class SalesStatementManager : MonoBehaviour
{
    public static SalesStatementManager Instance;
    public List<SalesStatement> CustomerStatements = new List<SalesStatement>();
    public List<GameObject> CustomerStatementObjects_List;


    public GameObject customerStatement_Prefab;
    public GameObject customerStatement_Parent;

    private void Start()
    {
        Instance = this;
    }

    public void AddToStatement(Customer _customer)
    {
        var cst = new SalesStatement()
        {
            customer = _customer,
            OrderID = CustomerStatements.Count + 1,
            TransactionDate = Timer.Instance.currentGameDate
        };
        CustomerStatements.Add(cst);

        var t = Instantiate(customerStatement_Prefab, customerStatement_Parent.transform);
        t.GetComponent<SalesStatementPanel>().CreateTransaction(cst);
    }
}