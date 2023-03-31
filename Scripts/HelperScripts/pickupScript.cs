using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupScript : MonoBehaviour
{
    private void Start()
    {
        Invoke("deactivate", Random.Range(3f, 6f));
    }

    private void deactivate()
    {
        Destroy(gameObject);
    }
}
