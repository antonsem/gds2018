using UnityEngine;

public class Hand : MonoBehaviour
{
    public bool isHolding = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isHolding = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isHolding = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isHolding = false;
    }

    private void OnDisable()
    {
        isHolding = false;
    }

    private void OnDestroy()
    {
        isHolding = false;
    }
}
