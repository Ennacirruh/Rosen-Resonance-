using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.LookAt(cam.gameObject.transform);
        this.GetComponent<Rigidbody>().AddForce(-Vector3.forward * 5);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}
