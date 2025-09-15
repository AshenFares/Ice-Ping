using UnityEngine;

public class BallMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float magnusCoefficient = 2f; // Adjust this for more/less curve
    private SpriteRenderer m_spriteRenderer;
    public AudioSource audioSource;
    public AudioClip ballHit;
    public AudioClip pointScored;
    private bool magnusEffect = false;

    public bool gameOn = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("LunchBall", 4);
    }

    void FixedUpdate()
    {
        if (magnusEffect == true)
        {
            MagnusEff();
        }
        
    }

    void MagnusEff()
    {
        // Only apply Magnus effect if the ball is moving
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            // Get spin from angular velocity
            float spin = rb.angularVelocity*2f;

            // Get perpendicular direction to velocity
            Vector2 perpendicular = new Vector2(-rb.linearVelocity.y, rb.linearVelocity.x).normalized;

            // Apply Magnus force
            rb.AddForce(perpendicular * spin * magnusCoefficient * Time.fixedDeltaTime, ForceMode2D.Force);
        }
    }

    void LunchBall()
    {
        int randomPos = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.linearVelocity = new Vector2(randomPos, randomPos) * speed;

        // Give the ball some initial spin
        rb.angularVelocity = Random.Range(-200f, 200f);
    }

    public void ResetBall()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = Vector2.zero;

        Invoke("LunchBall", 2f);
        audioSource.PlayOneShot(pointScored);
    }

    public void StopBall()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = Vector2.zero;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            audioSource.PlayOneShot(ballHit);
        }
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Activate Magnus effect after paddle hit
            magnusEffect = true;
            audioSource.PlayOneShot(ballHit);
        }
    }
}
