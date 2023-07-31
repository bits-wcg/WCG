using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager ins;
    private void Awake()
    {
        ins = this;
    }
}
