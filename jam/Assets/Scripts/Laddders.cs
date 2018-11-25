using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laddders : MonoBehaviour
{
    private bool Inledder;

    private PlayerInput player;

    void Start()
    {
        player = FindObjectOfType<PlayerInput>();
    }

    void Update()
    {
        if (Inledder)
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                player.GetComponent<PhysicalObject>().velocity.y = 0.1f;
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
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
        player.climbLadder = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Inledder = false;
        player.climbLadder = false;
    }
}
