using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 10f;
    public float minBoundary = -2.0f;
    public float maxBoundary = 18.0f;
    public float shootingCooldown = 1.0f;
    private float lastShootingTime = -1.0f;
    // Use this for initialization
    void Start()
    {
        
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
            Vector3 spawnPos = gameObject.transform.position;
            // instantiate the Bullet
            GameObject obj = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;
            // get the Bullet Script Component of the new Bullet instance
            BulletScript b = obj.GetComponent<BulletScript>();
            b.initPosition = spawnPos;
            obj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, b.bulletSpeed);
        }
    }
}