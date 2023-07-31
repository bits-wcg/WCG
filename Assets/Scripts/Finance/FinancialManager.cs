using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

    public class FinancialManager : MonoBehaviour
    {
        public static FinancialManager Instance;
        public List<PaymentDueManager> ActiveDueList = new List<PaymentDueManager>();
        public List<PaymentDueManager> UpcomingDueList = new List<PaymentDueManager>();
        private void Awake()
        {
            Instance = this;
        }

        public  void MonthlyCharges()
        {
            var g = GameManager.Instance.gameParameters;
            //Debit((int) (GameManager.Instance.gameParameters.ProductionCostPerCycle * GameManager.Instance.gameParameters.CyclesPerMonth), "Total monthly production charge");
            Debit((int) (GameManager.Instance.gameParameters.FixedOperatingCharge), "Monthly Fixed Processing charge");
            
            Debit( (int) ((g.CostOfTheFactory * g.Initial_DE_Ratio / 100) * g.InterestRateOnLoan / 100) / 12, "Monthly Interest charges");
        }

        public void Debit(int amount,string madeBy)
        {
            GameManager.Instance.gameParameters.AmountInBank -= amount;

            var t = new Transaction()
            {
            Date = $"{Timer.Instance.date}/{Timer.Instance.month}/{Timer.Instance.year}",
            MadeBy = madeBy,
            Type = Transaction.TransactionType.Debit,
            TransactionAmount = amount,
            ClosingBalance = (int)GameManager.Instance.gameParameters.AmountInBank
            
            };
            Debug.Log($"<color=Red> {t.TransactionAmount} has been Debited from Account by {madeBy}</color>");
            TransactionManager.Instance.AddTransaction(t);

        }
        
        public void Credit(int amount,string madeBy,int quantity)
        {
           

            var t = new Transaction()
            {
                Date = $"{Timer.Instance.date}/{Timer.Instance.month}/{Timer.Instance.year}",
                MadeBy = madeBy,
                Type = Transaction.TransactionType.Credit,
                TransactionAmount = amount,
                quantity = quantity,
                ClosingBalance = (int)GameManager.Instance.gameParameters.AmountInBank
            };
            Debug.Log($"<color=Green> {t.TransactionAmount} has been Credited to Account</color>");
            TransactionManager.Instance.AddTransaction(t);
        }
        
        public void ClearDue(PaymentDueManager paymentDueManager)
        {
            if (UpcomingDueList.Contains(paymentDueManager))
            {
                UpcomingDueList.Remove(paymentDueManager);
            }

            ActiveDueList.Remove(paymentDueManager);
          GameManager.Instance.ResumeTimer();
        }

        public void RecordCredit(PaymentDueManager paymentDueManager)
        {
            if (  FinancialManager.Instance.ActiveDueList.Contains(paymentDueManager))
            {
                FinancialManager.Instance.ActiveDueList.Remove(paymentDueManager);
            }

            FinancialManager.Instance. UpcomingDueList.Add(paymentDueManager);
        }
        
        public void PaymentDueC(float day, Supplier supplier, AccountPayableStatementPanel panel,GameObject SupplierPanel)
        {
            StartCoroutine(PaymentDue(day, supplier, panel, SupplierPanel));
        }

        private IEnumerator PaymentDue(float day, Supplier supplier, AccountPayableStatementPanel panel,GameObject SupplierPanel)
        {
            var d = Timer.Instance.dateTimeRef.AddDays(day);

            while (d != Timer.Instance.dateTimeRef)
            {
                // Debug.Log("PaymentDue " + d + " " + " " + Timer.Instance.dateTimeRef + " " +
                //           (d != Timer.Instance.dateTimeRef));
                yield return null;
            }

            var n = Instantiate(GameManager.Instance.paymentDuePrefab, GameManager.Instance.paymentDue_Parent.transform);
            n.GetComponent<PaymentDueManager>().SetUpDuePanel(supplier,SupplierPanel);
            n.GetComponent<PaymentDueManager>().StatementPanel = panel.gameObject;
            FinancialManager.Instance.ActiveDueList.Add(n.GetComponent<PaymentDueManager>());
            UI_Manager.Instance.Notification($"Payments is in Due ",
                UI_Manager.NotificationTypes.PaymentDone);
        }
        
        
        public void CreditAfterTime(int amount, float day, Customer customer, AccountReceivableStatement_Panel panel)
        {
            StartCoroutine(CreditAfterATime(amount, day, customer, panel));
        }

  
        private static readonly int Add = Animator.StringToHash("Add");
        private IEnumerator CreditAfterATime(int amount, float day, Customer customer,
            AccountReceivableStatement_Panel panel)
        {
            Debug.Log("CreditTimeStarted");
            var d = Timer.Instance.dateTimeRef.AddDays(day);

            while (d != Timer.Instance.dateTimeRef)
            {
                var n = (Timer.Instance.dateTimeRef - d).TotalDays;
                Debug.Log($"Due date :{d} Day to Credit {-n}");

                yield return null;
            }

            Debug.Log("CreditTimeStarted Over" + d);
            UI_Manager.Instance.Notification($"{customer.Cost} Has been credited to the account by {customer.Name}",
                UI_Manager.NotificationTypes.PaymentDone);
            GameManager.Instance.gameParameters.AccountReceivable -= amount;
            GameManager.Instance.gameParameters.AmountInBank += amount;
            GameManager.Instance.Transaction.text = amount.ToString();
            GameManager.Instance.Transaction.color = Color.green;
            Destroy(panel.gameObject);
            GameManager.Instance.Transaction.gameObject.GetComponent<Animator>().SetTrigger(Add);
        }
        
    

    }
