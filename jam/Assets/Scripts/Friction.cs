using ExtraTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicalObject))]
public class Friction : MonoBehaviour
{
    [SerializeField]
    private PhysicalObject obj;

    [Range(0, 1)]
    public float groundFriction = 0;
    [Range(0, 1)]
    public float airFriction = 0;

    private void Update()
    {
        if (!Mathf.Approximately(obj.velocity.x, 0))
            obj.velocity.x *= (1 - (obj.isGrounded ? groundFriction : airFriction));
    }

    private void Reset()
    {
        transform.SetComponent(out obj);
    }
}
