using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkingHud : MonoBehaviour
{
    public System.Action onEnd = delegate{};
    public System.Action onType = delegate{};
    [SerializeField] Text text;
    [SerializeField] float typeSpeed, endDelay = 1f;
    [SerializeField] AudioClip typeSound;
    string textInput;
    public void Initialize(string txt){
        textInput = txt;
        StartCoroutine(Type());
    }
    IEnumerator Type(){
        string temp = "";
        text.text = temp;
        int idx = 0;
        while(temp.Length < textInput.Length){
            temp += textInput[idx];
            idx++;
            text.text = temp;
            if(onType!=null)onType();
            yield return new WaitForSeconds(typeSpeed);
        }
        yield return new WaitForSeconds(endDelay);
        if(onEnd!=null)onEnd();
    }
}
