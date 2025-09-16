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

    [SerializeField] private GameObject player1X;
    [SerializeField] private GameObject player1Y;
    [SerializeField] private GameObject player2X;
    [SerializeField] private GameObject player2Y;
    [SerializeField] private GameObject x1ColliderObject;
    [SerializeField] private GameObject x2ColliderObject;
    [SerializeField] private GameObject y1ColliderObject;
    [SerializeField] private GameObject y2ColliderObject;

    private bool isYActive = false;


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

   public  void SwitchPlayers()
    {
        isYActive = !isYActive;

        // Activate/deactivate collider objects
        x1ColliderObject.SetActive(!isYActive);
        x2ColliderObject.SetActive(!isYActive);
        y1ColliderObject.SetActive(isYActive);
        y2ColliderObject.SetActive(isYActive);

        // Optionally, deactivate all player objects if needed
        player1X.SetActive(isYActive);
        player2X.SetActive(isYActive);
        player1Y.SetActive(!isYActive);
        player2Y.SetActive(!isYActive);
    }
    // Update is called once per frame
    void Update()
    {
        

    }
    void FixedUpdate()
    {
        if (gameObject.activeSelf && gameObject.CompareTag("Paddle"))
        {
            Vector2 move = new Vector2(moveInput.x, moveInput.y);
            rb.linearVelocityY = move.y * speed;
        }
    else{
        Vector2 move = new Vector2(moveInput.x, moveInput.y);
            rb.linearVelocityX = move.x * speed;
    }
       
    }
}
