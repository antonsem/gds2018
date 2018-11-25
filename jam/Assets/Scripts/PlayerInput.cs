using ExtraTools;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PhysicalObject), typeof(AnimationController))]
public class PlayerInput : MonoBehaviour, IUpdate
{
    public float maxSpeed = 10;
    public float runCoefficient = 1.5f;
    public float dashDistance = 3;
    public float jumpSpeed = 100;
    public float wallJumpHorizontalCoefficient = 2;
    public float wallJumpVerticalCoefficient = 0.5f;
    public int jumpCount = 1;
    [HideInInspector]
    public int defaultJumpCount = 1;//exposed just for the stats editing
    public float teleporterHorizontalThrow = 0.5f;
    public float teleporterVerticalThrow = 0.5f;
    public bool edgeGrab = true;
    public bool wallSlide = true;

    public bool Dashable = false;

    [SerializeField]
    private PhysicalObject physObj;
    [SerializeField]
    private Gravity gravity;
    [SerializeField]
    private AnimationController anim;
    [SerializeField]
    private PlayerAudioController audio;
    [SerializeField]
    private GameObject hook;
    [SerializeField]
    private Hand upperHand;
    [SerializeField]
    private Hand lowerHand;
    [SerializeField]
    private ContactFilter2D handFilter;
    private RaycastHit2D[] hits = new RaycastHit2D[2];
    private bool isHooked = false;
    private bool sliding = false;

    private float inputLock = 0;
    private float horizontalInput = 0;
    private float verticalInput = 0;
    private float stickTime = 0;
    private float speed = 0;

    private bool wallJump = false;
    private bool quitting = false;

    private void GetInput()
    {
        if (physObj.velocity.x == 0)
            speed = 0;

        speed += (maxSpeed - speed) / maxSpeed / 2;
        speed = Mathf.Min(maxSpeed, speed);
        horizontalInput = Input.GetAxisRaw("Horizontal") * Mathf.Min(maxSpeed, speed) * Time.deltaTime;
        verticalInput = Input.GetAxisRaw("Vertical");
        if (inputLock > 0) return;

        if (Dashable && Input.GetKeyDown(KeyCode.LeftShift) && horizontalInput != 0)
        {
            Dash(Mathf.Sign(horizontalInput));
            return;
        }

        if (verticalInput < 0 && hook.activeSelf)
        {
            hook.SetActive(false);
            isHooked = false;
        }

        if (!hook.activeSelf || physObj.isGrounded)
            physObj.velocity.x = horizontalInput * (Input.GetKey(KeyCode.LeftControl) ? runCoefficient : 1);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            if (--jumpCount >= 0 || physObj.isGrounded || isHooked)
            {
                physObj.velocity.y = jumpSpeed;
                audio.PlayJump();
            }
            if (sliding && physObj.velocity.y < 0 && wallJump)
            {
                inputLock = 0.5f;
                physObj.velocity = new Vector2(-horizontalInput * wallJumpHorizontalCoefficient, jumpSpeed * wallJumpVerticalCoefficient);
            }
        }
    }

    private void CheckHands()
    {
        gravity.ResetTerminalVelocity();
        wallJump = false;
        sliding = upperHand.isHolding && lowerHand.isHolding && (Input.GetAxisRaw("Horizontal") != 0 || stickTime > 0) && wallSlide && !physObj.isGrounded;
        if (sliding)
        {
            wallJump = true;
            gravity.terminalVelocity = -0.01f;
            stickTime -= Time.deltaTime;
        }
        else if (!upperHand.isHolding && lowerHand.isHolding && !hook.activeSelf && physObj.velocity.y <= 0)
            hook.SetActive(true);
        else if (physObj.velocity.y > 0 && hook.activeSelf)
        {
            hook.SetActive(false);
            isHooked = false;
        }

        if (!upperHand.isHolding && !lowerHand.isHolding)
            stickTime = 0.5f;
    }

    private void Dash(float dir)
    {
        physObj.velocity.x = dashDistance * dir;
    }

    private bool Hooked(float dist)
    {
        if (physObj.isGrounded || dist > 0) return false;
        float dir = Mathf.Sign(dist);
        float abs = Mathf.Abs(dist - 0.05f);
        int count = Physics2D.Raycast(hook.transform.position - Vector3.up * 0.05f, Vector2.down, handFilter, hits, abs);
        for (int i = 0; i < count; i++)
        {
            if (abs > hits[i].distance)
                abs = hits[i].distance;
        }

        if (Mathf.Approximately(abs, 0.05f))
            return true;
        if (abs < 0.05f)
        {
            transform.Translate(0, 0.05f - abs, 0);
            return true;
        }

        return false;
    }

    #region Mono

    private void Start()
    {
        defaultJumpCount = jumpCount;
    }

    private void Reset()
    {
        transform.SetComponent(out physObj);
        transform.SetComponent(out anim);
    }

    private void Update()
    {
        if (physObj.isGrounded)
            jumpCount = defaultJumpCount;

        CheckHands();
        GetInput();

        isHooked = edgeGrab && hook.activeSelf && Hooked(physObj.velocity.y);

        if (isHooked) physObj.velocity.y = 0;

        gravity.useGravity = !physObj.isGrounded && !isHooked;

        if (inputLock > 0)
        {
            if (wallJump && physObj.isGrounded)
                inputLock = 0;
            else
                inputLock -= Time.deltaTime;
        }

        SetAnimations();
    }

    private void SetAnimations()
    {
        if (physObj.isGrounded)
        {
            anim.SetHang(false);
            anim.SetFall(0);
            anim.SetWalk(horizontalInput);
        }
        else
        {

            if (physObj.velocity.y > 0)
            {
                anim.SetFall(physObj.velocity.y / jumpSpeed);
            }
            else
            {
                if (isHooked || sliding)
                {
                    anim.SetWalk(horizontalInput);
                    anim.SetHang(true);
                    anim.SetFall(0);
                }
                else
                {
                    anim.SetHang(false);
                    anim.SetFall(-physObj.velocity.y / gravity.terminalVelocity);
                }
            }
        }
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

    #endregion

    #region IUpdate
    public void FistUpdate()
    {
        if (inputLock > 0)
            inputLock -= Time.deltaTime;
    }

    public void SecondUpdate()
    {
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
