using System;
using System.Collections.Generic;
using UnityEngine;


    public class PurchaseStatementManager : MonoBehaviour
    {
        public static PurchaseStatementManager Instance;
        public List<PurchaseStatement> supplierStatements = new List<PurchaseStatement>();
        public List<GameObject> supplierStatementObjects_List;


        public GameObject supplierStatement_Prefab;
        public GameObject supplierStatement_Parent;

        private void Start()
        {
            Instance = this;
     
        }

        public void AddToStatement(Supplier _supplier)
        {
            Debug.Log("Started Adding Purchase");
            var cst = new PurchaseStatement()
            {
                supplier = _supplier,
                OrderID = supplierStatements.Count + 1,
                TransactionDate = Timer.Instance.currentGameDate
            };
            supplierStatements.Add(cst);

            var t = Instantiate(supplierStatement_Prefab, supplierStatement_Parent.transform);
            t.GetComponent<PurchasePanel>().CreateTransaction(cst);
        }
        
        
        
    }
