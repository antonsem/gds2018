using UnityEngine;

public class trampoline_jump : MonoBehaviour
{
    PhysicalObject player;
    public float velocity_increase = 0.36f;

    int jumpCount = 0;
    float jumpCountClearPeriod = 2;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PhysicalObject>();
    }

    // Update is called once per frame
    void Update()
    {
        jumpCountClearPeriod -= Time.deltaTime;
        if (jumpCountClearPeriod <= 0)
        {
            jumpCount = 0;
            jumpCountClearPeriod = 2;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.velocity.y = velocity_increase + velocity_increase * jumpCount / 5;
        ++jumpCount;
        jumpCountClearPeriod = 2;
    }
}