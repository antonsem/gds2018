using DigitalRuby.Tween;
using UnityEngine;

public class Log_pickup : MonoBehaviour
{
    public Transform plus_wood_prefab;
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
        Instantiate(plus_wood_prefab, collision.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
