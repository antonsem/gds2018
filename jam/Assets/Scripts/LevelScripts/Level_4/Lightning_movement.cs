using DigitalRuby.Tween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning_movement : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        endPos = startPos + new Vector3(0, 0.6f, 0);
        startPos.z = endPos.z = 0.0f;
        TweenFloatUpDown();
        GetComponent<SpriteRenderer>().color = Color.blue;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void TweenFloatUpDown()
    {
        gameObject.Tween("MoveUp", startPos, endPos, 1.85f, TweenScaleFunctions.QuadraticEaseInOut, (t) =>
        {
            // progress
            gameObject.gameObject.transform.position = t.CurrentValue;
        }, (t) =>
        {
            // completion
            gameObject.Tween("MoveUp", endPos, startPos, 1.15f, TweenScaleFunctions.QuadraticEaseInOut, (t2) =>
            {
                // progress
                gameObject.gameObject.transform.position = t2.CurrentValue;
            }, (t3) =>
            {
                // completion
                TweenFloatUpDown();
            });
        });
    }
}
