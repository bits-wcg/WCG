using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstanceTesting : MonoBehaviour
{

    private static InstanceTesting Instance;

    public static InstanceTesting instance
    {
        get
        {
            Instance = FindObjectOfType<InstanceTesting>();
            if (Instance == null)
            {
                Instance = new GameObject("Saver").AddComponent<InstanceTesting>();
                DontDestroyOnLoad(Instance);
            }
            return Instance;
        }
    }
}
