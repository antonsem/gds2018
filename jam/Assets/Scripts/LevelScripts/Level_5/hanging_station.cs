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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        foreach (var contact in collision.contacts)
        {
            if (contact.collider.GetComponentInParent<PlayerInput>() != null)
            {
                Debug.Log(contact.collider.name);
                Destroy(ground);
                //Events.Instance.playerDied.Invoke(DeathType.Hang);
                AllowPlayerToHang();
                //StartCoroutine(CompleteLevel());
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Destroy(ground);
            Events.Instance.playerDied.Invoke(DeathType.Hang);
            AllowPlayerToHang();
            //StartCoroutine(CompleteLevel());
        }
    }

    private void AllowPlayerToHang()
    {
        Rigidbody2D rigidbody2D = player.GetComponent<Rigidbody2D>();
        rigidbody2D.freezeRotation = false;
        rigidbody2D.bodyType = RigidbodyType2D.Dynamic;

        player.GetComponent<Gravity>().useGravity = false;

        GetComponent<HingeJoint2D>().enabled = true;

        GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<PhysicalObject>().enabled = false;
    }

    IEnumerator CompleteLevel()
    {
        yield return new WaitForSeconds(5.75f);
        Events.Instance.levelCompleted.Invoke();
    }
}
