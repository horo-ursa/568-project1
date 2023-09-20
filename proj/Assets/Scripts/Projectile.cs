using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    public GameManager gm;
    public int damage = 10;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameManager.instance.HP -= damage;
            var player = collider.GetComponent<Player>();
            Instantiate(player.deathExplosion, player.transform);
            Destroy(gameObject);
        }
        else if (collider.CompareTag("Barrier"))
        {
            GameObject obj = collider.gameObject;
            Destroy(obj);
            Destroy(gameObject);
        }
        else
        {
            //Debug.Log("Collided with " + collider.tag);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
