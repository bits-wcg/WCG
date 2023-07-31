using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static Custom_Func_4040;


public class UI_Manager : MonoBehaviour
{
    public static UI_Manager Instance;

    //  public GameObject notificationPrefab;
    public Text Notification_Text;
    public Text Notification_P_Text;

    public UnityEvent FireNotification = new();
    public UnityEvent FirePaymentDoneNotification = new();

    [Space(10)] [Header("Customer")] public GameObject Customer_Template_Prefab;
    public GameObject Customer_ParentObject;
    public List<GameObject> Customer_List;
    public GameObject CustomerPanel;

    [Space(10)] [Header("Supplier")] public GameObject Supplier_Template_Prefab;
    public GameObject Supplier_ParentObject;
    public List<GameObject> Supplier_List;
    public GameObject SupplierPanel;

    [Space(10)] public List<GameObject> GameHUDPanels;
    public List<GameObject> TutorialPanels;

    public GameObject LowWarningRawMaterialPanel;
    public TMP_Text LowWarningRawMaterialPanelText;
    public GameObject LowWarningCashPanel;
    public GameObject LowWarning_FG_Panel;
    public GameObject FullWarning_RM_Panel;
    public GameObject GameOverPanel;
    public GameObject GameWinPanel;

    public void LowRawMaterialWarning(string message)
    {
      //  Notification("Low on Raw Material", NotificationTypes.lowWarning);
        LowWarningRawMaterialPanel.SetActive(true);
        LowWarningRawMaterialPanelText.text = message;
    }
    public enum NotificationTypes
    {
        lowWarning,
        PaymentDone,
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        RefreshCustomerPanel();
    }

    public void Notification(string message, NotificationTypes types)
    {
        if (types == NotificationTypes.lowWarning)
        {
            FireNotification?.Invoke();
            Notification_Text.text = message;
            Debug.Log($"Notification message : {message}");
        }
        else
        {
            FirePaymentDoneNotification?.Invoke();
            Notification_P_Text.text = message;
            Debug.Log($"Notification message : {message}");
        }
    }

    public GameManager gameManager;

    #region Customer and Supplier Panel

    public CustomerManager customerManager;
    public SupplierManager supplierManager;

    public void RefreshCustomerPanel()
    {
        var customers = gameManager.gameParameters.AvailableCustomerList;

        Customer_List = GetChildren(Customer_ParentObject.transform); //Refreshes the List

        Debug.Log($"<color=Green> Found {customers.Count} Customer Adding to Customer Panel</color>");

        foreach (var customer in Customer_List)
        {
            DestroyImmediate(customer);
        }

        foreach (var customer in customers)
        {
            var t = Instantiate(Customer_Template_Prefab, Customer_ParentObject.transform);
            t.GetComponent<CustomerPanel>().FillData(customer);
            t.GetComponent<CustomerPanel>().customerManager = customerManager;
        }

        Customer_List = GetChildren(Customer_ParentObject.transform); //Refreshes the List
    }


    public void RefreshSupplierPanel()
    {
        var suppliers = gameManager.gameParameters.AvailableSupplierList;

        Supplier_List = GetChildren(Supplier_ParentObject.transform); //Refreshes the List

        Debug.Log($"<color=Green> Found {suppliers.Count} Suppliers Adding to Supplier Panel</color>");

        foreach (var supplier in Supplier_List)
        {
            DestroyImmediate(supplier);
        }

        foreach (var supplier in suppliers)
        {
            var t = Instantiate(Supplier_Template_Prefab, Supplier_ParentObject.transform);
            t.GetComponent<SupplierPanel>().FillData(supplier);
            t.GetComponent<SupplierPanel>().supplierManager = supplierManager;
        }

        Supplier_List = GetChildren(Supplier_ParentObject.transform); //Refreshes the List
    }

    #endregion

    public void TurnOnGameHUD()
    {
        foreach (var panel in GameHUDPanels)
        {
            panel.SetActive(true);
        }

        foreach (var panel in TutorialPanels)
        {
            panel.SetActive(false);
        }
    }

    public void OpenSupplierPanel()
    {
        SupplierPanel.SetActive(true);
        GameManager.Instance.PauseTimer();
    }

    public void CloseSupplierPanel()
    {
        SupplierPanel.SetActive(false);
        GameManager.Instance.ResumeTimer();
    }

    public void OpenCustomerPanel()
    {
        CustomerPanel.SetActive(true);
        GameManager.Instance.PauseTimer();
    }

    public void CloseCustomerPanel()
    {
        CustomerPanel.SetActive(false);
        GameManager.Instance.ResumeTimer();
    }

    public TMP_Text GameOverMessage;
    public void GameOver(string message)
    {
        Timer.Instance.startTimer = false;
        GameManager.Instance.isGameOver = true;
        Debug.LogError("GAME OVER");
        GameOverMessage.text = message;
        GameOverPanel.SetActive(true);
    }

    public void GameWin()
    {
        GameWinPanel.SetActive(true);
    }

    public void LowOnCashNotification()
    {
        Notification("Low on Cash",NotificationTypes.lowWarning);
    }
}