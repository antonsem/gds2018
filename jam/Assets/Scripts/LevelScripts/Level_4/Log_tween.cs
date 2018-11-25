using DigitalRuby.Tween;
using UnityEngine;

public class Log_tween : MonoBehaviour
{
    public float maxScale = 2f;
    public float minScale = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        TweenScaleUp();
    }

    private void TweenScaleUp()
    {
        Vector3 currentScale = transform.localScale;
        Vector3 startScale = new Vector3(minScale, minScale, minScale);
        Vector3 endScale = new Vector3(maxScale, maxScale, maxScale);
        currentScale.z = startScale.z = endScale.z = 0.0f;
        gameObject.Tween("ScaleLogs", currentScale, startScale, 1.25f, TweenScaleFunctions.CubicEaseIn, (t) =>
        {
            // progress
            if (t != null && transform != null)
                transform.localScale = t.CurrentValue;
        }, (t) =>
        {
            // completion
            gameObject.Tween("ScaleLogs", startScale, endScale, 1.25f, TweenScaleFunctions.Linear, (t2) =>
            {
                // progress
                if (t2 != null)
                    transform.localScale = t2.CurrentValue;
            }, (t2) =>
                {
                    // completion
                    TweenScaleUp();
                });
        });
    }
}
