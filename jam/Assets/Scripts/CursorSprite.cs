using UnityEngine;

public class CursorSprite : MonoBehaviour
{
    private GameObject cursor;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        cursor = GameObject.Find("Cursor");
    }

    // Update is called once per frame
    void Update()
    {
        cursor.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursor.transform.position = new Vector3(cursor.transform.position.x, cursor.transform.position.y, -0.1308594f);
    }
}
