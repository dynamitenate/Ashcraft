using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float jumpForce;

	private float moveInput;
	private Rigidbody2D rb;

	// Ground check
	private bool isGrounded;
	public Transform groundCheck;
	public float checkRadius;
	public LayerMask groundLayer;

	// Jumping
	public int extraJumps;
	private int remainingJumps;

	void Start()
    {
		remainingJumps = extraJumps;
		rb = GetComponent<Rigidbody2D>();   
    }

	void FixedUpdate()
	{
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

		moveInput = Input.GetAxis("Horizontal");
 		rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
	}

	void Update()
    {
		// Reset extraJumps when player hits ground again
		if (isGrounded)
		{
			remainingJumps = extraJumps;
		}

		// Handle the jumping
		if (Input.GetKeyDown(KeyCode.UpArrow) && remainingJumps > 0)
		{
			rb.velocity = Vector2.up * jumpForce;
			remainingJumps--;
		}
		else if (Input.GetKeyDown(KeyCode.UpArrow) && remainingJumps == 0 && isGrounded)
		{
			rb.velocity = Vector2.up * jumpForce;
		}
    }
}
