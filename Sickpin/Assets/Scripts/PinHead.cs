using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinHead : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnTriggerEnter2D(Collider2D collsion)
    {
        if (collsion.tag == "PinHead")
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
        }
    }
}
