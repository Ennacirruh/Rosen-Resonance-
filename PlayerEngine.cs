using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerEngine : MonoBehaviour
{
    public int health;
    public float difficulty = 0;
    public GameObject bullet;
    public GameObject bulletSpawnPos;
    public GameObject player;
    public GameObject lever;
    public Camera cam;
    public Text message;
    public List<float> main = new List<float>();
    public List<float> blue = new List<float>();
    public List<float> green = new List<float>();
    public List<float> purple = new List<float>();
    public List<float> light = new List<float>();
    public List<float> dark = new List<float>();
    public List<GameObject> lights = new List<GameObject>();
    public List<Material> mats = new List<Material>();
    int dimension = 0;
    // Start is called before the first frame update
    void Start()
    {
        Screen.fullScreen = true;
        foreach (Material mat in mats)
        {
            if (mat.HasProperty("Power_"))
            {
                mat.SetFloat("Power_", 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if(health <= 0)
        {
            SceneManager.LoadSceneAsync("Main");
        }
        if (Input.GetMouseButtonDown(0) && difficulty != 0)
        {
            dimension++;
            if (dimension == 6)
            {
                dimension = 0;
            }
            switch (dimension)
            {
                case 0:
                    changeDimensions(main);
                    GetComponent<DimensionEngine>().switchDimension(this.GetComponent<DimensionEngine>().mainDimension);
                    break;
                case 1:
                    changeDimensions(blue);
                    GetComponent<DimensionEngine>().switchDimension(this.GetComponent<DimensionEngine>().blueDimension);
                    break;
                case 2:
                    changeDimensions(green);
                    GetComponent<DimensionEngine>().switchDimension(this.GetComponent<DimensionEngine>().greenDimension);
                    break;
                case 3:
                    changeDimensions(purple);
                    GetComponent<DimensionEngine>().switchDimension(this.GetComponent<DimensionEngine>().purpleDimension);
                    break;
                case 4:
                    changeDimensions(light);
                    GetComponent<DimensionEngine>().switchDimension(this.GetComponent<DimensionEngine>().lightDimension);
                    break;
                case 5:
                    changeDimensions(dark);
                    GetComponent<DimensionEngine>().switchDimension(this.GetComponent<DimensionEngine>().darkDimension);
                    break;

            }

        }
        if (Input.GetMouseButtonDown(1))
        {

            int layerMask = 1 << 6;
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                if(hit.collider.gameObject != lever.gameObject)
                {
                    hit.collider.gameObject.GetComponent<Enemy>().health -= 1f;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {

            int layerMask = 1 << 6;
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, 4f, layerMask))
            {
                if (hit.collider.gameObject == lever.gameObject)
                {
                    if (difficulty == 0)
                    {
                        foreach (GameObject obj in lights)
                        {
                            obj.SetActive(true);
                        }
                    }
                    difficulty += 0.1f;
                    foreach (Material mat in mats)
                    {
                        if (mat.HasProperty("Power_")) {
                            mat.SetFloat("Power_", difficulty);
                        }
                    }
                }
            }
        }

    }
    void changeDimensions(List<float> movement)
    {
        player.GetComponent<PlayerController>().crouchSpeed = movement[0];
        player.GetComponent<PlayerController>().walkSpeed = movement[1];
        player.GetComponent<PlayerController>().sprintSpeed = movement[2];
        player.GetComponent<PlayerController>().jumpHeight = movement[3];
        player.GetComponent<PlayerController>().gravity = movement[4];
    }

}
