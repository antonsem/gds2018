using DigitalRuby.Tween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restart_hint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TweenFloat();
        TweenFadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TweenFloat()
    {
        Vector3 currentPos = transform.position;
        Vector3 endPos = currentPos + new Vector3(0, 2.1f, 0);
        currentPos.z = endPos.z = 0.0f;
        gameObject.Tween("PlusLogsFloat", currentPos, endPos, 1.25f, TweenScaleFunctions.CubicEaseOut, (t) =>
        {
            // progress
            transform.position = t.CurrentValue;
        }, (t) =>
        {
            // completion
        });
    }

    private void TweenFadeOut()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Vector4 startColor = renderer.color;
        Vector4 endColor = new Color(1, 1, 1, 0);
        gameObject.Tween("PlusLogsFadeOut", startColor, endColor, 1.5f, TweenScaleFunctions.CubicEaseOut, (t) =>
        {
            // progress
            renderer.color = t.CurrentValue;
        }, (t) =>
        {
            Destroy(gameObject);
            // completion
        });
    }
}
