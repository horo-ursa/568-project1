using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Vector3 initPosition;
    public float bulletSpeed = 10.0f;

    public bool isExplosion = false;
    public float explosionRadius = 1.0f;
    public float damage;
    // Use this for initialization
    void Start()
    {
        explosionRadius = 2.0f;
    }

/*    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Alien"))
        {
            Alien roid = collider.gameObject.GetComponent<Alien>();
            // let the other object handle its own death throes
            Destroy(gameObject);
            roid.Die();
            // Destroy the Bullet which collided with the Asteroid
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
        else if (collider.CompareTag("Floor"))
        {
            gameObject.GetComponent<SphereCollider>().isTrigger = false;
        }
        else
        {
            // if we collided with something else, print to console
            // what the other thing was
            Debug.Log("Collided with " + collider.tag);
        }
    }*/

    void OnCollisionEnter(Collision collision)
    {
        Collider collider = collision.collider;
        if (collider.CompareTag("Alien"))
        {
            if (isExplosion)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
                foreach (Collider c in colliders)
                {
                    if (c.CompareTag("Alien"))
                    {
                        // If it's an alien, kill it
                        Alien alienScript = c.GetComponent<Alien>();
                        if (alienScript != null)
                        {
                            alienScript.takeDamage(damage);
                        }
                    }
                }
            }
            else
            {
                Alien roid = collider.gameObject.GetComponent<Alien>();
                // let the other object handle its own death throes
                Destroy(gameObject);
                roid.takeDamage(damage);
                
            }
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
        else if (collider.CompareTag("Floor"))
        {
            gameObject.GetComponent<Rigidbody>().mass = 0.2f ;
        }
        else if (collider.CompareTag("DiedAlien"))
        {
            
            Alien roid = collider.gameObject.GetComponent<Alien>();
            roid.explode();
            Destroy(gameObject);
            Destroy(collision.gameObject);
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
        /*Vector3 currPos = transform.position;
        float distance = Vector3.Distance(initPosition, currPos);
        if(distance > 50)
        {
            Destroy(gameObject);
        }*/
    }
}
