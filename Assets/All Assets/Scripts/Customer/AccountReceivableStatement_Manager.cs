using System.Collections.Generic;
using UnityEngine;
using static Custom_Func_4040;

    public class AccountReceivableStatement_Manager : MonoBehaviour
    {
        public static AccountReceivableStatement_Manager Instance;
        public List<CreditTransaction> AccountReceivableStatements = new List<CreditTransaction>();
        public List<GameObject> AccountReceivableStatementObjects_List;


        public GameObject AccountReceivableStatement_Prefab;
        public GameObject AccountReceivableStatement_Parent;

        private void Start()
        {
            Instance = this;
        }

        public void AddToStatement(CreditTransaction creditTransaction)
        {
            var _customer = creditTransaction.customer;
                
        Debug.Log("CP "+_customer.CreditPeriod);
            var d = Timer.Instance.dateTimeRef.AddDays(_customer.CreditPeriod);
            var cst = new CreditTransaction()
            {
                customer = _customer,
                OrderID = AccountReceivableStatements.Count + 1,
                TransactionDate = Timer.Instance.currentGameDate,
                DueDate = d.ToString("dd/MM/yyyy")
            };
            AccountReceivableStatements.Add(cst);

            var t = Instantiate(AccountReceivableStatement_Prefab, AccountReceivableStatement_Parent.transform);
            t.GetComponent<AccountReceivableStatement_Panel>().CreateTransaction(cst);
            
            FinancialManager.Instance.CreditAfterTime(_customer.Cost,_customer.CreditPeriod,_customer, t.GetComponent<AccountReceivableStatement_Panel>()); //Credits After 30 Days
        }

        public void ClearStatement()
        {
            AccountReceivableStatementObjects_List = GetChildren(AccountReceivableStatement_Parent.transform);
            var header = AccountReceivableStatementObjects_List[0];
            foreach (var statement in AccountReceivableStatementObjects_List)
            {
                DestroyImmediate(statement);
            }

            AccountReceivableStatements.Clear();
            AccountReceivableStatementObjects_List.Add(header);
            AccountReceivableStatementObjects_List = GetChildren(AccountReceivableStatement_Parent.transform);
        }
    }
