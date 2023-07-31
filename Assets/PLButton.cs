using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PLButton : MonoBehaviour
{
    public GameObject plSheet;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ClickAction);
    }

    private void ClickAction()
    {
      //  ProfitLossManager.Instance.Reset();
        plSheet.SetActive(true);
    }
}
