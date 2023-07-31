using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WareHouseController : MonoBehaviour
{
    public float percentage;

    public float CurrentFGCount;

    [SerializeField] private List<SpriteRenderer> Scooters = new List<SpriteRenderer>();

    private Coroutine cal_Percentage;

    private void Start()
    {
        StartCoroutine(CalculatePercentage());
    }

    private IEnumerator CalculatePercentage()
    {
        while (true)
        {
            if (
                Math.Abs(percentage - ((GameManager.Instance.gameParameters.AvailableFinishedGoods /
                                        GameManager.Instance.gameParameters.MaximumFinishedGoods) * 100)) < .00001f)
                continue;
            ResetInventory();

            percentage = (GameManager.Instance.gameParameters.AvailableFinishedGoods /
                          GameManager.Instance.gameParameters.MaximumFinishedGoods) * 100;
//            Debug.Log(percentage);
            CurrentFGCount = Scooters.Count;

            var scooter = (int) Mathf.Round(CurrentFGCount / 100f * percentage);
            if (scooter > Scooters.Count - 1)
            {
                scooter = Scooters.Count - 1;
            }

            for (var i = 0; i < scooter; i++)
            {
                Scooters[i].gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(0.75f);

        }
    }

    private void ResetInventory()
        {
            foreach (var t in Scooters)
            {
                t.gameObject.SetActive(false);
            }
        }
    }