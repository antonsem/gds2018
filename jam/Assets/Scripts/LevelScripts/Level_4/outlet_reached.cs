using System.Collections;
using UnityEngine;

public class outlet_reached : MonoBehaviour
{
    public GameObject player;
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
        Events.Instance.playerDied.Invoke(DeathType.Electricuted);
        StartCoroutine(CompleteLevel());
    }

    IEnumerator CompleteLevel()
    {
        yield return new WaitForSeconds(1.75f);
        Events.Instance.levelCompleted.Invoke();
    }
}
