using ExtraTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trip_on_stone : MonoBehaviour
{
    private bool tripped = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.SetComponent(out PlayerInput player))
        {
            Events.Instance.playerDied.Invoke(DeathType.Tripped);
            StartCoroutine(CompleteLevel());
        }
        //foreach (var contact in collision.contacts)
        //{
        //    if (contact.collider.GetComponentInParent<PlayerInput>() != null)
        //    {
                
        //    }
        //}
    }

    IEnumerator CompleteLevel()
    {
        yield return new WaitForSeconds(4.75f);
        Events.Instance.levelCompleted.Invoke();
    }
}
