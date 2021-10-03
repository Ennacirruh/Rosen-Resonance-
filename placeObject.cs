using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class placeObject : MonoBehaviour
{
    public GameObject placer;

    public void place(bool enable)
    {
        this.GetComponent<MeshRenderer>().enabled = enable;
    }
}
