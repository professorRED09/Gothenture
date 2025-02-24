using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : Subject
{
    [Header("Ref")]
    public GameObject dialogueUI; // ref for ui in the scene
    public TMP_Text dialogueText; 
    public Text nameText;    

    [Header("Setting")]
    [SerializeField] float delayTime; // delay time to display a typing animation

    Queue<string> sentences;   // store sentences from a speaker

    // Start is called before the first frame update
    void Start()
    {        
        sentences = new Queue<string>();
    }


    public void StartDialogue(Dialogue dialogue)
    {
        NotifyObserver(PlayerAction.Talk);

        // display dialogue ui
        dialogueUI.SetActive(true);
        nameText.text = dialogue.name;

        // clear old sentences
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    // gradually typing a letter to form a sentence
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(delayTime);
        }
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false);
        NotifyObserver(PlayerAction.Leave);        
        print("End of conversation");
    }
}
