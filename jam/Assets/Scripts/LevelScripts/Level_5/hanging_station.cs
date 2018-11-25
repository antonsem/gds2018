using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hanging_station : MonoBehaviour
{
    public GameObject ground;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Destroy(ground);
            AllowPlayerToHang();
            StartCoroutine(StartDying());
            StartCoroutine(CompleteLevel());
        }
    }

    private void AllowPlayerToHang()
    {
        Rigidbody2D rigidbody2D = player.GetComponent<Rigidbody2D>();
        rigidbody2D.freezeRotation = false;
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        rigidbody2D.mass = 20;

        player.GetComponent<Gravity>().useGravity = false;

        GetComponent<HingeJoint2D>().enabled = true;

        GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<PhysicalObject>().enabled = false;

        foreach (GameObject rope_piece in GameObject.FindGameObjectsWithTag("Rope"))
            rope_piece.GetComponent<SpriteRenderer>().enabled = true;
    }

    IEnumerator CompleteLevel()
    {
        yield return new WaitForSeconds(8.75f);
        Events.Instance.levelCompleted.Invoke();
    }

    IEnumerator StartDying()
    {
        yield return new WaitForSeconds(0.95f);
        Events.Instance.playerDied.Invoke(DeathType.Hang);
    }
}
