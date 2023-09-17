using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public AttackType type;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();
            if(type == AttackType.DOUBLE)
            {
                GameManager.instance.hasDouble = true;
            }else if(type == AttackType.EXPLOSION)
            {
                GameManager.instance.hasExplosion = true;
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Floor"))
        {
            Destroy(gameObject);
        }
    }
}
