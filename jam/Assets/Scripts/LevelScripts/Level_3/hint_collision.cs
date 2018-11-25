using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hint_collision : MonoBehaviour
{
    public Transform hint_prefab;
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
        StartCoroutine(Destruction(Instantiate(hint_prefab, collision.transform.position, Quaternion.identity)));
    }

    IEnumerator Destruction(Transform gob)
    {
        yield return new WaitForSeconds(2.00f);
        Destroy(gob.gameObject);
    }
}
