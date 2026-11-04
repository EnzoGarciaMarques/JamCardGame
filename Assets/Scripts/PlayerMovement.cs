using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb2d;
    public float moveSpeed = 5f;
    public float upSpeed = 5f;
    float horizontalMovement;
    float verticalMovement;
    [SerializeField] private SpriteRenderer sr;
    InputSystem_Actions input;

    private void Awake()
    {
        input = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        input.Player2D.WASD.performed += Move;
        input.Player2D.WASD.canceled += Move;
        input.Player2D.Enable();
    }
    private void OnDisable()
    {
        input.Player2D.WASD.performed -= Move;
        input.Player2D.WASD.canceled -= Move;
        input.Player2D.Disable();
    }

    void Update()
    {
        rb2d.linearVelocity = new Vector2(horizontalMovement * moveSpeed,verticalMovement * upSpeed);
    
        if (horizontalMovement > 0)
        {
            sr.flipX = false;
        }

        else if (horizontalMovement < 0)
        {
            sr.flipX = true;
        }
    }


    public void Move(InputAction.CallbackContext ctx)
    {
        Vector2 movement = ctx.ReadValue<Vector2>();

        horizontalMovement = movement.x;
        verticalMovement = movement.y;
    }
}
