using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RawMaterial
{
    [Serializable]
    public class RawMaterialLog
    {
        public int count;
        public string purchaseDate;
        public int unitCost;
        public string soldBy;
    }

    public class RawMaterialManager : MonoBehaviour
    {
        public static RawMaterialManager Instance;
        public float processingRawMaterialValue;

        private void Awake()
        {
            Instance = this;
        }

        public List<RawMaterialLog> rawMaterialLogs = new List<RawMaterialLog>();
        public int RawMaterialValue;
        public int RawMaterialCount;


        private void FixedUpdate()
        {
            //  if(GameManager.Instance.isStarted)
            //   CheckValue();
        }

        public void CheckValue()
        {
            RawMaterialValue = 0;
            RawMaterialCount = 0;
            foreach (var raw in rawMaterialLogs)
            {
                RawMaterialValue += raw.count * raw.unitCost;
                RawMaterialCount += raw.count;
                Debug.Log($"<color=yellow>{RawMaterialCount},{RawMaterialValue}</color>");
            }

            var ARM = rawMaterialLogs.Sum(raw => raw.count);

            GameManager.Instance.gameParameters.AvailableRawMaterial = ARM;
            RawMaterialValue = (int)(RawMaterialValue + processingRawMaterialValue);

            GameManager.Instance.gameParameters.TotalRawMaterialValue = fixedRawMaterialValue *
                                                                        (RawMaterialCount + GameManager.Instance
                                                                            .gameParameters.UnitsProducedPerCycle);
        }

        public int fixedRawMaterialValue;

        public void CalculateFixedRawMaterialPrice(bool firstBuy)
        {
            CheckValue();
            if(firstBuy)
            fixedRawMaterialValue =(int) (RawMaterialValue) /
                                    (RawMaterialCount);
            else
            {
                fixedRawMaterialValue =(int) (RawMaterialValue) /
                                       (RawMaterialCount+GameManager.Instance
                                           .gameParameters.UnitsProducedPerCycle);
            }
            GameManager.Instance.gameParameters.FixedRawMaterialPrice = fixedRawMaterialValue;
            Debug.Log($"<color=blue>{RawMaterialCount},{RawMaterialValue}={fixedRawMaterialValue} {(RawMaterialValue+processingRawMaterialValue) / (RawMaterialCount + GameManager.Instance.gameParameters.UnitsProducedPerCycle)}</color>");
        }

        public void AddRawMaterial(int count, int unitCost, string purchaseDate, string soldBy,bool firstBuy)
        {
            var r = new RawMaterialLog
            {
                count = count,
                unitCost = unitCost,
                purchaseDate = purchaseDate,
                soldBy = soldBy
            };
            rawMaterialLogs.Add(r);
            CheckValue();
            CalculateFixedRawMaterialPrice(firstBuy);
        }

        public void RemoveRawMaterial(int count)
        {
            processingRawMaterialValue = 0;
            var r_count = count;
            foreach (var raw in rawMaterialLogs)
            {
                if (raw.count >= r_count)
                {
                    raw.count -= r_count;
                    var cost = r_count * raw.unitCost;
                    processingRawMaterialValue += cost;
                    Debug.Log("Sent Required Raw Material for month");
                    break;
                }
                else
                {
                    r_count -= raw.count;
                    var cost = raw.count * raw.unitCost;
                    processingRawMaterialValue += cost;
                    Debug.Log($"Sent Partial Raw Material of {raw.count} required for month");
                    raw.count = 0;
                }
            }

            for (var i = rawMaterialLogs.Count - 1; i >= 0; i--)
            {
                if (rawMaterialLogs[i].count == 0)
                {
                    rawMaterialLogs.Remove(rawMaterialLogs[i]);
                }
            }
            CheckValue();
        }
    }
}