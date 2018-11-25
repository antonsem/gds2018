using Cinemachine;
using System.Collections;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private bool playerDead = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!playerDead)
        {
            Events.Instance.playerDied.Invoke(DeathType.Impale);
            playerDead = true;
            Level1State.zoomCamera = true;
        }

        if (playerDead)
        {
            StartCoroutine(CompleteLevel());
        }
    }
    IEnumerator CompleteLevel()
    {
        yield return new WaitForSeconds(2.0f);
        Events.Instance.levelCompleted.Invoke();
    }
}
