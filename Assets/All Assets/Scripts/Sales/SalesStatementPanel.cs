using TMPro;
using UnityEngine;


    public class SalesStatementPanel : MonoBehaviour
    {
        public TMP_Text OrderNumber_Text;
        public TMP_Text Name;
        public TMP_Text Time;
        public TMP_Text Quantity;
        public TMP_Text Cost;
        public TMP_Text PaymentMethod;
       
        public void CreateTransaction(SalesStatement statement)
        {
            var customer = statement.customer;
            OrderNumber_Text.text = statement.OrderID.ToString();
            Time.text = statement.TransactionDate;
            Name.text = customer.Name;
            Quantity.text = customer.Quantity.ToString();
            Cost.text = customer.Cost.ToString();
            PaymentMethod.text = customer.orderType.ToString();
        }   
    }
