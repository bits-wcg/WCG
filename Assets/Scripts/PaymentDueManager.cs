using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class PaymentDueManager : MonoBehaviour
{
    public TMP_Text MessageText;
    public TMP_Text MessageFooterText;
    public TMP_Text SupplierNameText;
    public TMP_Text RW_QuantityText;
    public Supplier supplier;
    public GameObject StatementPanel;
    public GameObject Panel;

    public void SetUpDuePanel(Supplier _supplier, GameObject panel)
    {
        Panel = panel;
        supplier = _supplier;
        MessageText.text = $"{supplier.Cost} INR <size=30>in Due</size>";
        SupplierNameText.text = $"SUPPLIER NAME: {supplier.Name}";
        RW_QuantityText.text = $"";
    }

    public void PayNow()
    {
        if (GameManager.Instance.gameParameters.AmountInBank > supplier.Cost)
        {
            if (GameManager.Instance.gameParameters.AccountPayable > supplier.Cost)
                GameManager.Instance.gameParameters.AccountPayable -= supplier.Cost;
            else
            {
                GameManager.Instance.gameParameters.AccountPayable -=
                    GameManager.Instance.gameParameters.AccountPayable;
            }

            GameManager.Instance.gameParameters.AmountInBank -= supplier.Cost;
            FinancialManager.Instance.ClearDue(this);

            Destroy(gameObject);
            Destroy(StatementPanel);
        }
        else
        {
            MessageFooterText.text = "NOT ENOUGH MONEY TO CLEAR THE DUE AMOUNT";
            Invoke(nameof(HideInSec), 1);
        }
    }

    public void HideInSec()
    {
        MessageFooterText.text = "";
    }

    public void RemindLater()
    {
        Debug.Log("Attempting to Buy on credit");
        GameObject o;
        (o = gameObject).SetActive(false);
        Panel.GetComponent<SupplierPanel>().customUpdateLatePay();
        Debug.Log(
            $"<color=red> Missed Payment Due for {supplier.Name}, Rating reduce from {supplier.Rating + 1} to {supplier.Rating}</color>");

        GameManager.Instance.AddAgain(o, 15, this, supplier);
    }

    public void RemoveNow()
    {
    }
}