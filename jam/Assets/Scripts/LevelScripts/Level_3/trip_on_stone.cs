using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trip_on_stone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (var contact in collision.contacts)
        {
            if (contact.collider.GetComponentInParent<PlayerInput>() != null)
            {
                Events.Instance.playerDied.Invoke(DeathType.Explode);
                StartCoroutine(CompleteLevel());
            }
        }
    }

    IEnumerator CompleteLevel()
    {
        yield return new WaitForSeconds(4.75f);
        Events.Instance.levelCompleted.Invoke();
    }
}
