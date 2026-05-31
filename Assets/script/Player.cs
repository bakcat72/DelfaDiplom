using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;

    private Rigidbody rb;
    private Vector3 input;
    private bool isGrounded;

    private void Start() => rb = GetComponent<Rigidbody>();

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        input = (transform.right * x + transform.forward * z).normalized;
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.3f, groundMask);

        if (Input.GetButtonDown("Jump") && isGrounded)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = input * speed;
        targetVelocity.y = rb.linearVelocity.y;
        rb.linearVelocity = targetVelocity;
    }
}
