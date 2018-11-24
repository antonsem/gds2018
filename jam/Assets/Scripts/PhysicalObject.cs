using ExtraTools;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicalObject : MonoBehaviour, IUpdate
{
    public Vector2 velocity = Vector2.zero;

    [SerializeField]
    private Transform rotateObject;
    [SerializeField]
    private ContactFilter2D horizontalMask;
    [SerializeField]
    private Transform forwardReference;
    [SerializeField]
    private Transform[] forwardRays;
    private float minHorizontalDist = 0;
    [SerializeField]
    private ContactFilter2D verticalMask;
    [SerializeField]
    private Transform bottomReference;
    [SerializeField]
    private Transform[] bottomRays;
    private float minBottomDist = 0;
    [SerializeField]
    private Transform topReference;
    [SerializeField]
    private Transform[] topRays;
    private float minTopDist = 0;
    private readonly RaycastHit2D[] hits = new RaycastHit2D[4];
    private Vector2 castDir = Vector2.zero;

    [Space(10), Header("Assignables"), SerializeField]
    private Rigidbody2D rigid;
    public Rigidbody2D Rigid
    {
        get => rigid;
        private set => rigid = value;
    }
    public bool isGrounded = false;

    private void MoveHorizontal(float dist)
    {
        transform.Translate(dist, 0, 0);
    }

    public float CheckHorizontal(float dist)
    {
        if (dist == 0) return 0;
        float dir = Mathf.Sign(dist);
        float abs = Mathf.Abs(dist);

        castDir.y = 0;
        castDir.x = dir;

        for (int i = 0; i < forwardRays.Length; i++)
        {
            for (int j = 0; j < Physics2D.Raycast(forwardRays[i].position, castDir, horizontalMask, hits, abs + minHorizontalDist); j++)
            {
                if (hits[j].distance - minHorizontalDist < abs && hits[j].normal.x * dir < 0)
                {
                    abs = hits[j].distance - minHorizontalDist;
                    if (abs < 0)
                    {
                        transform.Translate(new Vector3(abs * dir, 0, 0));
                        abs = 0;
                    }
                }
            }
        }

        return abs * dir;
    }

    private void MoveVertical(float dist)
    {
        transform.Translate(0, dist, 0);
    }

    private float CheckGroundDistance(float dist)
    {
        if (dist == 0) return 0;
        float dir = Mathf.Sign(dist);
        float abs = Mathf.Abs(dist);

        for (int i = 0; i < bottomRays.Length; i++)
        {
            if (!bottomRays[i].gameObject.activeInHierarchy) continue;
            for (int j = 0; j < Physics2D.Raycast(bottomRays[i].position, castDir, verticalMask, hits, abs); j++)
            {
                if (abs > hits[j].distance - minBottomDist && Mathf.Abs(hits[j].normal.y) > 0.8f
                && (dir * hits[j].normal.y) < 0)
                {
                    abs = hits[j].distance - minBottomDist;
                    if ((abs > -0.1f && abs < 0.1f) || hits[j].point.y > bottomReference.position.y - abs)
                    {
                        transform.Translate(new Vector3(0, hits[j].point.y - bottomReference.position.y, 0));
                        abs = 0;
                        break;
                    }
                }
            }
        }

        return abs * dir * (abs < 0 ? 0 : 1);
    }

    private bool CheckGround(float dist)
    {
        if (dist > 0) return true;
        float groundPos = bottomReference.position.y + Mathf.Min(dist, -0.05f);
        bool hitTheGround = false;
        for (int i = 0; i < bottomRays.Length; i++)
        {
            if (!bottomRays[i].gameObject.activeInHierarchy) continue;
            for (int j = 0; j < Physics2D.Raycast(bottomRays[i].position, Vector2.down, verticalMask, hits, minBottomDist - Mathf.Min(dist, -0.05f)); j++)
            {
                if (hits[j].point.y > groundPos)
                {
                    groundPos = hits[j].point.y;
                    hitTheGround = true;
                }
            }
        }

        if (hitTheGround)
        {
            transform.Translate(new Vector3(0, groundPos - bottomReference.position.y, 0));
            return true;
        }
        else
            return false;

    }

    private bool CheckTopDistance(float dist)
    {
        if (dist <= 0) return false;
        float minHeight = topReference.position.y + dist;
        bool hitTop = false;
        for (int i = 0; i < topRays.Length; i++)
        {
            for (int j = 0; j < Physics2D.Raycast(topRays[i].position, Vector2.up, verticalMask, hits, minTopDist + 0.05f); j++)
            {
                if (minHeight > hits[j].point.y)
                {
                    minHeight = hits[j].point.y;
                    hitTop = true;
                }
            }
        }

        if (hitTop)
        {
            transform.Translate(0, minHeight - topReference.position.y, 0);
            return true;
        }

        return false;
    }

    private void Rotate(float dir)
    {
        if (dir > 0)
            rotateObject.rotation = Quaternion.Euler(0, 0, 0);
        else if (dir < 0)
            rotateObject.rotation = Quaternion.Euler(0, 180, 0);
    }

    #region Mono
    private bool quitting = false;

    private void Start()
    {
        if (forwardReference && forwardRays != null && forwardRays.Length > 0)
            minHorizontalDist = Mathf.Abs(forwardReference.position.x - forwardRays[0].position.x);
        if (bottomReference && bottomRays != null && bottomRays.Length > 0)
            minBottomDist = Mathf.Abs(bottomReference.position.y - bottomRays[0].position.y);
        if (topReference && topRays != null && topRays.Length > 0)
            minTopDist = Mathf.Abs(topReference.position.y - topRays[0].position.y);
    }

    private void OnEnable()
    {
        StartCoroutine(EnableCoroutine());
    }

    private IEnumerator EnableCoroutine()
    {
        yield return null;
        Register();
    }

    private void OnDisable()
    {
        if (!quitting)
            Unregister();
    }

    private void OnApplicationQuit()
    {
        quitting = true;
    }

    private void Reset()
    {
        if (!rigid)
            transform.SetComponent(out rigid);
    }
    #endregion

    #region IUpdate
    public void FistUpdate()
    {
        isGrounded = velocity.y <= 0 && CheckGround(velocity.y);
        if (isGrounded || CheckTopDistance(velocity.y)) velocity.y = 0;
        if (!isGrounded && velocity.y < 0)
            velocity.y = CheckGroundDistance(velocity.y);
        MoveVertical(velocity.y);
    }

    public void SecondUpdate()
    {
        Rotate(velocity.x);
        velocity.x = CheckHorizontal(velocity.x);
        MoveHorizontal(velocity.x);
    }

    public void ThirdUpdate()
    {
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void Register()
    {
        Events.Instance.registerUpdate.Invoke(this);
    }

    public void Unregister()
    {
        Events.Instance.unregisterUpdate.Invoke(this);
    }
    #endregion
}
