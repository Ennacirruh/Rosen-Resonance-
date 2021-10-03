using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class difficulty : MonoBehaviour
{
    public int strength;
    // Start is called before the first frame update
    public void escalate()
    {
        strength++;
    }
}
