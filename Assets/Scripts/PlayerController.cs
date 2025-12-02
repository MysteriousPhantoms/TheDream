using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.8f;

    [Header("Collision")]
    public LayerMask solidObjectLayer;

    [Header("Interaction")]
    public LayerMask interactLayer;
    public float interactRadius = 1f;

    [Header("UI")]
    public GameObject interactPrompt;   // Assign a Text or TMP object
    public GameObject interactBubble;   // Assign a bubble Image

    private Rigidbody2D rb;
    private Vector2 movement;
    private Vector2 lastMove;
    private Animator animator;

    public bool canMove = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        rb.gravityScale = 0;
        rb.freezeRotation = true;

        lastMove = Vector2.down; // default facing down

        // Disable UI initially
        if (interactPrompt != null) interactPrompt.SetActive(false);
        if (interactBubble != null) interactBubble.SetActive(false);
    }

    private void Update()
    {
        HandleMovement();
        HandleAnimation();
        HandleInteractionUI();
    }

    private void HandleMovement()
    {
        if (!canMove)
        {
            movement = Vector2.zero;
            return;
        }

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        if (movement.sqrMagnitude > 0.01f)
            lastMove = movement;
    }

    private void HandleAnimation()
    {
        animator.SetFloat("moveX", movement.x);
        animator.SetFloat("moveY", movement.y);
        animator.SetFloat("lastMoveX", lastMove.x);
        animator.SetFloat("lastMoveY", lastMove.y);
        animator.SetBool("isMoving", movement.sqrMagnitude > 0.01f);
    }

    private void HandleInteractionUI()
    {
        // Detect interactable objects around the player
        Collider2D obj = Physics2D.OverlapCircle(transform.position, interactRadius, interactLayer);

        if (obj != null)
        {
            if (interactPrompt != null) interactPrompt.SetActive(true);
            if (interactBubble != null) interactBubble.SetActive(true);

            // Position the UI above the NPC or item
            Vector3 worldPos = obj.transform.position + Vector3.up * 1f;
            Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
            interactPrompt.transform.position = screenPos;
            interactBubble.transform.position = screenPos;

            // Interact input
            Interactable interactable = obj.GetComponent<Interactable>();
            if (interactable != null && Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact();
            }
        }
        else
        {
            if (interactPrompt != null) interactPrompt.SetActive(false);
            if (interactBubble != null) interactBubble.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (!canMove) return;

        float currentSpeed = moveSpeed;
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            currentSpeed *= sprintMultiplier;

        Vector2 newPos = rb.position + movement * currentSpeed * Time.fixedDeltaTime;

        // Stop at walls
        if (Physics2D.OverlapCircle(newPos, 0.2f, solidObjectLayer) == null)
            rb.MovePosition(newPos);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
