using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorRandomize : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(RandomColor),5,5);
    }

    void RandomColor()
    {
        spriteRenderer.color = Random.ColorHSV();
    }
}
