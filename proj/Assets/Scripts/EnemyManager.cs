using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject alienShip1;
    public GameObject projectile;
    public GameObject UFO;
    public GameManager gm;
    public int rows = 5;
    public int cols = 11;
    public float horizontalSpacing = 1.5f;
    public float verticalSpacing = 1.5f;
    public float horizontalSpeed = 1.0f;
    public float verticalSpeed = 0.5f;
    public float boundary = 2.0f;
    public float bulletSpeed = -8.0f;

    private Vector3 initialPosition;
    private bool movingRight = true;
    private List<GameObject> mAliens = new List<GameObject>();

    public float shootingCooldown = 3.0f;
    private float lastShootingTime = 2.0f;

    public float UFOCooldown = 8.0f;
    private float UFOLastTime = 1.0f;

    private Vector3 initPos;

    private void Awake()
    {
        initialPosition = transform.position;
        GenerateEnemyGrid();
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        initPos = gameObject.transform.position;
        /*initialPosition = transform.position;
        GenerateEnemyGrid();*/
    }

    void GenerateEnemyGrid()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Vector3 position = new Vector3(
                    initialPosition.x + (j * horizontalSpacing),
                    initialPosition.y,
                    initialPosition.z - (i * verticalSpacing)
                );

                var alien = Instantiate(alienShip1, position, Quaternion.identity, transform);

                mAliens.Add(alien);
            }
        }
    }

    private bool killPlayer()
    {

        for (int i = 0; i < mAliens.Count; ++i)
        {
            if (mAliens[i] != null)
            {
                if (mAliens[i].transform.position.z <= 1) return true;
            }
        }
        return false;
    }

    private void shootProjectile()
    {
        bool isValidate = false;
        while (!isValidate)
        {
            int rand = Random.Range(0, mAliens.Count);
            if (mAliens[rand] != null)
            {
                isValidate = true;
                var shootPos = mAliens[rand].transform.position;
                var proj = Instantiate(projectile, shootPos, Quaternion.identity);
                proj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, bulletSpeed);
            }
        }
    }

    private bool allKilled()
    {
        bool allDestroyed = true;
        for(int i = 0; i < mAliens.Count; ++i)
        {
            if (mAliens[i] != null)
            {
                allDestroyed = false;
            }
        }
        return allDestroyed;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            transform.position += new Vector3(horizontalSpeed * Time.deltaTime, 0, 0);
            if (transform.position.x >= boundary)
            {
                Debug.Log("out of boundary");
                movingRight = false;
                transform.position += new Vector3(0,0 , -verticalSpeed);
            }
        }
        else
        {
            transform.position -= new Vector3(horizontalSpeed * Time.deltaTime, 0, 0);
            if (transform.position.x <= -boundary + 5)
            {
                movingRight = true;
                transform.position += new Vector3(0, 0, -verticalSpeed);
            }
        }
        if (allKilled())
        {
            gm.WinGame();
        }
        if (killPlayer())
        {
            gm.EndGame();
        }
        //generate projectiles
        if(Time.time - lastShootingTime >= (shootingCooldown + Random.Range(-1.5f, 1.5f))){
            shootProjectile();
            lastShootingTime = Time.time;
        }
        //generate UFO
        if (Time.time - UFOLastTime >= (UFOCooldown + Random.Range(-3f, 3f)))
        {
            Vector3 offset = new Vector3(0.0f, 0.0f, 1.0f);
            var go = Instantiate(UFO, initPos + offset , Quaternion.identity) as GameObject;
            var ufo = go.GetComponent<UFO>();
            ufo.initPos = go.transform.position;
            ufo.GetComponent<Rigidbody>().velocity = new Vector3(ufo.speed, 0.0f, 0.0f);
            UFOLastTime = Time.time;
        }
    }
}
