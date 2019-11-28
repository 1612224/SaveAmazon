using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCircle : MonoBehaviour
{
    
    public Transform TargetCircleTransform;
    public GameObject Fire;
    private GameObject []Fires;

    private static DamageCircle instance;

    private Vector3 CircleSize;
    private Vector3 CirclePosition;

    private Vector3 TargetCircleSize;
    private Vector3 TargetCirclePosition;

    public float CIRCLE_SHRINK_SPEED;
    public static float shrinkTimer;
    
    private Transform circleTransform;

    public float DEFAULT_SHRINK_TIMER;
    public float DISTANCE_TEST;
    public int FIRES_AMOUNT;

    public float RescureSpotRadius;


    // Start is called before the first frame update
    void Start()
    {
        RescureSpotRadius = 10f;
        instance = this;

        CIRCLE_SHRINK_SPEED = 2.5f;
        circleTransform = transform.Find("Circle");
        CirclePosition = circleTransform.position;

        SetCircle(new Vector3(0,0), new Vector3(300f,300f));

        DEFAULT_SHRINK_TIMER = 10f;
        DISTANCE_TEST = .5f;

        FIRES_AMOUNT = 200;
        GenerateFires();

        shrinkTimer = DEFAULT_SHRINK_TIMER;
        SetTargetCircle(new Vector3(0f, 0f), new Vector3(300f*0.8f, 300f * 0.8f));

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        shrinkTimer -= Time.deltaTime;

        if(shrinkTimer < 0 && CircleSize.x - 2 * RescureSpotRadius > 0)
        {
            Vector3 sizeChangeVector =(TargetCircleSize - CircleSize).normalized;
            Vector3 CircleMoveDir = (TargetCirclePosition - CirclePosition).normalized;

            Vector3 newCircleSize = CircleSize + sizeChangeVector * Time.fixedDeltaTime* CIRCLE_SHRINK_SPEED;
            
            Vector3 NewCirclePosition = CirclePosition + CircleMoveDir * Time.fixedDeltaTime * CIRCLE_SHRINK_SPEED;

            SetCircle(NewCirclePosition, newCircleSize);
            UpdateFires();
            if (Vector3.Distance(newCircleSize, TargetCircleSize) < DISTANCE_TEST)
            {
                GenerateTargetCircle();
            }
            
        }      
    }


    private void GenerateTargetCircle()
    {
        if(CircleSize.x - 2* RescureSpotRadius > 0)
        {
            float shrinkAmount = CircleSize.x*0.2f;

            Vector3 generatedTargetCircleSize = CircleSize - new Vector3(shrinkAmount, shrinkAmount);
            Vector3 generatedTargetCirclePosition = CirclePosition + new Vector3(
                Random.Range(-shrinkAmount * CirclePosition.x / CircleSize.x, shrinkAmount * CirclePosition.x / CircleSize.x),
                Random.Range(-shrinkAmount * CirclePosition.y / CircleSize.y, shrinkAmount * CirclePosition.y / CircleSize.y));

            shrinkTimer = DEFAULT_SHRINK_TIMER;
            SetTargetCircle(generatedTargetCirclePosition, generatedTargetCircleSize);
        }        
    }
    private void SetCircle(Vector3 position ,Vector3 size)
    {
        CirclePosition = position;
        CircleSize = size;
        
        circleTransform.position = position;
        circleTransform.localScale = size;
    }

    private void SetTargetCircle(Vector3 position, Vector3 size)
    {
        TargetCircleTransform.position = position;
        TargetCircleTransform.localScale = size/50;

        TargetCirclePosition = position;
        TargetCircleSize = size;
    }

    private bool isOutsideCircle(Vector3 position)
    {
        return Vector3.Distance(position, CirclePosition) > CircleSize.x * .5f;
    }

    public static bool IsOutSideCircle(Vector3 position)
    {
        return instance.isOutsideCircle(position);
    }

    private void GenerateFires()
    {
        Fires = new GameObject[FIRES_AMOUNT];

        float angle = Mathf.PI/2;
        float r = CircleSize.x*9 /100 ;
        for(int i =0; i< FIRES_AMOUNT; ++i)
        {
            Fires[i] = Instantiate(Fire);
            Fires[i].transform.position = new Vector3(Mathf.Cos(angle) * r, Mathf.Sin(angle) * r);
            angle += Mathf.PI / 50;
        }
        Fire.SetActive(false);
    }
    private void UpdateFires()
    {
        int NEW_FIRES_AMOUNT = (int)Mathf.Round(CircleSize.x * 2f / 3f);
        for (int i = NEW_FIRES_AMOUNT; i < FIRES_AMOUNT; ++i)
        {
            Fires[i].SetActive(false);
        }
        FIRES_AMOUNT = NEW_FIRES_AMOUNT;

        float angle = Mathf.PI / 2;
        float r = CircleSize.x * 9 / 100;
        for (int i = 0; i < FIRES_AMOUNT; ++i)
        {
            Fires[i].transform.position = new Vector3(Mathf.Cos(angle) * r, Mathf.Sin(angle) * r);
            angle += Mathf.PI / (FIRES_AMOUNT/2);
        }

       
    }
}
