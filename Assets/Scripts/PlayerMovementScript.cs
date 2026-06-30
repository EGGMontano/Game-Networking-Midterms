using Unity.Netcode;
using UnityEngine;

public class PlayerMovementScript : NetworkBehaviour
{
    #region Movement
    [SerializeField] private float horizontal;
    [SerializeField] private float speed = 8f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private bool isFacingRight = true;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    #endregion

    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);


    private void Update()
    {
        Debug.Log(OwnerClientId + "; Random Number: " + randomNumber.Value);

        if (!IsOwner) return;

        if (Input.GetKeyDown(KeyCode.T))
        {
            randomNumber.Value = Random.Range(0, 10);
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))    // && IsGrounded() <- removed for fun
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    //private bool IsGrounded()
    //{
    //    return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    //}

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
