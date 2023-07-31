using TMPro;
using UnityEngine;
public class AccountReceivableStatement_Panel : MonoBehaviour
    {
        public TMP_Text OrderNumber_Text;
        public TMP_Text Name;
        public TMP_Text Time;
        public TMP_Text Quantity;
        public TMP_Text Cost;
        public TMP_Text DueDate;
        public void CreateTransaction(CreditTransaction statement)
        {
            var supplier = statement.customer;
            OrderNumber_Text.text = statement.OrderID.ToString();
            Time.text = statement.TransactionDate;
            Name.text = supplier.Name;
            Quantity.text = supplier.Quantity.ToString();
            Cost.text = supplier.Cost.ToString();
            DueDate.text =statement.DueDate.ToString();
        }
    }
