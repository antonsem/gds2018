using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedReduction : MonoBehaviour
{
    PlayerInput pi_script;
    // Start is called before the first frame update
    void Start()
    {
        pi_script = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            pi_script.maxSpeed = Mathf.Max(0.00001f, pi_script.maxSpeed - pi_script.maxSpeed / 100);
        }
    }
}
