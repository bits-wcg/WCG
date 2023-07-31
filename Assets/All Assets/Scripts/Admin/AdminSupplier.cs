
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class AdminSupplier : MonoBehaviour
{
    public TMP_InputField Name;
    public TMP_InputField Cost_Tmp;
    public TMP_InputField RawMaterial;
    public TMP_InputField Rating;
    public TMP_Dropdown Credit;
    public GameObject toggler;
    public GameObject togglerCredit;
    public Button ConfirmBtn;
    public Button DeleteBtn;

    private Supplier supplier=new Supplier();
    
    public Button[] Stars=new Button[5];
    public Sprite goldStar;
    public Sprite blackStar;
    
    private void Start()
    {
        Stars[0].onClick.AddListener(() =>
        {
            foreach (var s in Stars)
            {
                s.image.sprite = blackStar;
            }
            Stars[0].image.sprite = goldStar;
            Rating.text = 1.ToString();
        });
        Stars[1].onClick.AddListener(() =>
        {
            foreach (var s in Stars)
            {
                s.image.sprite = blackStar;
            }
            Stars[0].image.sprite = goldStar;
            Stars[1].image.sprite = goldStar;
            Rating.text = 2.ToString();
        });
        Stars[2].onClick.AddListener(() =>
        {
            foreach (var s in Stars)
            {
                s.image.sprite = blackStar;
            }
            Stars[0].image.sprite = goldStar;
            Stars[1].image.sprite = goldStar;
            Stars[2].image.sprite = goldStar;
            Rating.text = 3.ToString();
        });
        Stars[3].onClick.AddListener(() =>
        {
            foreach (var s in Stars)
            {
                s.image.sprite = blackStar;
            }
            Stars[0].image.sprite = goldStar;
            Stars[1].image.sprite = goldStar;
            Stars[2].image.sprite = goldStar;
            Stars[3].image.sprite = goldStar;
            Rating.text = 4.ToString();
        });
        Stars[4].onClick.AddListener(() =>
        {
            foreach (var s in Stars)
            {
                s.image.sprite = goldStar;
            }
              
            Rating.text = 5.ToString();
        });
    }
    public void Default()
    {
      //  Name.text = "Supplier " + (AdminManager.Instance.AdminParameters.AvailableSupplierList.Count + 1);
        var n = Random.Range(100, 300);
        Cost_Tmp.text = (n * AdminManager.Instance.AdminParameters.FixedRawMaterialPrice).ToString();
      //  RawMaterial.text = n.ToString();
        Rating.text = 5.ToString();
        Credit.value = 0;
        foreach (var s in Stars)
        {
            s.image.sprite = blackStar;
        }
        Stars[0].image.sprite = goldStar;
        Rating.text = 1.ToString();

    }
    public void Add(Supplier _supplier)
    {
        supplier = _supplier;
        Name.text = supplier.Name;
      
        Cost_Tmp.text = (supplier.RawMaterials * 10).ToString();
        RawMaterial.text = supplier.RawMaterials.ToString();
        Rating.text = supplier.Rating.ToString();
        for (int i = 0; i < supplier.Rating; i++)
        {
            Stars[i].image.sprite = goldStar;
        }
        
        Credit.value = supplier.CreditValue;
        togglerCredit.SetActive(supplier.orderType == Supplier.OrderType.Credit);
        toggler.SetActive(false);
        ConfirmBtn.gameObject.SetActive(false);
        DeleteBtn.gameObject.SetActive(true);
        
        Name.interactable = false;
        Cost_Tmp.interactable = false;
        RawMaterial.interactable = false;
        Rating.interactable = false;
        Credit.interactable = false;
        foreach (var star in Stars)
        {
            star.interactable = false;
        }
    }
    
    public void MakeCreditOrder()
    {
        
        supplier.orderType = Supplier.OrderType.Credit;
        
    }

    public void NotCreditOrder()
    {
        supplier.orderType = Supplier.OrderType.Cash;
    }
    public void Confirm()
    {
        if (Name.text == ""||RawMaterial.text=="") 
            return;
        var r = int.Parse(Rating.text);
        Rating.text = r switch
        {
            > 5 => 5.ToString(),
            < 1 => 1.ToString(),
            _ => Rating.text
        };
        var creditTime = Credit.value switch
        {
            0 => 30,
            1 => 60,
            2 => 90,
            _ => 30
        };
   

       var _supplier = new Supplier() {
           // Cost = int.Parse(Cost_Tmp.text),
            Name = Name.text,
            Rating =int.Parse(Rating.text),
            RawMaterials = int.Parse(RawMaterial.text),
            CreditPeriod = creditTime,
            orderType = supplier.orderType,
            CreditValue = Credit.value
            
        };
      //  SupplierManager.Instance.AddNewSupplier(sup);

        Name.interactable = false;
        Cost_Tmp.interactable = false;
        RawMaterial.interactable = false;
        Rating.interactable = false;
        Credit.interactable = false;
        supplier = _supplier;
        AdminManager.Instance.AdminParameters.AvailableSupplierList.Add(supplier);
        AdminManager.Instance.Upload();
        foreach (var star in Stars)
        {
            star.interactable = false;
        }
        ConfirmBtn.gameObject.SetActive(false);
        DeleteBtn.gameObject.SetActive(true);
        
    }

    public void Delete()
    {
        AdminManager.Instance.AdminParameters.AvailableSupplierList.Remove(supplier);
        AdminManager.Instance.Upload();
        Destroy(gameObject);
    }
}
