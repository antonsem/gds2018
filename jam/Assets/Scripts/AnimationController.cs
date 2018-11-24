using ExtraTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private int fallSpeed = Animator.StringToHash("Fall");
    private int walkSpeed = Animator.StringToHash("Walk");
    private int jump = Animator.StringToHash("Jump");
    private int land = Animator.StringToHash("Landed");
    private int hang = Animator.StringToHash("Hang");
    [SerializeField]
    private Animator anim;

    private bool hanging = false;
    private bool jumping = false;
    private float walking = 0;
    private float falling = 0;

    public void SetFall(float val)
    {
        if (val != falling)
        {
            falling = val;
            SetJump(val != 0);
            anim.SetFloat(fallSpeed, val);
        }

    }

    public void SetJump(bool val)
    {
        if (val != jumping)
        {
            jumping = val;
            anim.SetBool(jump, val);
        }
    }

    public void SetWalk(float val)
    {
        if (val != walking)
        {
            walking = val;
            anim.SetFloat(walkSpeed, Mathf.Abs(val));
        }
    }

    public void SetHang(bool val)
    {
        if (val != hanging)
        {
            hanging = val;
            anim.SetBool(hang, val);
        }
    }

    private void Reset()
    {
        transform.SetComponent(out anim);
    }
}
