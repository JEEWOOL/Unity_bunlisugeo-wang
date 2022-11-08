using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextManager : MonoBehaviour
{
    private void Update()
    {
        SceneManager.LoadScene("GameScene");
    }
}
