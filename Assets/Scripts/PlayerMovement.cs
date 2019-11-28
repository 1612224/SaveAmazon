using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Collections;

public class PlayerMovement : MonoBehaviour
{
    public enum Direction
    {
        Up, Right, Down, Left
    }

    // movement
    public Rigidbody2D body;
    public int speed;
    private Vector2 movement;
    public Direction direction;

    public bool movementLock = false;
    public SoundController soundController;

    // animation
    public Animator animator;

    private void Start()
    {
        animator.SetFloat("LastHorizontal", 0);
        animator.SetFloat("LastVertical", -1);
    }

    // Update is called once per frame
    void Update()
    {
        // update facing & direction if is moving
        if (movement.sqrMagnitude > 0)
        {
            animator.SetFloat("LastHorizontal", movement.x);
            animator.SetFloat("LastVertical", movement.y);
        }

        // update movement
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Movement", movement.sqrMagnitude);
        animator.SetInteger("Speed", speed);
    }

    private void FixedUpdate()
    {
        body.position += movement.normalized * speed * Time.fixedDeltaTime;
    }

    public void Move(Vector2 mv)
    {
        if (movementLock)
        {
            movement = Vector2.zero;
            return;
        }

        movement = mv;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (mv.sqrMagnitude > 0)
        {
            soundController.PlayRunningSound();
            if ((Mathf.Abs(mv.x) - Mathf.Abs(mv.y)) > 0)
            {
                direction = mv.x > 0 ? Direction.Right : Direction.Left;
            }
            else if (mv.y > 0)
            {
                direction = Direction.Up;
            }
            else
            {
                direction = Direction.Down;
            }
        }
    }

    public bool IsMoving()
    {
        return movement.sqrMagnitude > 0;
    }
}
