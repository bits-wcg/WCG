#if (UNITY_EDITOR)
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UI_Manager),true)]
public class UI_MangerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Test Notification"))
        {
            var uiManager = (UI_Manager) target;
            uiManager.Notification("Test Button ",UI_Manager.NotificationTypes.PaymentDone);
        }
    }
}

[CustomEditor(typeof(CustomerManager),true)]
public class CustomerManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Add Customer"))
        {
            var customerManager = (CustomerManager) target;

            customerManager.AddNewCustomer(customerManager.newCustomer);
        }

        if (GUILayout.Button("Clear Customer List"))
        {
            var customerManager = (CustomerManager) target;

            customerManager.ClearAll();
        }
    }
}

[CustomEditor(typeof(SupplierManager),true)]
public class SupplierManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Add Supplier"))
        {
            var supplierManager = (SupplierManager) target;

        //    supplierManager.AddNewSupplier(supplierManager.newSupplier);
        }

        if (GUILayout.Button("Clear Supplier List"))
        {
            var supplierManager = (SupplierManager) target;

            supplierManager.ClearAll();
        }
    }
}

[CustomEditor(typeof(CustomerStatement_Manager),true)]
public class CustomerStatement_ManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Add Statement"))
        {
            var customerStatementManager = (CustomerStatement_Manager) target;
            var n= new Customer
            {
                Name = "Tester",
                Cost = 10000,
                Quantity = 10,
                Rating = 5,
                CreditPeriod = 0,
                CreditValue = 0,
                orderType = Customer.OrderType.Cash
            };
            var cst = new CustomerStatement
            {
                customer = n,
                OrderID = Random.Range(111,999),
                TransactionDate = "01-01-2022"
            };
            customerStatementManager.AddToStatement(cst);
        }
        
        if (GUILayout.Button("Clear Statement"))
        {
            var supplierManager = (CustomerStatement_Manager) target;

            supplierManager.ClearStatement();
        }
    }
}

[CustomEditor(typeof(SupplierStatementManager),true)]
public class SupplierStatement_ManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Add Statement"))
        {
            var customerStatementManager = (SupplierStatementManager) target;
            var n= new Supplier()
            {
                Name = "Tester",
                Cost = 10000,
                RawMaterials = 10,
                Rating = 5
            };
            var cst = new SupplierStatement()
            {
                supplier = n,
                OrderID = Random.Range(111,999),
                TransactionDate = "01-01-2022"
            };
            customerStatementManager.AddToStatement(cst);
        }
        
        if (GUILayout.Button("Clear Statement"))
        {
            var supplierManager = (SupplierStatementManager) target;

            supplierManager.ClearStatement();
        }
    }
}
#endif