using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    public Sprite[] birdSprites = new Sprite[3];
    public string[] animationNames = { "WingFlapYellow", "WingFlapBlue", "WingFlapRed" };
    private SpriteRenderer birdRenderer;
    private int rand;
    public GameManager gameManager;
    private Rigidbody2D rb;
    public float jumpForce;
    public float jumpForceDefault = 2.5f;

    private bool canJump = false;
    private bool firstJump = true;
    private bool canRotate = true;
    private float delayTimer = 0f;
    private float timeElapsed = 0f;
    private float lastJumpTime;

    public float upRotationDuration = .5f;
    public float upRotationEnd = 45f;
    private float upRotationStart = 0f;
    private float upRotationLerpedValue;
    public float upRotationDelayMultiplier = 4f;

    public float downRotationDelay = 1f;
    public float downRotationDuration = .5f;
    public float downRotationEnd = -90f;
    private float downRotationStart = 45f;
    private float downRotationLerpedValue;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        Random.seed = System.DateTime.Now.Millisecond;
        rand = Random.Range(0, 3);
        animator.Play(animationNames[rand]);
        Debug.Log("RandBird = " + rand);
        birdRenderer = gameObject.GetComponent<SpriteRenderer>();
        birdRenderer.sprite = birdSprites[rand];
        rb = GetComponent<Rigidbody2D>();
        ScoreController.score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space)
            || Input.GetKeyDown(KeyCode.Mouse0)
            || Input.GetKeyDown(KeyCode.Return)
            || Input.GetKeyDown(KeyCode.UpArrow))
            && gameManager.gameOver == false)
        {
            gameManager.gameStarted = true;
            gameManager.BirdJump();
            Jump();
            lastJumpTime = Time.time;
        }
    }

    private void FixedUpdate()
    {
        if (gameManager.gameStarted == true)
        {
            if (Time.time - lastJumpTime >= downRotationDelay)
            {
                canJump = false;
            }
            if (canJump)
            {
                RotateBirdUp();
            }
            else if (!canJump)
            {
                RotateBirdDown();
            }
        }
    }

    private void Jump()
    {
        canJump = true;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    void RotateBirdUp()
    {
        if (firstJump == true)
        {
            if (timeElapsed < upRotationDuration)
            {
                float t = timeElapsed / upRotationDuration;
                upRotationLerpedValue = Mathf.Lerp(upRotationStart, upRotationEnd, t); // Start, end, step
                timeElapsed += Time.deltaTime;
                transform.rotation = Quaternion.Euler(0f, 0f, upRotationLerpedValue);
            }
            else
            {
                upRotationLerpedValue = upRotationEnd;
                delayTimer = Time.time;
                timeElapsed = 0;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                firstJump = false;
                canJump = false;
                canRotate = true;
            }
        }
    }
    void RotateBirdDown()
    {
        if (Time.time - lastJumpTime >= downRotationDelay && canRotate)
        {
            if (timeElapsed < downRotationDuration)
            {
                float t = timeElapsed / downRotationDuration;
                downRotationLerpedValue = Mathf.Lerp(downRotationStart, downRotationEnd, t); // Start, end, step
                timeElapsed += Time.deltaTime;
                transform.rotation = Quaternion.Euler(0f, 0f, downRotationLerpedValue);
            }
            else
            {
                downRotationLerpedValue = downRotationEnd;
                timeElapsed = 0;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                canJump = true;
                canRotate = false;
            }
            firstJump = true;
        }
    }
}
