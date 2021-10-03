using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionEngine : MonoBehaviour
{
    public List<GameObject> mainDimension = new List<GameObject>();
    public List<GameObject> blueDimension = new List<GameObject>();
    public List<GameObject> greenDimension = new List<GameObject>();
    public List<GameObject> purpleDimension = new List<GameObject>();
    public List<GameObject> lightDimension = new List<GameObject>();
    public List<GameObject> darkDimension = new List<GameObject>();
    public GameObject enemy;
    List<List<GameObject>> allDimensions = new List<List<GameObject>>();
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        allDimensions.Add(mainDimension);
        allDimensions.Add(blueDimension);
        allDimensions.Add(greenDimension);
        allDimensions.Add(purpleDimension);
        allDimensions.Add(lightDimension);
        allDimensions.Add(darkDimension);
        switchDimension(mainDimension);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= Mathf.Clamp( 10 - GetComponent<PlayerEngine>().difficulty,0,9))
        {
            if (GetComponent<PlayerEngine>().difficulty != 0)
            {
                GameObject newEnemey = Instantiate(enemy);
                newEnemey.SetActive(true);

                time = 0;
            }
        }
    }

    public void switchDimension(List<GameObject> selection)
    {
        foreach (List<GameObject> dimension in allDimensions)
        {
            if (dimension != selection)
            {
                foreach (GameObject obj in dimension)
                {
                    if (obj.GetComponent<MeshRenderer>() == null)
                    {
                        obj.SetActive(false);
                    }
                    else
                    {
                        obj.GetComponent<MeshRenderer>().enabled = false;
                    }
                }
            }
            else
            {
                foreach (GameObject activate in selection)
                {
                    if (activate.GetComponent<MeshRenderer>() == null)
                    {
                        activate.SetActive(true);
                    }
                    else
                    {
                        activate.GetComponent<MeshRenderer>().enabled = true;
                    }
                }
            }
        }
    }
}
