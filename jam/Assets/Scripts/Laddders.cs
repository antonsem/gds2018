using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laddders : MonoBehaviour
{
    private bool Inledder;

    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (Inledder)
        {
            if (Input.GetKey(KeyCode.W))
            {
                player.GetComponent<PhysicalObject>().velocity.y = 0.1f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                player.GetComponent<PhysicalObject>().velocity.y = -0.1f;
            }
            else
            {
                player.GetComponent<PhysicalObject>().velocity.y = 0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Inledder = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Inledder = false;
    }
}
