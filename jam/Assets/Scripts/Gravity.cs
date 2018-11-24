using ExtraTools;
using UnityEngine;

[RequireComponent(typeof(PhysicalObject))]
public class Gravity : MonoBehaviour
{
    public float gravity = 1;
    public bool useGravity = true;
    public float terminalVelocity = -0.5f;
    private float defaultTerminalVelocity;

    [SerializeField]
    private PhysicalObject obj;

    public void ResetTerminalVelocity()
    {
        terminalVelocity = defaultTerminalVelocity;
    }

    private void Awake()
    {
        defaultTerminalVelocity = terminalVelocity;
    }

    private void Reset()
    {
        if (!obj)
            transform.SetComponent(out obj, true);
    }

    private void ApplyGravity()
    {
        obj.velocity.y -= gravity * Time.deltaTime;
        if (obj.velocity.y < terminalVelocity)
            obj.velocity.y = terminalVelocity;
    }

    private void Update()
    {
        if (useGravity)
            ApplyGravity();
    }
}
