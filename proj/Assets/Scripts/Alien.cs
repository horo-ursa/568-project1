using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
    public int pointValue;
    public int damage;
    public float HP;
    public GameObject deathExplosion;
    public AudioClip deathKnell;
    public GameObject projectile;
    public float bulletSpeed = -8.0f;
    public bool isAlive  = true;
    public Material dieMat;

    private GameObject drop1;
    private GameObject drop2;
    private GameObject drop3;

    private GameObject drop;

    public AudioClip shootAudio;

    public void shoot()
    {
        AudioSource.PlayClipAtPoint(shootAudio, gameObject.transform.position);
        var shootPos = transform.position;
        var proj = Instantiate(projectile, shootPos, Quaternion.identity);
        proj.GetComponent<Projectile>().damage = damage;
        proj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, bulletSpeed);
    }

    public void takeDamage(float d)
    {
        HP -= d;
        AudioSource.PlayClipAtPoint(deathKnell, gameObject.transform.position);
        if (GameManager.instance.HP < 100)
        {
            GameManager.instance.HP = GameManager.instance.LifeSteal + GameManager.instance.HP > 100? 
                100 : GameManager.instance.LifeSteal + GameManager.instance.HP;
        }
        if (HP <= 0)
        {
            Die();
        }
    }

    private void DropItem()
    {
        float rand = Random.Range(0.0f, 100.0f);
        if(rand < 70.0f)
        {
            drop = drop1;
        }else if(rand < 90.0f)
        {
            drop = drop2;
        }
        else
        {
            drop = drop3;
        }
        var dropped = Instantiate(drop, gameObject.transform.position, Quaternion.Euler(90,0,0)) as GameObject;
        dropped.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        float random = Random.Range(0.0f, 1.0f);
        float velocity = random > 0.5f ? 100 : -100;
        dropped.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, velocity));
    }
    public void Die()
    {
        
        Instantiate(deathExplosion, gameObject.transform.position,
        Quaternion.AngleAxis(-90, Vector3.right));
        GameObject obj = GameObject.Find("GameManager");
        GameManager g = obj.GetComponent<GameManager>();
        g.score += pointValue;
        isAlive = false;
        gameObject.GetComponent<MeshRenderer>().material = dieMat;
        var rigidbody = gameObject.GetComponent<Rigidbody>();
        rigidbody.useGravity = true;
        rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rigidbody.constraints |= RigidbodyConstraints.FreezePositionY;
        rigidbody.mass = 0.3f;
        //gameObject.GetComponent<BoxCollider>().isTrigger = true;
        isAlive = false;
        GameObject.Find("EnemyManager").GetComponent<EnemyManager>().removeAlien(this.gameObject);
        this.gameObject.transform.SetParent(null);
        this.gameObject.tag = "DiedAlien";
        GetComponent<Rigidbody>().mass = 0.3f;

        float rand = Random.Range(0.0f, 10.0f) / 10.0f;
        if(rand <= GameManager.instance.DropPossibility)
        {
            DropItem();
        }
        
    }

    public void explode()
    {
        GameManager.instance.gold += 5;
    }

    // Start is called before the first frame update
    void Start()
    {
        pointValue = 10;
        isAlive = true;
        drop1 = GameManager.instance.dropItem1;
        drop2 = GameManager.instance.dropItem2;
        drop3 = GameManager.instance.dropItem3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
