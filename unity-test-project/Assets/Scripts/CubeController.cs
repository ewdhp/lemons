using UnityEngine;

/// <summary>
/// Simple cube controller to test Unity input and physics on Linux
/// </summary>
public class CubeController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 90f;
    
    [Header("Physics Test")]
    public bool enableGravity = true;
    public float jumpForce = 10f;
    
    private Rigidbody rb;
    private bool isGrounded = false;
    
    void Start()
    {
        // Get or add Rigidbody component
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        
        rb.useGravity = enableGravity;
        
        Debug.Log("CubeController initialized on Linux!");
        Debug.Log("Controls: WASD to move, Space to jump, QE to rotate");
    }
    
    void Update()
    {
        HandleInput();
        DisplayStats();
    }
    
    void HandleInput()
    {
        // Movement input
        float horizontal = Input.GetAxis("Horizontal"); // A/D keys
        float vertical = Input.GetAxis("Vertical");     // W/S keys
        
        Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
        
        // Rotation input
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        
        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && rb != null)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jump! Testing physics on Linux.");
        }
        
        // Test various input methods
        if (Input.GetKeyDown(KeyCode.T))
        {
            TestInputSystems();
        }
    }
    
    void TestInputSystems()
    {
        Debug.Log("=== Input System Test ===");
        Debug.Log($"Mouse Position: {Input.mousePosition}");
        Debug.Log($"Mouse Delta: {new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"))}");
        Debug.Log($"Mouse Buttons: L:{Input.GetMouseButton(0)} R:{Input.GetMouseButton(1)} M:{Input.GetMouseButton(2)}");
        
        // Test all keyboard keys
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(key) && key != KeyCode.T)
            {
                Debug.Log($"Key pressed: {key}");
            }
        }
    }
    
    void DisplayStats()
    {
        // Display position and rotation info every 2 seconds
        if (Time.time % 2f < Time.deltaTime)
        {
            Debug.Log($"Position: {transform.position:F2}, Rotation: {transform.eulerAngles:F1}");
            
            if (rb != null)
            {
                Debug.Log($"Velocity: {rb.velocity:F2}, Angular Velocity: {rb.angularVelocity:F2}");
            }
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
        }
        
        Debug.Log($"Collision detected with: {collision.gameObject.name}");
    }
    
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = false;
        }
    }
    
    // Gizmos for debugging
    void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.down * 0.5f, Vector3.one * 0.1f);
    }
}