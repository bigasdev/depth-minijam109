using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BigasTools;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    [TextArea]
    [SerializeField] string textInput;
    [SerializeField] GameObject tutorialBox, mainMenu;
    [SerializeField] Text text;
    [SerializeField] float typeSpeed, endDelay = 1f;
    [SerializeField] AudioClip typingSound, startSound;
    [SerializeField] AudioSource sfxSource;
    bool finished = false;
    private void Update() {
        if(Input.anyKey){
            StartTextAndGame();
            TryToFinish();
        }
    }
    public void StartTextAndGame(){
        if(tutorialBox.activeSelf)return;
        tutorialBox.SetActive(true);
        mainMenu.SetActive(false);
        sfxSource.PlayOneShot(startSound);
        StartCoroutine(Type());
    }
    public void TryToFinish(){
        if(!tutorialBox.activeSelf || text.text.Length <= 10)return;
        if(text.text == textInput && finished){
            SceneManager.LoadScene("Main");
        }
        if(text.text != textInput){
            text.text = textInput;
            StopAllCoroutines();
            finished = true;
            return;
        }
    }

    IEnumerator Type(){
        yield return new WaitForSeconds(.5f);
        string temp = "";
        text.text = temp;
        int idx = 0;
        while(temp.Length < textInput.Length){
            temp += textInput[idx];
            idx++;
            text.text = temp;
            sfxSource.PlayOneShot(typingSound);
            yield return new WaitForSeconds(typeSpeed);
        }
        finished = true;
        yield return new WaitForSeconds(endDelay);
    }
}
