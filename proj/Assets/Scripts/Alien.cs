using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public int pointValue;
    public GameObject deathExplosion;
    public AudioClip deathKnell;
    public bool isAlive  = true;
    public void Die()
    {
        AudioSource.PlayClipAtPoint(deathKnell, gameObject.transform.position);
        Instantiate(deathExplosion, gameObject.transform.position,
        Quaternion.AngleAxis(-90, Vector3.right));
        GameObject obj = GameObject.Find("GameManager");
        GameManager g = obj.GetComponent<GameManager>();
        g.score += pointValue;
        isAlive = false;
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        pointValue = 10;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
