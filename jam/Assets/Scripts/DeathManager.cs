using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeathType
{
    Explode,
    Impale
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
    }
#endif

    private void Death(DeathType type)
    {
        switch (type)
        {
            case (DeathType.Explode):
                blood.transform.SetParent(null);
                blood.SetActive(true);
                gameObject.SetActive(false);
                Debug.Log("Exploded!");
                break;
            case (DeathType.Impale):
                phys.enabled = false;
                gravity.enabled = false;
                input.enabled = false;
                Debug.Log("Impaled");
                break;
            default:
                Debug.Log("Exploded!");
                break;
        }
        Debug.Log(string.Format("Player died because of {0}", type.ToString()));
    }
}
