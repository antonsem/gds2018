using System.Collections;
using UnityEngine;

public class outlet_reached : MonoBehaviour
{
    public GameObject player;

    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    public bool zoomingIn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("yoom");
        if (zoomingIn)
        {
            if (virtualCamera.m_Lens.FieldOfView > 40)
            {
                virtualCamera.m_Lens.FieldOfView -= 0.1f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Events.Instance.playerDied.Invoke(DeathType.Electricuted);
        zoomingIn = true;
        StartCoroutine(CompleteLevel());
    }

    IEnumerator CompleteLevel()
    {
        yield return new WaitForSeconds(1.75f);
        Events.Instance.levelCompleted.Invoke();
    }
}
