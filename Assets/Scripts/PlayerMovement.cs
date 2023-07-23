using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// sử dụng file để lưu trữ cài đặt, level, idPlayer
public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public Animator anm;
    BoxCollider2D coll;
    SpriteRenderer spr;
    [SerializeField] LayerMask jumpAbleGround, jumpAblePlat;
    GameObject fan;
    enum MovemenState { idle, run, jump, fall, doubleJump, wall };
    MovemenState state;
    AudioSource aus;
    public AudioClip sound;

    public bool isJump, isDoubleJump, isWall, isCheckJumpWall, isFly;
    float k;
    GameController gameController;
    Vector2 startingPoint;
    int leftTouch = 36;
    Camera _camera;
    public Vector3 pastPlayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anm = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        aus = FindObjectOfType<AudioSource>();
        gameController = FindObjectOfType<GameController>();
        _camera = FindObjectOfType<Camera>();
        isJump = false;
        isDoubleJump = false;
        isWall = false;
        isCheckJumpWall = false;
        isFly = false;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
        int i = 0;
        while (i < Input.touchCount)
        {
            Touch t = Input.GetTouch(i);
            Vector2 touchPos = getTouchPosition(t.position); // * -1 for perspective cameras
            if (t.phase == TouchPhase.Began)
            {
                if (t.position.x > Screen.width / 2)
                {
                    if (!isFly)
                    {
                        if (isWall)
                        {
                            if (gameController.isMusic)
                                aus.PlayOneShot(sound);
                            isCheckJumpWall = true;
                            isJump = true;
                            isWall = false;
                            isDoubleJump = false;
                            rb.gravityScale = 1;
                            if (isWallLeft())
                            {
                                spr.flipX = false;
                                transform.position += new Vector3(0.3f, 0, 0);
                                rb.velocity = new Vector2(3f, 6f);
                            }
                            else
                            {
                                spr.flipX = true;
                                transform.position -= new Vector3(0.3f, 0, 0);
                                rb.velocity = new Vector2(-3f, 6f);
                            }
                        }
                        else if (isGrounded() || isPlat())
                        {
                            if (gameController.isMusic)
                                aus.PlayOneShot(sound);
                            isJump = true;
                            isDoubleJump = false;
                            rb.velocity = new Vector2(rb.velocity.x, 6f);
                        }
                        else if (isJump)
                        {
                            if (gameController.isMusic)
                                aus.PlayOneShot(sound);
                            isJump = false;
                            isDoubleJump = true;
                            isCheckJumpWall = false;
                            rb.velocity = new Vector2(rb.velocity.x, 6f);
                        }
                    }
                }
                else if (leftTouch==36)
                {
                    leftTouch = t.fingerId;
                    startingPoint = touchPos;
                    pastPlayer = transform.position;
                    Debug.Log("cham");
                }
            }
            else if (t.phase == TouchPhase.Moved && leftTouch == t.fingerId)
            {
                Vector2 change = transform.position - pastPlayer;
                pastPlayer = transform.position; // mấu chốt
                Debug.Log(change);
                startingPoint += change;
                Vector2 offset = touchPos - startingPoint;
                if (offset.x > 0)
                    k = 1f;
                else
                    k = -1f;
            }
            else if (t.phase == TouchPhase.Ended && leftTouch == t.fingerId)
            {
                leftTouch = 36;
                k = 0f;
                pastPlayer = transform.position;
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            ++i;
            if (k != 0f)
                if (!isCheckJumpWall && !isWall)
                    rb.velocity = new Vector2(k * 6f, rb.velocity.y);
        }
        if (isFly)
        {
            if (transform.position.x > fan.transform.position.x + 1.2f || transform.position.x < fan.transform.position.x - 1.2f)
            {
                isFly = false;
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            }
            else
                rb.velocity = new Vector2(rb.velocity.x, 3f);
        }
    }
    Vector2 getTouchPosition(Vector2 touchPosition)
    {
        return _camera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, transform.position.z));
    }
    void UpdateAnimation()
    {
        if (k > 0f && !isWall)
        {
            state = MovemenState.run;
            spr.flipX = false;
        }
        else if (k < 0f && !isWall)
        {
            state = MovemenState.run;
            spr.flipX = true;
        }
        else
            state = MovemenState.idle;
        if (rb.velocity.y > 0.1f)
        {
            if (isDoubleJump && (!isGrounded() && !isPlat()))
                state = MovemenState.doubleJump;
            else
                state = MovemenState.jump;
        }
        else if (rb.velocity.y < -0.1f)
        {
            isCheckJumpWall = false;
            state = MovemenState.fall;
        }
        if (isCheckJumpWall)
            state = MovemenState.jump;
        if (!isFly && !isWall && (isJump || isDoubleJump) && (!isGrounded() && !isPlat()) && (isWallLeft() || isWallRight()))
        {
            isCheckJumpWall = false;
            isWall = true;
            isJump = false;
            isDoubleJump = false;
            rb.gravityScale = 0;
            rb.velocity = new Vector2(0, 0);
            if (isWallLeft())
                spr.flipX = true;
            else
                spr.flipX = false;
            state = MovemenState.wall;
        }
        anm.SetInteger("state", (int)state);
    }
    bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpAbleGround);
    }
    bool isPlat()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, 0.1f, jumpAblePlat);
    }
    bool isWallRight()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.right, 0.1f, jumpAbleGround);
    }
    bool isWallLeft()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.left, 0.1f, jumpAbleGround);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trampoline"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }
        else if (collision.gameObject.CompareTag("Fan"))
        {
            fan = collision.gameObject;
            isFly = true;
        }
    }
}
