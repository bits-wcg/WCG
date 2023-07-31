using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UITabToNext : MonoBehaviour
{
    public List<GameObject> Parents;
    private Mod_Values[] _InputFields;
    public List<Mod_Values> InputFields;
   
    public int SelectedInputField;

    private void Start()
    {
        foreach (var p in Parents)
        {
            _InputFields = p.GetComponentsInChildren<Mod_Values>();
            InputFields.AddRange(_InputFields.ToList());
        }
        
        var i = 0;
        foreach (var Ifield in InputFields)
        {
            var i1 = i;
            Ifield.inputField.onSelect.AddListener((arg0 => { SelectedInputField = i1; }));
            i++;
        }
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (SelectedInputField < InputFields.Count - 1)
            {
                SelectedInputField++;
            }
            else
                SelectedInputField = 0;
             

            InputFields[SelectedInputField].inputField.Select();
        }
    }
}