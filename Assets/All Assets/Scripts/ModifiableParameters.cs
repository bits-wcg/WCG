
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using Sirenix.OdinInspector;

public class ModifiableParameters : MonoBehaviour
{
    public TMP_InputField StartYear;
    public TMP_InputField InitialCost;
    public TMP_InputField UsefulLife;
    public TMP_InputField UnitsProducedPerCycle;
    public TMP_InputField CyclesPerMonth;
    public TMP_InputField InitialRawMaterial;
    public TMP_InputField InitialRawMaterialCost;
    public TMP_InputField InitialBankBalance;
    public TMP_InputField InitialFinishedGoods;
    public TMP_InputField InitialFinishedGoodsCost;
    public TMP_InputField MaximumRawMaterial;
    public TMP_InputField MaximumFinishedGoods;
    public TMP_InputField ProductionCostPerCycle;
    public TMP_InputField InitialRatio;
    public TMP_InputField FixedProcessingCharge;
    public TMP_InputField InterestRateOnLoan;
    public TMP_InputField TaxRate;
    public TMP_InputField Depreciation;
    public TMP_InputField FastMode;
    public TMP_InputField MediumMode;
    public TMP_InputField SlowMode;
    public bool AdminUser;

    //private bool ServerCallBack = false;

    private IEnumerator Start()
    {
        if (AdminUser)
        {
            //ServerManager.instance.SaveData(GameManager.Instance.dummyData,()=>{Debug.Log("Dummy Updated");},()=>{Debug.Log("Dummy failed");});
            ServerManager.instance.FetchData((() => { Debug.Log("Download Success"); }),
                (() => { Debug.Log("Download Failed"); }));
            while (!ServerManager.instance.Received)
            {
                Debug.Log("Waiting for Server Data Student");
                yield return null;
            }

            yield return new WaitForSeconds(1f);
            
//#if UNITY_EDITOR || UNITY_WEBGL
    //        var n = JsonUtility.FromJson<GameParameters>(GameManager.Instance.dummyData);
//#else
           var n = JsonUtility.FromJson<GameParameters>(ServerManager.instance.ReceivedData);
//#endif
           
            Debug.Log(ServerManager.instance.ReceivedData);
            GameManager.Instance.gameParameters = n;
        }

        //GameManager.Instance.Starter();
        InitialCost.text = GameManager.Instance.gameParameters.CostOfTheFactory.ToString();
        UsefulLife.text = GameManager.Instance.gameParameters.UsefulLife.ToString();
        UnitsProducedPerCycle.text = GameManager.Instance.gameParameters.UnitsProducedPerCycle.ToString();
        CyclesPerMonth.text = GameManager.Instance.gameParameters.CyclesPerMonth.ToString();
        InitialRawMaterial.text = GameManager.Instance.gameParameters.InitialRawMaterial.ToString();
        InitialRawMaterialCost.text = GameManager.Instance.gameParameters.InitialRawMaterialPrice.ToString();
        InitialBankBalance.text = GameManager.Instance.gameParameters.InitialBankBalance.ToString();
        InitialFinishedGoods.text = GameManager.Instance.gameParameters.InitialFinishedGoods.ToString();
        InitialFinishedGoodsCost.text = GameManager.Instance.gameParameters.InitialFinishedGoodsPrice.ToString();
        MaximumRawMaterial.text = GameManager.Instance.gameParameters.MaximumRawMaterial.ToString();
        MaximumFinishedGoods.text = GameManager.Instance.gameParameters.MaximumFinishedGoods.ToString();
        ProductionCostPerCycle.text = GameManager.Instance.gameParameters.ProductionCostPerCycle.ToString();
        InitialRatio.text = GameManager.Instance.gameParameters.Initial_DE_Ratio.ToString();
        FixedProcessingCharge.text = GameManager.Instance.gameParameters.FixedOperatingCharge.ToString();
        InterestRateOnLoan.text = GameManager.Instance.gameParameters.InterestRateOnLoan.ToString();
        TaxRate.text = GameManager.Instance.gameParameters.TaxRate.ToString();
        if(Depreciation)
        Depreciation.text = GameManager.Instance.gameParameters.Depreciation.ToString();
        if (AdminUser)
        {
            StartYear.text = GameManager.Instance.gameParameters.startYear.ToString();
            FastMode.text = (GameManager.Instance.gameParameters.Fast ).ToString();
            MediumMode.text = (GameManager.Instance.gameParameters.Medium ).ToString();
            SlowMode.text = (GameManager.Instance.gameParameters.Slow ).ToString();
        }
    }

    public void C_StartYear(string value)
    {
        AdminManager.Instance.Exit.SetActive(false);
        
        if (AdminUser)
        {
            if(int.TryParse(value, out var year))
            {
                if (year is >= 2000 and <= 2100)
                {
                    AdminManager.Instance.AdminParameters.startYear = year;
                }
                else
                {
                    AdminManager.Instance.AdminParameters.startYear = 2023;
                    StartYear.text = "2023";
                }
            }
            else
            {
                Debug.LogError("NOT VALID YEAR");
            }
        }
        else
            GameManager.Instance.gameParameters.startYear = int.Parse(value);
    }
    public void C_InitialCost(string value)
    {
        AdminManager.Instance.Exit.SetActive(false);
        if (AdminUser)
            AdminManager.Instance.AdminParameters.CostOfTheFactory = int.Parse(value);
        else
            GameManager.Instance.gameParameters.CostOfTheFactory = int.Parse(value);
    }

    public void C_UsefulLife(string value)
    {
        AdminManager.Instance.Exit.SetActive(false);
        if (AdminUser)
            AdminManager.Instance.AdminParameters.UsefulLife = int.Parse(value);
        else
            GameManager.Instance.gameParameters.UsefulLife = int.Parse(value);
    }

    public void C_UnitsProducedPerCycle(string value)
    {
        AdminManager.Instance.Exit.SetActive(false);
        if (AdminUser)
            AdminManager.Instance.AdminParameters.UnitsProducedPerCycle = int.Parse(value);
        else
            GameManager.Instance.gameParameters.UnitsProducedPerCycle = int.Parse(value);
    }

    public void C_CyclesPerMonth(string value)
    {
        AdminManager.Instance.Exit.SetActive(false);
        if (AdminUser)
            AdminManager.Instance.AdminParameters.CyclesPerMonth = int.Parse(value);
        else
            GameManager.Instance.gameParameters.CyclesPerMonth = int.Parse(value);
    }

    public void C_InitialRawMaterial(string value)
    {
        if (AdminUser)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.InitialRawMaterial = int.Parse(value);
            AdminManager.Instance.AdminParameters.AvailableRawMaterial = int.Parse(value);
        }
        else
        {
            GameManager.Instance.gameParameters.InitialRawMaterial = int.Parse(value);
            GameManager.Instance.gameParameters.AvailableRawMaterial = int.Parse(value);
        }
    }
    public void C_InitialRawMaterialPrice(string value)
    {
        if (AdminUser)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.InitialRawMaterialPrice = int.Parse(value);
            
        }
        else
        {
            GameManager.Instance.gameParameters.InitialRawMaterialPrice = int.Parse(value);
            
        }
    }
    public void C_InitialBankBalance(string value)
    {
        if (AdminUser)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.InitialBankBalance = int.Parse(value);
            AdminManager.Instance.AdminParameters.AmountInBank = int.Parse(value);
        }
        else
        {
            GameManager.Instance.gameParameters.InitialBankBalance = int.Parse(value);
            GameManager.Instance.gameParameters.AmountInBank = int.Parse(value);
        }
    }

    public void C_InitialFinishedGoods(string value)
    {
        if (AdminUser)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.InitialFinishedGoods = int.Parse(value);
            AdminManager.Instance.AdminParameters.AvailableFinishedGoods = int.Parse(value);
        }
        else
        {
            GameManager.Instance.gameParameters.InitialFinishedGoods = int.Parse(value);
            GameManager.Instance.gameParameters.AvailableFinishedGoods = int.Parse(value);
        }
    }
    public void C_InitialFinishedGoodsPrice(string value)
    {
        if (AdminUser)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.InitialFinishedGoodsPrice = int.Parse(value);
            
        }
        else
        {
            GameManager.Instance.gameParameters.InitialFinishedGoodsPrice = int.Parse(value);
           
        }
    }
    public void C_MaximumRawMaterial(string value)
    {
        if (AdminUser)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.MaximumRawMaterial = int.Parse(value);
        }
        else
            GameManager.Instance.gameParameters.MaximumRawMaterial = int.Parse(value);
    }

    public void C_MaximumFinishedGoods(string value)
    {
        if (AdminUser)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.MaximumFinishedGoods = int.Parse(value);
        }
        else
            GameManager.Instance.gameParameters.MaximumFinishedGoods = int.Parse(value);
    }

    public void C_ProductionCostPerCycle(string value)
    {
        if (AdminUser)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.ProductionCostPerCycle = int.Parse(value);
        }
        else
            GameManager.Instance.gameParameters.ProductionCostPerCycle = int.Parse(value);
    }

    public void C_InitialRatio(string value)
    {
        if (AdminUser)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.Initial_DE_Ratio = int.Parse(value);
        }
        else
            GameManager.Instance.gameParameters.Initial_DE_Ratio = int.Parse(value);
    }

    public void C_FixedProcessingCharge(string value)
    {
        if (AdminUser)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.FixedOperatingCharge = int.Parse(value);
        }
        else
            GameManager.Instance.gameParameters.FixedOperatingCharge = int.Parse(value);
    }

    public void C_InterestRateOnLoan(string value)
    {
        AdminManager.Instance.Exit.SetActive(false);
        if (AdminUser)
            AdminManager.Instance.AdminParameters.InterestRateOnLoan = int.Parse(value);
        else
            GameManager.Instance.gameParameters.InterestRateOnLoan = int.Parse(value);
    }

    public void C_TaxRate(string value)
    {
        AdminManager.Instance.Exit.SetActive(false);
        if (AdminUser)
            AdminManager.Instance.AdminParameters.TaxRate = int.Parse(value);
        else
            GameManager.Instance.gameParameters.TaxRate = int.Parse(value);
    }
    public void C_Depreciation(string value)
    {
        AdminManager.Instance.Exit.SetActive(false);
        if (AdminUser)
            AdminManager.Instance.AdminParameters.Depreciation = int.Parse(value);
        else
            GameManager.Instance.gameParameters.Depreciation = int.Parse(value);
    }

    public void C_FastMode(string value)
    {
        AdminManager.Instance.Exit.SetActive(false);
        if (AdminUser)
        {
            var n = int.Parse(value);
            if (n <  AdminManager.Instance.AdminParameters.Medium)
            {
                n =  AdminManager.Instance.AdminParameters.Medium+1;
            }

            if (n > 100)
            {
                n = 100;
            }
            FastMode.text = n.ToString();
            AdminManager.Instance.AdminParameters.Fast = n;
        }
    }

    public void C_MediumMode(string value)
    {
        AdminManager.Instance.Exit.SetActive(false);
        if (AdminUser)
        {
            var n = int.Parse(value);
            if (n <  AdminManager.Instance.AdminParameters.Slow)
            {
                n =  AdminManager.Instance.AdminParameters.Slow+1;
            }

            if (n >  AdminManager.Instance.AdminParameters.Fast)
            {
                n =  AdminManager.Instance.AdminParameters.Fast-1;
            }
        
            MediumMode.text = n.ToString();
          
            AdminManager.Instance.AdminParameters.Medium = n;
        }
    }

    public void C_SlowMode(string value)
    {
        AdminManager.Instance.Exit.SetActive(false);
        if (AdminUser)
        {
            var n = int.Parse(value);
           
            if (n < 1 )
            {
                n =1 ;
            }

            if (n > AdminManager.Instance.AdminParameters.Medium)
            {
                n =AdminManager.Instance.AdminParameters.Medium-1 ;
            }
            SlowMode.text = n.ToString();
            AdminManager.Instance.AdminParameters.Slow = n;
        }
    }


    #region Editor

    [TableList] public List<ModifiableValues> modifiableValuesList = new List<ModifiableValues>();

    #endregion
}