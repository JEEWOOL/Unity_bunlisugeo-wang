using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleTextBlink : MonoBehaviour
{
    IEnumerator enumerator;
    Text flashingText;
    
    void Start()
    {        
        flashingText = GetComponent<Text>();
        enumerator = BlinkText();
        StartCoroutine(enumerator);
    }

    private void Update()
    {
        if(Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
        {
            flashingText.text = "";
            StopCoroutine(enumerator);
        }
    }

    public IEnumerator BlinkText()
    {
        while (true)
        {
            flashingText.text = "";
            yield return new WaitForSeconds(0.5f);
            flashingText.text = "¢º";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
