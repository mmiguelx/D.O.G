using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryText : MonoBehaviour
{
    private float actualDelay; //delay actual del texto
    private bool speeded; //el testo va a velocidad x2?
    public string[] cutsceneTexts; //textos del cutscene
    public float[] textDelays; //delays de letras de cada texto
    public int selectedText; //texto seleccionado
    private string currentText = ""; //texto actual
    private bool writing = false; //esta escribiendo?

    public AudioSource ads; //reproductor de clips de audio
    public AudioClip[] ac; //clips de audio
    public int[] whatClip; //que clip reproducir en la escena actual

    public int nextScene; //la siguiente escena a la que irá al terminar la cutscene

    public FadeLoader bl;

    private void Awake()
    {
        Cutscene c = CutsceneLoader.instance.GetCutscene();
        cutsceneTexts = c.texts;
        textDelays = c.textDelays;
        ac = c.audioClips;
        whatClip = c.arep;
        nextScene = c.nextScene;
    }
    void Start()
    {
        this.GetComponent<Text>().text = "";
        selectedText = 0;
        StartCoroutine(startingDelay());
    }

    IEnumerator startingDelay()
    {
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(showText());
    }

    IEnumerator showText()
    {
        writing = true;
        actualDelay = textDelays[selectedText];
        speeded = false;
        currentText = "";
        for (int i = 0; i < cutsceneTexts[selectedText].Length; i++)
        {
            if (cutsceneTexts[selectedText][i] != ' ')
                ads.PlayOneShot(ac[whatClip[selectedText]]);
            currentText += cutsceneTexts[selectedText][i];
            this.GetComponent<Text>().text = currentText;
            yield return new WaitForSeconds(actualDelay);
        }
        writing = false;
    }

    public void clicked()
    {
        if (writing == true)
        {
            if (speeded == false)
            {
                actualDelay /= 2;
                speeded = true;
            }
        }
        else
        {
            selectedText++;
            if (selectedText == cutsceneTexts.Length)
                bl.fadeExit(nextScene);
            else
                StartCoroutine(showText());
        }
    }
}
