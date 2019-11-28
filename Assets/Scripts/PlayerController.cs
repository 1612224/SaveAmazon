using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public enum Tools
    {
        Water, Hammer,
        TotalNum
    }

    public bool toolUseLock = false;
    private List<Tools> tools;
    private int currentTool = 0;
    private Dictionary<Tools, Action> toolHandleFuncs;
    private Dictionary<Tools, Image> toolIconBgs;
    private Dictionary<Tools, GameObject> toolIcons;

    public GameObject WaterIcon;
    public GameObject HammerIcon;
    public Image WaterIconBg;
    public Image HammerIconBg;

    [Header("Controllers")]
    public PlayerMovement movementController;
    public CarryingObject carryingController;
    public WaterController waterController;
    public HammerController hammerController;

    [Header("Carry detection zones")]
    public Transform leftDetect;
    public Transform rightDetect;
    public Transform upDetect;
    public Transform downDetect;
    private Transform currentDetect;

    [Header("Throw & water target zones")]
    public Transform leftDrop;
    public Transform rightDrop;
    public Transform upDrop;
    public Transform downDrop;
    private Transform currentDrop;

    [Header("Water shoot points")]
    public Transform leftFirePoint;
    public Transform rightFirePoint;
    public Transform upFirePoint;
    public Transform downFirePoint;
    private Transform currentFirePoint;

    [Header("Hammer hit zones")]
    public Transform leftHammer;
    public Transform rightHammer;
    public Transform upHammer;
    public Transform downHammer;
    private Transform currentHammer;

    private void Start()
    {
        toolHandleFuncs = new Dictionary<Tools, Action> {
            { Tools.Water, HandleWater },
            {Tools.Hammer, HandleHammer },
        };
        toolIconBgs = new Dictionary<Tools, Image> {
            { Tools.Water, WaterIconBg },
            {Tools.Hammer, HammerIconBg },
        };
        toolIcons = new Dictionary<Tools, GameObject> {
            { Tools.Water, WaterIcon },
            {Tools.Hammer, HammerIcon },
        };
        tools = new List<Tools> { Tools.Water };
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandlePickupThrow();
        HandleToolChange();

        // if is carrying something or is moving, cannot use tools
        if (carryingController.IsCarrying() || movementController.IsMoving())
        {
            return;
        }

        HandleToolUse();
    }

    public void AddTool(Tools type)
    {
        if (!HaveTool(type))
        {
            tools.Add(type);
            toolIcons[type].SetActive(true);
            toolIconBgs[type].color = Color.black;
        }
    }

    public bool HaveTool(Tools type)
    {
        for (int i = 0; i < tools.Count; i++)
        {
            if (tools[i] == type)
            {
                return true;
            }
        }
        return false;
    }

    private void HandleToolUse()
    {
        if (toolUseLock)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Tools tool = tools[currentTool];
            Action handleFunc = toolHandleFuncs[tool];
            handleFunc();
        }
    }

    private void HandleToolChange()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            toolIconBgs[tools[currentTool]].color = Color.black;
            currentTool = (currentTool + 1) % tools.Count;
            toolIconBgs[tools[currentTool]].color = Color.white;
        }
    }

    private void HandleHammer()
    {
        StartCoroutine(hammerController.HammerTo(currentHammer));
    }

    private void HandleWater()
    {
        StartCoroutine(waterController.WaterTo(currentFirePoint, currentDrop));
    }

    void HandleMovement()
    {
        Vector2 movement;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movementController.Move(movement);
        switch (movementController.direction)
        {
            case PlayerMovement.Direction.Up:
                currentDetect = upDetect;
                currentDrop = upDrop;
                currentFirePoint = upFirePoint;
                currentHammer = upHammer;
                break;
            case PlayerMovement.Direction.Right:
                currentDetect = rightDetect;
                currentDrop = rightDrop;
                currentFirePoint = rightFirePoint;
                currentHammer = rightHammer;
                break;
            case PlayerMovement.Direction.Down:
                currentDetect = downDetect;
                currentDrop = downDrop;
                currentFirePoint = downFirePoint;
                currentHammer = downHammer;
                break;
            case PlayerMovement.Direction.Left:
                currentDetect = leftDetect;
                currentDrop = leftDrop;
                currentFirePoint = leftFirePoint;
                currentHammer = leftHammer;
                break;
            default:
                throw new UnityException("WTF Unknown direction");
        }
    }

    void HandlePickupThrow()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (carryingController.IsCarrying())
            {
                carryingController.Throw(currentDrop.position);
            }
            else
            {
                Collider2D[] colliders = Physics2D.OverlapPointAll(currentDetect.position);
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
            collision.collider.gameObject.GetComponent<SupplyController>().Supply(gameObject);
        }
        else if (collision.collider.CompareTag("Obstacle"))
        {
            GetComponent<Animator>().Play("Fall", -1, 0f);
            if (carryingController.IsCarrying())
            {
                carryingController.Throw(currentDrop.position);
            }
        }
    }
}
