using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Building : MonoBehaviour
{
    bool Dragging = false;

    public Texture2D DraggableObjectCursorIcon;

    public Texture2D DefaultCursorIcon;

    public int speed = 100;

    public float DistanceFromPlayer = 10f;

    private PhysicalObject rigidbody2DComponent;
    private Gravity gravityComponent;
    private GameObject Player;

    void Start()
    {
        rigidbody2DComponent = gameObject.GetComponent<PhysicalObject>();
        gravityComponent = gameObject.GetComponent<Gravity>();
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        if (!IsCloseToPlayer())
        {
            EndBuilding();
        }

        if (Dragging)
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;

            rigidbody2DComponent.velocity = -(transform.position - mousePosition);
            Debug.Log(-(transform.position - mousePosition));
        }
    }

    private void OnMouseEnter()
    {
        Cursor.SetCursor(DraggableObjectCursorIcon, Vector2.zero, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(DefaultCursorIcon, Vector2.zero, CursorMode.Auto);
    }

    void OnMouseDown()
    {
        StartBuilding();
    }

    void OnMouseUp()
    {
        EndBuilding();
    }

    private void StartBuilding()
    {
        Dragging = true;
        var pos = this.gameObject.transform.position;
        gravityComponent.enabled = false;
    }

    private void EndBuilding()
    {
        Dragging = false;
        var pos = this.gameObject.transform.position;
        gravityComponent.enabled = true;

        LimitVelocity();
    }

    private void LimitVelocity()
    {
        const float limitVelocity = 0.2f;
        var magnitude = rigidbody2DComponent.velocity.magnitude;
        if (magnitude > limitVelocity)
        {
            var vec = rigidbody2DComponent.velocity.normalized;
            vec.Scale(new Vector2(limitVelocity/4, limitVelocity));
            rigidbody2DComponent.velocity = vec;
        }
    }

    private bool IsCloseToPlayer()
    {
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        var playerPosition = Player.transform.position;
        playerPosition.z = 0;

        var delta = (mousePosition - playerPosition);

        return delta.magnitude < DistanceFromPlayer;
    }

}
