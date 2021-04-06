using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rotateScript : MonoBehaviour
{
    public float rate = 1.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, rate);
    }
}
