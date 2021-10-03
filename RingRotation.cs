using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingRotation : MonoBehaviour
{
    public float devience = 10f;
    public float devienceSpeed = 1f;
    public float devienceSpeed2 = 2f;


    void Update()
    {
        float offset = (Mathf.Sin(Time.time * devienceSpeed)) * devience;
        float offset2 = (Mathf.Sin(Time.time * devienceSpeed2)) * 2 * devience;
        transform.rotation = Quaternion.Euler(offset - 90f, offset2, 0f);
        
        
    }
}
