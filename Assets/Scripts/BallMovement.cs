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
      
    }

  

    void LunchBall()
    {
        int randomPos = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.linearVelocity = new Vector2(randomPos, randomPos) * speed;

       
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
