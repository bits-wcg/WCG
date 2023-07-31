using TMPro;
using UnityEngine;

    public class PurchasePanel : MonoBehaviour
    {
        public TMP_Text OrderNumber_Text;
        public TMP_Text Name;
        public TMP_Text Time;
        public TMP_Text Quantity;
        public TMP_Text Cost;
        public TMP_Text PaymentMethod;
       
        public void CreateTransaction(PurchaseStatement statement)
        {
            Debug.Log("CreateTransaction");
            var supplier = statement.supplier;
            OrderNumber_Text.text = statement.OrderID.ToString();
            Time.text = statement.TransactionDate;
            Name.text = supplier.Name;
            Quantity.text = supplier.RawMaterials.ToString();
            Cost.text = supplier.Cost.ToString();
            PaymentMethod.text = supplier.orderType.ToString();
        }
    }
