using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFO : MonoBehaviour
{
    public int pointValue;
    public bool isAlive = true;
    public Vector3 initPos;
    public float speed = 10.0f;
    void Start()
    {
        
    }

    public void Die()
    {
        GameManager g = GameObject.Find("GameManager").GetComponent<GameManager>();
        g.score += pointValue;
        isAlive = false;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(initPos, gameObject.transform.position) > 50.0f)
        {
            Destroy(gameObject);
        }
    }
}
