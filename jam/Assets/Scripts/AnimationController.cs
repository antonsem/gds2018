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
    private int hang = Animator.StringToHash("Hang");
    private int explode = Animator.StringToHash("Explode");
    private int hang_death = Animator.StringToHash("Hang_Death");
    private int electricuted = Animator.StringToHash("Electricuted");
    private int trip = Animator.StringToHash("Trip");
    private int impale = Animator.StringToHash("Impaled");
    private int spider = Animator.StringToHash("Spider");
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

    public void SetExplode()
    {
        anim.SetTrigger(explode);
    }

    public void SetHang_Death()
    {
        anim.SetTrigger(hang_death);
    }

    public void SetElectricuted()
    {
        anim.SetTrigger(electricuted);
    }

    public void SetTrip()
    {
        anim.SetTrigger(trip);
    }

    public void SetImpaled()
    {
        anim.SetTrigger(impale);
    }

    public void SetSpider()
    {
        anim.SetTrigger(spider);
    }

    private void Reset()
    {
        transform.SetComponent(out anim);
    }
}
