using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleManager : MonoBehaviour
{
    public int recycleScore;
    public float speed;
    Rigidbody2D rigid;
    public GameObject player;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.down * speed;
    }    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Floor" || collision.gameObject.tag == "DeadLine")
        {
            Destroy(this.gameObject);
        }        

        if(collision.gameObject.tag == "Player")
        {
            
            PlayerMove playerLogic = player.GetComponent<PlayerMove>();
            playerLogic.score += recycleScore;
            Destroy(this.gameObject);
        }
    }
}
