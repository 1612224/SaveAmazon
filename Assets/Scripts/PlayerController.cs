using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerMovement movementController;
    public CarryingObject carryingController;
    public WaterController waterController;

    private Transform currentZone;

    public Transform leftDetect;
    public Transform rightDetect;
    public Transform upDetect;
    public Transform downDetect;

    public Transform leftDrop;
    public Transform rightDrop;
    public Transform upDrop;
    public Transform downDrop;

    public Transform leftFirePoint;
    public Transform rightFirePoint;
    public Transform upFirePoint;
    public Transform downFirePoint;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandlePickupThrow();
        HandleWater();
    }

    private void HandleWater()
    {
        if (carryingController.IsCarrying() || movementController.IsMoving())
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (movementController.direction)
            {
                case PlayerMovement.Direction.Up:
                    StartCoroutine(waterController.WaterTo(upFirePoint, upDrop));
                    break;
                case PlayerMovement.Direction.Right:
                    StartCoroutine(waterController.WaterTo(rightFirePoint, rightDrop));
                    break;
                case PlayerMovement.Direction.Down:
                    StartCoroutine(waterController.WaterTo(downFirePoint, downDrop));
                    break;
                case PlayerMovement.Direction.Left:
                    StartCoroutine(waterController.WaterTo(leftFirePoint, leftDrop));
                    break;
                default:
                    throw new UnityException("WTF");
            }
        }
    }

    void HandleMovement()
    {
        Vector2 movement;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movementController.Move(movement);
    }

    void HandlePickupThrow()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Transform detect;
            Transform drop;
            switch (movementController.direction)
            {
                case PlayerMovement.Direction.Up:
                    detect = upDetect;
                    drop = upDrop;
                    break;
                case PlayerMovement.Direction.Right:
                    detect = rightDetect;
                    drop = rightDrop;
                    break;
                case PlayerMovement.Direction.Down:
                    detect = downDetect;
                    drop = downDrop;
                    break;
                case PlayerMovement.Direction.Left:
                    detect = leftDetect;
                    drop = leftDrop;
                    break;
                default:
                    throw new UnityException("WTF");
            }
            if (carryingController.IsCarrying())
            {
                carryingController.Throw(drop.position);
            }
            else
            {
                Collider2D[] colliders = Physics2D.OverlapPointAll(detect.position);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i] && colliders[i].CompareTag("Animal"))
                    {
                        carryingController.Pickup(colliders[i].transform);
                        break;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Supply"))
        {
            waterController.ammo += 10;
            collision.collider.gameObject.SetActive(false);
        }
        else if (collision.collider.CompareTag("Obstacle"))
        {
            GetComponent<Animator>().Play("Fall", -1, 0f);
            if (carryingController.IsCarrying())
            {
                switch (movementController.direction)
                {
                    case PlayerMovement.Direction.Up:
                        carryingController.Throw(upDrop.position);
                        break;
                    case PlayerMovement.Direction.Right:
                        carryingController.Throw(rightDrop.position);
                        break;
                    case PlayerMovement.Direction.Down:
                        carryingController.Throw(downDrop.position);
                        break;
                    case PlayerMovement.Direction.Left:
                        carryingController.Throw(leftDrop.position);
                        break;
                    default:
                        throw new UnityException("WTF");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
