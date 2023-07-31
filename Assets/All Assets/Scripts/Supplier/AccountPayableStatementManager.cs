using System.Collections.Generic;
using UnityEngine;
using static Custom_Func_4040;

    public class AccountPayableStatementManager : MonoBehaviour
    {
        public static AccountPayableStatementManager Instance;
        private List<AccountPayableStatement> AccountPayableStatements = new List<AccountPayableStatement>();
        public List<GameObject> AccountPayableStatementObjects_List;


        public GameObject AccountPayableStatement_Prefab;
        public GameObject AccountPayableStatement_Parent;

        private void Start()
        {
            Instance = this;
        }

        public AccountPayableStatementPanel AddToStatement(Supplier _supplier)
        {
            Debug.Log("CP "+_supplier.CreditPeriod);
            var d = Timer.Instance.dateTimeRef.AddDays(_supplier.CreditPeriod);
            var cst = new AccountPayableStatement()
            {
                supplier = _supplier,
                OrderID = AccountPayableStatements.Count + 1,
                TransactionDate = Timer.Instance.currentGameDate,
                DueDate = d.ToString("dd/MM/yyyy")
            };
            AccountPayableStatements.Add(cst);

            var t = Instantiate(AccountPayableStatement_Prefab, AccountPayableStatement_Parent.transform);
            t.GetComponent<AccountPayableStatementPanel>().CreateTransaction(cst);
            return t.GetComponent<AccountPayableStatementPanel>();
        }

        public void AddToStatement(AccountPayableStatement cst)
        {
            AccountPayableStatements.Add(cst);

            var t = Instantiate(AccountPayableStatement_Prefab, AccountPayableStatement_Parent.transform);
            t.GetComponent<AccountPayableStatementPanel>().CreateTransaction(cst);
        }

        public void ClearStatement()
        {
            AccountPayableStatementObjects_List = GetChildren(AccountPayableStatement_Parent.transform);
            var header = AccountPayableStatementObjects_List[0];
            foreach (var statement in AccountPayableStatementObjects_List)
            {
                DestroyImmediate(statement);
            }

            AccountPayableStatements.Clear();
            AccountPayableStatementObjects_List.Add(header);
            AccountPayableStatementObjects_List = GetChildren(AccountPayableStatement_Parent.transform);
        }
    }
