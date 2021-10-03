using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject engine;
    public Text text;
    public float health = 0;
    public float speed = 0;
    public int damage = 0;
    void Start()
    {
        health = 1f + engine.GetComponent<PlayerEngine>().difficulty;
        speed = (0.1f + (engine.GetComponent<PlayerEngine>().difficulty / 100f)) / 10f;
        damage = Mathf.RoundToInt(1 + engine.GetComponent<PlayerEngine>().difficulty);
        
        transform.position = new Vector3(Random.Range(-20f, 20f), Random.Range(1f, 5f), Random.Range(-20f, 20f));
    }
    void Update()
    {

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        if (engine.GetComponent<PlayerEngine>().difficulty != 0) {
            this.transform.LookAt(player.transform);
            this.transform.position += this.transform.forward * speed;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            engine.GetComponent<PlayerEngine>().health -= damage;
            text.GetComponent<Text>().text = "Health: " + engine.GetComponent<PlayerEngine>().health.ToString();
            Destroy(this.gameObject);
        }
    }
}
