using ExtraTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeathType
{
    Explode,
    Impale,
    Hang,
    Electricuted
}

public class DeathManager : MonoBehaviour
{
    private bool quitting = false;

    [SerializeField]
    private PhysicalObject phys;
    [SerializeField]
    private Gravity gravity;
    [SerializeField]
    private PlayerInput input;
    [SerializeField]
    private GameObject blood;
    [SerializeField]
    private GameObject head;
    [SerializeField]
    private GameObject audioSource;
    [SerializeField]
    private AnimationController anim;

    private void OnEnable()
    {
        Events.Instance.playerDied.AddListener(Death);
    }

    private void OnDisable()
    {
        if (!quitting)
        {
            Events.Instance.playerDied.RemoveListener(Death);
            phys.enabled = false;
            gravity.enabled = false;
        }
    }

    private void OnApplicationQuit()
    {
        quitting = true;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Events.Instance.playerDied.Invoke(DeathType.Explode);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            Events.Instance.playerDied.Invoke(DeathType.Impale);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            Events.Instance.playerDied.Invoke(DeathType.Hang);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            Events.Instance.playerDied.Invoke(DeathType.Electricuted);
    }
#endif

    private void Death(DeathType type)
    {
        switch (type)
        {
            case (DeathType.Explode):
                anim.SetExplode();
                Debug.Log("Exploded!");
                break;
            case (DeathType.Impale):
                phys.enabled = false;
                gravity.enabled = false;
                input.enabled = false;
                Debug.Log("Impaled");
                break;
            case (DeathType.Hang):
                anim.SetHang_Death();
                DoHang();
                break;
            case (DeathType.Electricuted):
                anim.SetElectricuted();
                break;
            default:
                Debug.Log("Exploded!");
                break;
        }
        Debug.Log(string.Format("Player died because of {0}", type.ToString()));
    }

    public void DoExplode()
    {
        Events.Instance.shakeCamera.Invoke();
        audioSource.transform.SetParent(null);
        blood.transform.SetParent(null);
        blood.SetActive(true);
        gameObject.SetActive(false);
    }

    public void DoHang()
    {
        head.SetActive(true);
        if(head.transform.SetComponent(out Rigidbody2D rigid))
            rigid.angularVelocity = Random.Range(-180, 180);
    }
}
