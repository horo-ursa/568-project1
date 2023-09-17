using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10.0f;
    public float bulletForce = 1500.0f;
    public float minBoundary = -2.0f;
    public float maxBoundary = 18.0f;
    public int damage = 10;

    public float shootingCooldown = 1.0f;
         float lastShootingTime = -1.0f;

    // Use this for initialization
    void Start()
    {
        //speed = 10.0f;
    }
    /* forced changes to rigid body physics parameters should be done through the FixedUpdate() method, not the Update() method
    */
    void FixedUpdate()
    {

    }
    // Update is called once per frame
    public GameObject bullet; // the GameObject to spawn
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float moveAmount = horizontalInput * speed * Time.deltaTime;

        // Get the new position, clamping it to prevent the ship from moving out of bounds
        float newPositionX = Mathf.Clamp(transform.position.x + moveAmount, minBoundary, maxBoundary);

        // Update the position while retaining the current Y and Z values
        transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);
        if (Input.GetButtonUp("Fire1") && Time.time - lastShootingTime >= shootingCooldown)
        {
            Debug.Log("Fire! ");
            lastShootingTime = Time.time;

            bool SpawnPos1 = true;
            bool SpawnPos2 = false;
            if (GameManager.instance.hasDouble)
            {
                SpawnPos1 = true;
                SpawnPos2 = true;
            }

            if (SpawnPos1 && SpawnPos2)
            {
                Vector3 spawnPosR = gameObject.transform.position + new Vector3(0.25f, 0, 1.0f);
                Vector3 spawnPosL = gameObject.transform.position + new Vector3(-0.25f, 0, 1.0f);
                GameObject obj1 = Instantiate(bullet, spawnPosL, Quaternion.identity) as GameObject;
                GameObject obj2 = Instantiate(bullet, spawnPosR, Quaternion.identity) as GameObject;

                BulletScript b1 = obj1.GetComponent<BulletScript>();
                b1.initPosition = spawnPosL;
                b1.damage = damage;
                obj1.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, bulletForce));

                BulletScript b2 = obj1.GetComponent<BulletScript>();
                b2.initPosition = spawnPosR;
                b2.damage = damage;
                obj2.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, bulletForce));

                if (GameManager.instance.hasExplosion)
                {
                    b1.isExplosion = true;
                    b2.isExplosion = true;
                }
            }
            else
            {
                Vector3 spawnPos = gameObject.transform.position + new Vector3(0, 0, 0.5f);
                // instantiate the Bullet
                GameObject obj = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;
                // get the Bullet Script Component of the new Bullet instance
                BulletScript b = obj.GetComponent<BulletScript>();
                b.initPosition = spawnPos;
                b.damage = damage;
                obj.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, bulletForce));

                if (GameManager.instance.hasExplosion)
                {
                    b.isExplosion = true;
                }
            }
        }
    }
}