using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierMatrix : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject piece;

    public float horizontalSpacing;
    public float verticalSpacing;
    void Start()
    {
        Vector3 initialPosition = transform.position;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                Vector3 position = new Vector3(
                    initialPosition.x + (j * horizontalSpacing),
                    0,
                    initialPosition.z - (i * verticalSpacing)
                );

                // Get the appropriate alien ship type based on the current row
                var alien = Instantiate(piece, position, Quaternion.Euler(0, 0, 0), transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
