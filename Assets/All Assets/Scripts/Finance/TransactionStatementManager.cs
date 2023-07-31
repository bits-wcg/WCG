
using TMPro;
using UnityEngine;

public class TransactionStatementManager : MonoBehaviour
{
        
    public TMP_Text OrderNumber_Text;
    public TMP_Text Name;
    public TMP_Text Time;
    public TMP_Text PaymentType;
    public TMP_Text Amount;
    public TMP_Text ClosingBalance;
       
    public void CreateTransaction(Transaction transaction,int Id)
    {
        OrderNumber_Text.text = Id.ToString();
        Time.text = transaction.Date;
        Name.text = transaction.MadeBy;
      
        Amount.text = transaction.TransactionAmount.ToString();

        ClosingBalance.text = transaction.ClosingBalance.ToString();
        PaymentType.text = transaction.Type.ToString();
    }
}
