 using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class AdminCustomer : MonoBehaviour
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

        public Customer customer;

        public Button[] Stars = new Button[5];
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
            // Name.text = "Customer " + (AdminManager.Instance.AdminParameters.AvailableCustomerList.Count + 1);
            var n = Random.Range(100, 300);
            Cost_Tmp.text = (n * 10).ToString();
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

        public void Add(Customer _customer)
        {
            customer = _customer;
            Name.text = customer.Name;

            Cost_Tmp.text = (customer.Quantity * 10).ToString();
            RawMaterial.text = customer.Quantity.ToString();
            Rating.text = customer.Rating.ToString();
            for (var i = 0; i < customer.Rating; i++)
            {
                Stars[i].image.sprite = goldStar;
            }
            Credit.value = customer.CreditValue;

            togglerCredit.SetActive(_customer.orderType==Customer.OrderType.Credit);
            toggler.SetActive(false);
            
            save();
        }

        public void MakeCreditOrder()
        {
            customer.orderType = Customer.OrderType.Credit;
            Credit.value = 0;
        }

        public void NotCreditOrder()
        {
            customer.orderType = Customer.OrderType.Cash;
        }
        public void Confirm()
        {
            if (Cost_Tmp.text == ""||RawMaterial.text==""||Name.text=="") 
                return;
            
            toggler.SetActive(false);
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
          var  _customer = new Customer()
            {
                Cost = int.Parse(Cost_Tmp.text),
                Name = Name.text,
                Rating = int.Parse(Rating.text),
                Quantity = int.Parse(RawMaterial.text),
                CreditValue = Credit.value,
                orderType = customer.orderType,
                CreditPeriod = creditTime
            };
          customer = _customer;
            AdminManager.Instance.AdminParameters.AvailableCustomerList.Add(customer);
            AdminManager.Instance.Upload();
            save();
            ConfirmBtn.gameObject.SetActive(false);
        }

        private void save()
        {
            Name.interactable = false;
            Cost_Tmp.interactable = false;
            RawMaterial.interactable = false;
            Rating.interactable = false;
            Credit.interactable = false;
            foreach (var star in Stars)
            {
                star.interactable = false;
            }
            ConfirmBtn.gameObject.SetActive(false);
            DeleteBtn.gameObject.SetActive(true);
            
        }

        public void Delete()
        {
            AdminManager.Instance.AdminParameters.AvailableCustomerList.Remove(customer);
            AdminManager.Instance.Upload();
            Destroy(gameObject);
        }
    }
}