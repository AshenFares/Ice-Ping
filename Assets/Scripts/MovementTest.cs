using UnityEngine;

public class MovementTest : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float magnusCoefficient = 10f; // Curve strength
    private SpriteRenderer m_spriteRenderer;
    public AudioSource audioSource;
    public AudioClip ballHit;
    public AudioClip pointScored;

    public bool gameOn = true;
    private bool magnusActive = false; // only true after paddle hit

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        Invoke("LunchBall", 4);
    }

    void FixedUpdate()
    {
        if (magnusActive)
        {
            MagnusEff();
        }
    }

    void MagnusEff()
    {
        if (rb.linearVelocity.magnitude > 0.1f)
        {
            float spin = rb.angularVelocity;
            Vector2 perpendicular = new Vector2(-rb.linearVelocity.y, rb.linearVelocity.x).normalized;

            rb.AddForce(perpendicular * spin * magnusCoefficient * Time.fixedDeltaTime, ForceMode2D.Force);
        }
    }

    void LunchBall()
    {
        int randomPos = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.linearVelocity = new Vector2(randomPos, randomPos) * speed;

        rb.angularVelocity = 0f; // no spin at start
        magnusActive = false;    // Magnus inactive until paddle hit
    }

    public void ResetBall()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = Vector2.zero;

        magnusActive = false;

        Invoke("LunchBall", 2f);
        audioSource.PlayOneShot(pointScored);
    }

    public void StopBall()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.position = Vector2.zero;
        magnusActive = false;
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
            magnusActive = true;

            // Example: give spin based on paddle movement direction
            Rigidbody2D paddleRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (paddleRb != null)
            {
                float paddleVelocityY = paddleRb.linearVelocity.y;
                rb.angularVelocity = paddleVelocityY * 40f; // tweak multiplier
            }
            else
            {
                // If paddle has no Rigidbody2D, just give some default spin
                rb.angularVelocity = Random.Range(-150f, 150f);
            }

            audioSource.PlayOneShot(ballHit);
        }
    }
}
