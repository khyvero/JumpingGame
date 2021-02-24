using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    enum PlayerFacing
    {
        LEFT,
        RIGHT,
        MID
    }
    static Player Instance;

    PlayerFacing playerFacing;
    private bool playerJumping;
    private bool playerCatchFood;
    private float jumpWalkingPower;

    private SpriteRenderer playerGraphic;
    private float fallMultiplier = 6f;
    private float jumpVelocity = 7f;
    private float speed = 4f;

    private Rigidbody2D rb;

    AudioSource eatAudio;
    AudioSource jumpAudio;

    public Text addScorelLabel;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        eatAudio = GetComponent<AudioSource>();
        jumpAudio = gameObject.transform.Find("JumpAudio").GetComponent<AudioSource>();
    }

    void Start()
    {
        playerGraphic = GetComponent<SpriteRenderer>();

        SetSprite("player_standMid");

        playerFacing = PlayerFacing.MID;
        playerJumping = false;
        playerCatchFood = false;
        jumpWalkingPower = 0;
    }

    private void handleWalkAndJump(bool jump ,bool left ,bool right)
    {

        if (jump && !playerJumping && !playerCatchFood)
        {
            playerJumping = true;
            if (left)
            {
                rb.velocity = Vector2.up * (jumpVelocity+ jumpWalkingPower);
                SetSprite("player_jumpL01");
                playerFacing = PlayerFacing.LEFT;
            }
            else if(right)
            {
                rb.velocity = Vector2.up * (jumpVelocity + jumpWalkingPower);
                SetSprite("player_jumpR01");
                playerFacing = PlayerFacing.RIGHT;
            }
            else
            {
                rb.velocity = Vector2.up * (jumpVelocity + jumpWalkingPower);
                SetSprite("player_jumpMid");
                playerFacing = PlayerFacing.MID;
            }
            jumpAudio.Play();
        }
        else
        {
            if (left && !playerCatchFood)
            {
                rb.velocity = new Vector2(-1 * speed, rb.velocity.y);
                if (!playerJumping)
                {
                    SetSprite("player_walkL01");
                    jumpWalkingPower+= 0.02f;
                }
                playerFacing = PlayerFacing.LEFT;
            }
            else if (right && !playerCatchFood)
            {
                rb.velocity = new Vector2(1 * speed, rb.velocity.y);
                if (!playerJumping)
                {
                    SetSprite("player_walkR01");
                    jumpWalkingPower += 0.02f;
                }
                playerFacing = PlayerFacing.RIGHT;
            }
            else
            {
                jumpWalkingPower = 0;
                switch (playerFacing)
                {
                    case PlayerFacing.LEFT:
                        if (!playerJumping)
                            SetSprite("player_standL");
                        break;
                    case PlayerFacing.RIGHT:
                        if (!playerJumping)
                            SetSprite("player_standR");
                        break;
                    case PlayerFacing.MID:
                        if (!playerJumping)
                            SetSprite("player_standMid");
                        break;
                }
            }
        }

    }

    private void handleGravity()
    {
        if (rb.velocity.y < 0 && !playerCatchFood)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            
        }
    }

    void Update()
    {
        bool left = Input.GetKey(KeyCode.LeftArrow);
        bool right = Input.GetKey(KeyCode.RightArrow);
        bool jump  = Input.GetKey(KeyCode.Space) ;

        handleWalkAndJump(jump , left, right);

        handleGravity();

    }

    //[System.Obsolete]
    void OnCollisionStay2D(Collision2D collision)
    {
        if ("Ground".Equals(collision.gameObject.name))
        {
            playerJumping = false;
        }
        if ("Food(Clone)".Equals(collision.gameObject.name) && !playerCatchFood)
        {

            ScoreManager.Instance.AddToScore(10);
            addScorelLabel.text = "+ 10";
            playerCatchFood = true;
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
            SetSprite("player_jumpMid");
            collision.gameObject.transform.SetParent(gameObject.transform);
            Destroy(collision.gameObject.GetComponent<Rigidbody2D>());
            playerFacing = PlayerFacing.MID;
            collision.gameObject.transform.position = gameObject.transform.Find("CatchPosition").transform.position;


            CountDownTimer.GetInstance().CountDown("addScorelLabel_timer", 1f, () =>
            {
                addScorelLabel.text = "";
            });

            CountDownTimer.GetInstance().CountDown("playerFall_timer", 0.3f, () =>
            {
                rb.isKinematic = false;
                rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
                SetSprite("player_StandMid");
                collision.gameObject.transform.position = gameObject.transform.Find("FoodPosition").transform.position;
                eatAudio.PlayDelayed(0.8f);

            });


            CountDownTimer.GetInstance().CountDown("eatingFood_Timer" ,3.2f, () => {
                playerCatchFood = false;
                Destroy(gameObject.transform.Find("Food(Clone)").gameObject);
            });
        }
        
    }



    void SetSprite(string image)
    {
        playerGraphic.sprite = Resources.Load<Sprite>("Player/" + image);
    }
}
