using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public float speed = 1;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0f, speed, 0f);
    }
}
