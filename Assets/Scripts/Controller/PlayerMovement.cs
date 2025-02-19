using UnityEditor.ShaderGraph;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;
    public int health = 100;

    private Rigidbody2D rb;
    private bool isGrounded;

    private PlayerController playerController;
    private Vector2 moveDirection;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = new PlayerController();
    }

    private void Start()
    {
        health = 100;
        EventSystem.instance.PlayerOnHurt?.Invoke();
    }

    private void OnEnable()
    {
        playerController.Enable();

        playerController.Player2D.Movement.performed += ctx => moveDirection = ctx.ReadValue<Vector2>();
        playerController.Player2D.Movement.canceled += ctx => moveDirection = Vector2.zero;
        playerController.Player2D.Jump.performed += _ => Jump();
        playerController.Player2D.Attack.performed += _ => Attack();
        playerController.Player2D.Damaged.performed += _ => TakeDamage();
    }

    private void OnDisable()
    {
        playerController.Disable();

        playerController.Player2D.Movement.performed -= ctx => moveDirection = ctx.ReadValue<Vector2>();
        playerController.Player2D.Movement.canceled -= ctx => moveDirection = Vector2.zero;
        playerController.Player2D.Jump.performed -= _ => Jump();
        playerController.Player2D.Attack.performed -= _ => Attack();
        playerController.Player2D.Damaged.performed -= _ => TakeDamage();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(moveDirection.x * moveSpeed, rb.linearVelocity.y);

        if (moveDirection.x != 0)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x) * Mathf.Sign(moveDirection.x);
            transform.localScale = newScale;
        }

        transform.rotation = Quaternion.identity;

        EventSystem.instance.PlayerOnMove?.Invoke(moveDirection.x != 0);
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false;
            EventSystem.instance.PlayerOnJump?.Invoke(true);
        }
    }

    void Attack()
    {
        EventSystem.instance.PlayerOnAttack?.Invoke();
        Debug.Log("Player Attacked!");
    }

    void TakeDamage()
    {
        health -= 10;
        EventSystem.instance.PlayerOnHurt?.Invoke();
        EventSystem.instance.PlayerHealthStatus?.Invoke(health);
        Debug.Log("Player Took Damage! Health: " + health);

        if (health <= 0)
        {
            EventSystem.instance.PlayerOnDead?.Invoke();
            Debug.Log("Player Died!");
            Destroy(this.gameObject, 1.5f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            EventSystem.instance.PlayerOnJump?.Invoke(false);
        }
    }

}
