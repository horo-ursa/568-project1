using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector3 initPosition;
    public float bulletSpeed = 10.0f;
    // Use this for initialization
    void Start()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Alien"))
        {
            Alien roid = collider.gameObject.GetComponent<Alien>();
            // let the other object handle its own death throes
            roid.Die();
            // Destroy the Bullet which collided with the Asteroid
            Destroy(gameObject);
        }
        else if (collider.CompareTag("Barrier"))
        {
            GameObject obj = collider.gameObject;
            Destroy(obj);
            Destroy(gameObject);
        }
        else if (collider.CompareTag("UFO"))
        {
            UFO ufo = collider.gameObject.GetComponent<UFO>();
            // let the other object handle its own death throes
            ufo.Die();
            // Destroy the Bullet which collided with the Asteroid
            Destroy(gameObject);
        }
        else
        {
            // if we collided with something else, print to console
            // what the other thing was
            Debug.Log("Collided with " + collider.tag);
        }
    }

    void Update()
    {
        Vector3 currPos = transform.position;
        float distance = Vector3.Distance(initPosition, currPos);
        if(distance > 50)
        {
            Destroy(gameObject);
        }
    }
}
