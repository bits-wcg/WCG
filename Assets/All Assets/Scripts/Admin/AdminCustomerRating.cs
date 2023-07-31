using System;
using TMPro;
using UnityEngine;


    public class AdminCustomerRating : MonoBehaviour
    {
        public static AdminCustomerRating Instance;
        public TMP_InputField min_RM_Cost_Text;
        public TMP_InputField max_RM_Cost_Text;


        public TMP_InputField[] max_Rating_Cost_Percent_Text;
        public TMP_InputField[] min_Rating_Cost_Percent_Text;

        private void Awake()
        {
            Instance = this;
            
        }

        public void Populate()
        {
            Debug.Log("Populating rating");
            min_RM_Cost_Text.text = AdminManager.Instance.AdminParameters.FinishedGoodsMinPrice.ToString();
            max_RM_Cost_Text.text = AdminManager.Instance.AdminParameters.FinishedGoodsMaxPrice.ToString();

            for (int i = 0; i < max_Rating_Cost_Percent_Text.Length; i++)
            {
                max_Rating_Cost_Percent_Text[i].text =
                    AdminManager.Instance.AdminParameters.CustomerOfferPricingIncreaseBys[i].max.ToString();
            }

            for (int i = 0; i < min_Rating_Cost_Percent_Text.Length; i++)
            {
                min_Rating_Cost_Percent_Text[i].text =
                    AdminManager.Instance.AdminParameters.CustomerOfferPricingIncreaseBys[i].min.ToString();
            }
        }



        public void Min_RM_Percent_5(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.CustomerOfferPricingIncreaseBys[0].min = int.Parse(value);
        }

        public void Max_RM_Percent_5(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.CustomerOfferPricingIncreaseBys[0].max = int.Parse(value);
        }

        public void Min_RM_Percent_4(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.CustomerOfferPricingIncreaseBys[1].min = int.Parse(value);
        }

        public void Max_RM_Percent_4(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.CustomerOfferPricingIncreaseBys[1].max = int.Parse(value);
        }

        public void Min_RM_Percent_3(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.CustomerOfferPricingIncreaseBys[2].min = int.Parse(value);
        }

        public void Max_RM_Percent_3(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.CustomerOfferPricingIncreaseBys[2].max = int.Parse(value);
        }

        public void Min_RM_Percent_2(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.CustomerOfferPricingIncreaseBys[3].min = int.Parse(value);
        }

        public void Max_RM_Percent_2(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.CustomerOfferPricingIncreaseBys[3].max = int.Parse(value);
        }

        public void Min_RM_Percent_1(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.CustomerOfferPricingIncreaseBys[4].min = int.Parse(value);
        }

        public void Max_RM_Percent_1(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.CustomerOfferPricingIncreaseBys[4].max = int.Parse(value);
        }
        public void Min_RM_Price(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.FinishedGoodsMinPrice = int.Parse(value);
        }
        public void Max_RM_Price(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.FinishedGoodsMaxPrice = int.Parse(value);
        }

    }
