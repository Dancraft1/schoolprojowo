﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextBox : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void EnableDialogueBox()
    {
        if(!GetComponent<TextMeshProUGUI>().isActiveAndEnabled)
        {
            GetComponent<TextMeshProUGUI>().enabled = true;
            textMesh = GetComponent<TextMeshProUGUI>();
        }
        if(!GetComponentInParent<Image>().isActiveAndEnabled)
            GetComponentInParent<Image>().enabled = true;
    }

    public void DisableDialogueBox()
    {
        if(GetComponent<TextMeshProUGUI>().isActiveAndEnabled)
            GetComponent<TextMeshProUGUI>().enabled = false;
        if(GetComponentInParent<Image>().isActiveAndEnabled)
            GetComponentInParent<Image>().enabled = false;
    
    }

    public float speed = 10;
    public bool isDialogueSkipped = false;

    public void EnableChoices(int count)
    {
        numberOfUsedChoices = count;
        if(count >= 1)
        {
            GameObject.Find("Text Choice 1").GetComponent<TextMeshProUGUI>().enabled = true;
            GameObject.Find("Choice Button 1").GetComponent<Button>().interactable = true;
        }
        if(count >= 2)
        {
            GameObject.Find("Text Choice 2").GetComponent<TextMeshProUGUI>().enabled = true;
            GameObject.Find("Choice Button 2").GetComponent<Button>().interactable = true;
        }
        if(count >= 3)
        {
            GameObject.Find("Text Choice 3").GetComponent<TextMeshProUGUI>().enabled = true;
            GameObject.Find("Choice Button 3").GetComponent<Button>().interactable = true;
        }
    }
    public void DisableChoices(int count)
    {

        if(count >= 1)
        {
            GameObject.Find("Text Choice 1").GetComponent<TextMeshProUGUI>().enabled = false;
            GameObject.Find("Choice Button 1").GetComponent<Button>().interactable = false;
        }
        if(count >= 2)
        {
            GameObject.Find("Text Choice 2").GetComponent<TextMeshProUGUI>().enabled = false;
            GameObject.Find("Choice Button 2").GetComponent<Button>().interactable = false;
        }
        if(count >= 3)
        {
            GameObject.Find("Text Choice 3").GetComponent<TextMeshProUGUI>().enabled = false;
            GameObject.Find("Choice Button 3").GetComponent<Button>().interactable = false;
        }
        numberOfUsedChoices = 0;
    }
    
    int numberOfUsedChoices = 0;
    public void SetResponses(GameObject sender, string response1, string event1){
        EnableChoices(1);
        GameObject target = GameObject.Find("Text Choice 1").gameObject;
        target.GetComponent<TextChoice>().target = sender;
        target.GetComponent<TextChoice>().eventFunction = event1;
        target.GetComponent<TextMeshProUGUI>().SetText(response1);
        DisableChoices(3);
        numberOfUsedChoices = 1;
    }
    public void SetResponses(GameObject sender, string response1, string event1, string response2, string event2){
        EnableChoices(2);
        GameObject target = GameObject.Find("Text Choice 1").gameObject;
        target.GetComponent<TextChoice>().target = sender;
        target.GetComponent<TextChoice>().eventFunction = event1;
        target.GetComponent<TextMeshProUGUI>().SetText(response1);
        target = GameObject.Find("Text Choice 2").gameObject;
        target.GetComponent<TextChoice>().target = sender;
        target.GetComponent<TextChoice>().eventFunction = event2;
        target.GetComponent<TextMeshProUGUI>().SetText(response2);
        DisableChoices(3);
        numberOfUsedChoices = 2;
    }
    public void SetResponses(GameObject sender, string response1, string event1, string response2, string event2, string response3, string event3){
        EnableChoices(3);
        GameObject target = GameObject.Find("Text Choice 1").gameObject;
        target.GetComponent<TextChoice>().target = sender;
        target.GetComponent<TextChoice>().eventFunction = event1;
        target.GetComponent<TextMeshProUGUI>().SetText(response1);
        target = GameObject.Find("Text Choice 2").gameObject;
        target.GetComponent<TextChoice>().target = sender;
        target.GetComponent<TextChoice>().eventFunction = event2;
        target.GetComponent<TextMeshProUGUI>().SetText(response2);
        target = GameObject.Find("Text Choice 3").gameObject;
        target.GetComponent<TextChoice>().target = sender;
        target.GetComponent<TextChoice>().eventFunction = event3;
        target.GetComponent<TextMeshProUGUI>().SetText(response3);
        DisableChoices(3);
        numberOfUsedChoices = 3;
    }

    public void DialogueWithTitle(string title, string text)
    {
        EnableDialogueBox();
        StopAllCoroutines(); //to immedietly override existing and avoid overlap
        StartCoroutine(DialogueWithTitleProcessor(title, text));
    }
    IEnumerator DialogueWithTitleProcessor(string title, string text)
    {
        isDialogueSkipped = false;
        //todo: add a skip dialogue when another dialogue starts
        string titleText = "<color=\"yellow\">" + title + ": ";
        string bodyText = "<color=\"white\">";
        foreach(char t in text)
        {
        bodyText += t;
        textMesh.SetText(titleText + bodyText);
        if(!isDialogueSkipped)
            yield return new WaitForSecondsRealtime(1.0f/speed);
        else
            yield return new WaitForSecondsRealtime(0);
        }
        EnableChoices(numberOfUsedChoices);
    }
    
    public void Dialogue(string text)
    {
        EnableDialogueBox();
        StopAllCoroutines(); //to immedietly override existing and avoid overlap
        StartCoroutine(DialogueProcessor(text));
    }
    IEnumerator DialogueProcessor(string text)
    {
        isDialogueSkipped = false;
        string sentText = "";
        foreach(char t in text)
        {
        sentText += t;
        textMesh.SetText(sentText);
        if(!isDialogueSkipped)
            yield return new WaitForSecondsRealtime(1.0f/speed);
        else
            yield return new WaitForSecondsRealtime(0);
        }
        EnableChoices(numberOfUsedChoices);
    }

    void Update()
    {
        //mechanism for skipping text typing animation
        if(Input.GetMouseButtonDown(0))
            isDialogueSkipped = true;
    }
}
