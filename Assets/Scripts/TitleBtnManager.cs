using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class TitleBtnManager : MonoBehaviour
{
    public GameObject titleText;
    public GameObject StarBtnBar;
    public GameObject ExitBtnBar;    

    private bool isTitleBtn;
    
    private void Awake()
    {
        titleText.SetActive(true);
        StarBtnBar.SetActive(false);        
        ExitBtnBar.SetActive(false);
        
        isTitleBtn = false;                      
    }

    private void Update()
    {
        if (isTitleBtn == false)
        {
            if(Input.GetMouseButtonDown(0) || Input.GetButtonDown("Jump"))
            {
                isTitleBtn = true;
                StarBtnBar.SetActive(true);                
                ExitBtnBar.SetActive(true);
                titleText.SetActive(false);                
            }
        }
    }

    public void StartBtn()
    {        
        SceneManager.LoadScene("LoadingScene");
    }    

    public void Exit()
    {        
        Application.Quit();
    }
}