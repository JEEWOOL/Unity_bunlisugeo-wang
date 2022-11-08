using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{    
    public GameObject[] recycleObj;    
    public Transform[] spawnPoints;
    public Text scoreText;
    public GameObject player;
    public Text myScore;
    public Text bestScore;

    int ranRecycle;
    int ranPoint;

    // 재활용품
    public float maxSpawnDelay;
    public float curSpawnDelay;
    public float highMaxSpawnDelay = 0.1f;
    public float lowMaxSpawnDelay = 3f;
    
    // 쓰레기
    public float maxGarSpawnDelay;
    public float curGarSpawnDelay;
    public float highGarMaxSpawnDelay = 0.1f;
    public float lowGarMaxSpawnDelay = 3f;

    // 쓰레기 리스폰
    public GameObject garbageObj;    

    private void Start()
    {
        PlayerMove playerBest = player.GetComponent<PlayerMove>();
        bestScore.text = string.Format("{0:n0}", playerBest.bestScore);

        playerBest.bestScore = PlayerPrefs.GetInt("Best Score", 0);        
        bestScore.text = string.Format("{0:n0}", playerBest.bestScore);

        StartCoroutine(CreatGarRoutine());
    }

    void Update()
    {
        curSpawnDelay += Time.deltaTime;
        curGarSpawnDelay += Time.deltaTime;

        // 재활용
        if (curSpawnDelay > maxSpawnDelay)
        {
            SpawnRecycle();
            maxSpawnDelay = Random.Range(highMaxSpawnDelay, lowMaxSpawnDelay);
            curSpawnDelay = 0;
        }        

        // UI Score Update
        PlayerMove playerLogic = player.GetComponent<PlayerMove>();
        scoreText.text = string.Format("{0:n0}", playerLogic.score);

        PlayerMove playermyScore = player.GetComponent<PlayerMove>();
        myScore.text = string.Format("{0:n0}", playermyScore.score);

        PlayerMove playerBest = player.GetComponent<PlayerMove>();
        bestScore.text = string.Format("{0:n0}", playerBest.bestScore);

        if(playermyScore.score > playerBest.bestScore)
        {
            playerBest.bestScore = playermyScore.score;

            PlayerPrefs.SetInt("Best Score", playerBest.bestScore);
        }

        GarbageManager garmanager = garbageObj.GetComponent<GarbageManager>();        
    }

    IEnumerator CreatGarRoutine()
    {
        PlayerMove playermyScore = player.GetComponent<PlayerMove>();

        float resTime = 1f;

        while (true)
        {
            if (playermyScore.score > 500 && 999 > playermyScore.score)
            {
                resTime = 0.7f;                
            }
            else if(playermyScore.score > 1000 && 1499 > playermyScore.score)
            {
                resTime = 0.6f;                
            }
            else if(playermyScore.score > 1500 && 1999 > playermyScore.score)
            {
                resTime = 0.5f;                
            }
            else if(playermyScore.score > 2000 && 2499 > playermyScore.score)
            {
                resTime = 0.4f;                
            }
            else if (playermyScore.score > 2500 && 2999 > playermyScore.score)
            {
                resTime = 0.35f;                
            }
            else if (playermyScore.score > 3000 && 3499 > playermyScore.score)
            {
                resTime = 0.32f;                
            }
            else if (playermyScore.score > 3500 && 3999 > playermyScore.score)
            {
                resTime = 0.3f;                
            }
            else if (playermyScore.score > 4000)
            {
                resTime = 0.2f;                
            }

            creatGarbage();
            yield return new WaitForSeconds(resTime);
        }
    }

    private void creatGarbage()
    {
        
        Vector3 pos = Camera.main.ViewportToWorldPoint
            (new Vector3(UnityEngine.Random.Range(0.02f, 0.98f), 1.1f, 0));
        pos.z = 0.0f;
        createObject(garbageObj, pos, Quaternion.identity);
    }

    private GameObject createObject(GameObject original, Vector3 position, Quaternion rotation)
    {
        return (GameObject)Instantiate(original, position, rotation);
    }    

    void SpawnRecycle()
    {        
        ranRecycle = Random.Range(0, recycleObj.Length); // 소환될 재활용
        ranPoint = Random.Range(0, spawnPoints.Length); // 스폰갯수
        Instantiate(recycleObj[ranRecycle], spawnPoints[ranPoint].position, spawnPoints[ranPoint].rotation);
    }    

    public void ReStart()
    {
        SceneManager.LoadScene("NextScene");
    }

    public void GoTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
