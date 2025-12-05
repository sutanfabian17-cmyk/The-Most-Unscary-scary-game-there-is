using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private PlayerControls controls;

    void Awake()
    {
        controls = new PlayerControls();
    }

    void OnEnable()
    {
        if (controls != null)
        {
            controls.Player.Enable();
            controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
            controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        }
    }

    void OnDisable()
    {
        if (controls != null)
        {
            controls.Player.Disable();
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.tag = "Player";
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * speed; // ✅ correct property
    }
}
