using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerTesting : MonoBehaviour
{
    public int fx = 100;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var fxx = fx + fx / (Random.Range(
            1, 5));
        Debug.Log($"Production Cost {fx} Selling Price {fxx} Profir {fxx - fx}");
    }
}