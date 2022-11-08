using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    AudioSource audioSource;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    public GameObject floor;
    public GameObject gameOverSet;
    public GameObject moveBtn;

    public int score;
    public int bestScore;
    public float maxSpeed = 4;

    // Mobile Key
    int left_Value;
    int right_Value;
    bool left_Down;
    bool right_Down;
    bool left_Up;
    bool right_Up;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        score = 0;

        floor.SetActive(true);
        gameOverSet.SetActive(false);
        moveBtn.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        if (Mathf.Abs(rigid.velocity.x) < 0.45)
        {
            anim.SetBool("IsWalk", false);
        }
        else
        {
            anim.SetBool("IsWalk", true);
        }        
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal") + right_Value + left_Value;

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        }
    }

    public void GameOver()
    {
        floor.SetActive(false);
        gameOverSet.SetActive(true);
        moveBtn.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "RecycleGarbage")
        {
            audioSource.Play();
        }

        if(collision.gameObject.tag == "Garbage")
        {
            OnDamaged(collision.transform.position);
            GameOver();
        }

        if(collision.gameObject.tag == "DeadLine")
        {
            Destroy(this.gameObject);
        }
    }

    private void LateUpdate()
    {
        ScreenChk();        
    }

    

    void OnDamaged(Vector2 targetPos)
    {
        gameObject.layer = 10;
        gameObject.tag = "PlayerDie";

        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        anim.SetBool("IsJump", true);
    }

    private void ScreenChk()
    {
        Vector3 worldPos = Camera.main.WorldToViewportPoint(this.transform.position);

        if(worldPos.x < 0.05f)
        {
            worldPos.x = 0.05f;
        }
        if(worldPos.x > 0.95f)
        {
            worldPos.x = 0.95f;
        }

        this.transform.position = Camera.main.ViewportToWorldPoint(worldPos);
    }

    public void ButtonDown(string type)
    {
        switch (type)
        {
            case "L":
                left_Value = -1;
                left_Down = true;
                spriteRenderer.flipX = left_Value == -1;                
                break;

            case "R":
                right_Value = 1;
                right_Down = true;
                spriteRenderer.flipX = left_Value == 1;                
                break;
        }
    }

    public void ButtonUp(string type)
    {
        switch (type)
        {
            case "L":
                left_Value = 0;
                left_Up = true;
                break;
            case "R":
                right_Value = 0;
                right_Up = true;
                break;
        }
    }
}
