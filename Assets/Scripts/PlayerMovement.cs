using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 24f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    public AudioSource audioSource;
    public AudioClip ballHit;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            audioSource.PlayOneShot(ballHit);
        }
    }
    // Update is called once per frame
    void Update()
    {
        

    }
    void FixedUpdate()
    {
        Vector2 move = new Vector2(moveInput.x, moveInput.y);
        rb.linearVelocityY = move.y * speed ;
    }
}
