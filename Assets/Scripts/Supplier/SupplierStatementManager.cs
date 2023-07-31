using System.Collections.Generic;
using UnityEngine;
using static Custom_Func_4040;

public class SupplierStatementManager : MonoBehaviour
{
    public static SupplierStatementManager Instance;
    public List<SupplierStatement> supplierStatements = new List<SupplierStatement>();
    public List<GameObject> supplierStatementObjects_List;


    public GameObject supplierStatement_Prefab;
    public GameObject supplierStatement_Parent;

    private void Start()
    {
        Instance = this;
    }

    public void AddToStatement(Supplier _supplier)
    {
        if (_supplier.orderType == Supplier.OrderType.Credit)
        {
            var cst = new SupplierStatement()
            {
                supplier = _supplier,
                OrderID = supplierStatements.Count + 1,
                TransactionDate = Timer.Instance.currentGameDate
            };
            supplierStatements.Add(cst);

            var t = Instantiate(supplierStatement_Prefab, supplierStatement_Parent.transform);
            t.GetComponent<SupplierStatementPanel>().CreateTransaction(cst);
        }
    }

    public void AddToStatement(SupplierStatement cst)
    {
        // supplierStatements.Add(cst);
        //
        // var t = Instantiate(supplierStatement_Prefab, supplierStatement_Parent.transform);
        // t.GetComponent<SupplierStatementPanel>().CreateTransaction(cst);
    }

    public void ClearStatement()
    {
        supplierStatementObjects_List = GetChildren(supplierStatement_Parent.transform);
        var header = supplierStatementObjects_List[0];
        foreach (var statement in supplierStatementObjects_List)
        {
            DestroyImmediate(statement);
        }
        supplierStatements.Clear();
        supplierStatementObjects_List.Add(header);
        supplierStatementObjects_List = GetChildren(supplierStatement_Parent.transform);
    }
}