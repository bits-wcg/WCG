using System;
using TMPro;
using UnityEngine;

namespace Admin
{
    public class AdminSupplierRating : MonoBehaviour
    {
        public TMP_InputField min_RM_Cost_Text;
        public TMP_InputField max_RM_Cost_Text;


        public TMP_InputField[] max_Rating_Cost_Percent_Text;
        public TMP_InputField[] min_Rating_Cost_Percent_Text;

        private void Start()
        {
            min_RM_Cost_Text.text = AdminManager.Instance.AdminParameters.RawMaterialMinPrice.ToString();
            max_RM_Cost_Text.text = AdminManager.Instance.AdminParameters.RawMaterialMaxPrice.ToString();

            for (int i = 0; i < max_Rating_Cost_Percent_Text.Length; i++)
            {
                max_Rating_Cost_Percent_Text[i].text =
                    AdminManager.Instance.AdminParameters.RW_Ratings_MinMax[i].max.ToString();
            }

            for (int i = 0; i < min_Rating_Cost_Percent_Text.Length; i++)
            {
                min_Rating_Cost_Percent_Text[i].text =
                    AdminManager.Instance.AdminParameters.RW_Ratings_MinMax[i].min.ToString();
            }
        }

        public void Min_RM_Cost(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.RawMaterialMinPrice = int.Parse(value);
        }

        public void Max_RM_Cost(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.RawMaterialMaxPrice = int.Parse(value);
        }

        public void Min_RM_Percent_5(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.RW_Ratings_MinMax[0].min = int.Parse(value);
        }

        public void Max_RM_Percent_5(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.RW_Ratings_MinMax[0].max = int.Parse(value);
        }

        public void Min_RM_Percent_4(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.RW_Ratings_MinMax[1].min = int.Parse(value);
        }

        public void Max_RM_Percent_4(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.RW_Ratings_MinMax[1].max = int.Parse(value);
        }

        public void Min_RM_Percent_3(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.RW_Ratings_MinMax[2].min = int.Parse(value);
        }

        public void Max_RM_Percent_3(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.RW_Ratings_MinMax[2].max = int.Parse(value);
        }

        public void Min_RM_Percent_2(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.RW_Ratings_MinMax[3].min = int.Parse(value);
        }

        public void Max_RM_Percent_2(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.RW_Ratings_MinMax[3].max = int.Parse(value);
        }

        public void Min_RM_Percent_1(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.RW_Ratings_MinMax[4].min = int.Parse(value);
        }

        public void Max_RM_Percent_1(string value)
        {
            AdminManager.Instance.Exit.SetActive(false);
            AdminManager.Instance.AdminParameters.RW_Ratings_MinMax[4].max = int.Parse(value);
        }
    }
}