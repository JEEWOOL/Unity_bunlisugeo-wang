using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageManager : MonoBehaviour
{
    Rigidbody2D rigid;
    public GameObject player;
    public float speed;
    public int garbageScore;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DeadLine" || collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Floor")
        {
            PlayerMove playerLogic = player.GetComponent<PlayerMove>();
            playerLogic.score += garbageScore;
            Destroy(this.gameObject);
        }
    }    
}
