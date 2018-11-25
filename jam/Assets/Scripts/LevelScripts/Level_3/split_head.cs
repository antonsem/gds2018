using System.Collections;
using UnityEngine;

public class split_head : MonoBehaviour
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
        Events.Instance.playerDied.Invoke(DeathType.Explode);
        StartCoroutine(CompleteLevel());
    }

    IEnumerator CompleteLevel()
    {
        yield return new WaitForSeconds(1.75f);
        Events.Instance.levelCompleted.Invoke();
    }
}
