using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    public bool Bitted;

    void Start()
    {
        Bitted = false;
    }
    
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bitted = true;
    }
}
