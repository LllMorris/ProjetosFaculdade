using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Assistant : MonoBehaviour 
{
    public TextMeshProUGUI messageText;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    private Text_Writer.TextWriterSingle textWriterSingle;
    public AudioSource talkingAudioSource;

    bool blockMessage = false;
    bool releaseTheAnswers = false;

    int indexOfTheArray = 0;
    [SerializeField] public List<string> messageTextArray;

    public string messageOne = "This is a conversation to the work of PDJ4 in puc minas",
           messageTwo = "just choose one of thoes three buttons and to change the flow of the conversation , and click the button in the right to continue",

           messageThree = "You choose the first button them",
           messageFour = "well i just said that will change the flow of the conversation but that is a lie, sorry",

           messageFive = "You choose the second button them",
           messageSix = "congratulations you have choose the right button, but i dont want to give you anything so, go away!",

           messageSeven = "You choose the third button them",
           messageEight = "I dont like your choice, so i dont wanna talk to you.",

           lastMessage = "I hope everyone liked, this was a little difficult to make, and i have sure that i made mistakes here and there but was a good chance to learn, thanks to see";

    private void Awake()
    {
        messageTextArray = new List<string>
           {
                messageOne,
                messageTwo,
           };
    }
    public void changeText()
    {
        Debug.Log(indexOfTheArray);
        if (CheckIfIsNull())
        {
            return;
        }
        if (!blockMessage)
        {
            string message = messageTextArray[indexOfTheArray];
            StartTalking();
            textWriterSingle = Text_Writer.AddWriterStatic(messageText, message, 0.1f, true, true, StopTalking);
            indexOfTheArray++;
        }

        if (indexOfTheArray == 2)
        {
            releaseTheAnswers = true;
            blockMessage = true;
        }
        else if (indexOfTheArray == 4)
        {
            blockMessage = true;
        }
        else if(indexOfTheArray == 6)
        {
            blockMessage = true;
        }

        if (indexOfTheArray >= 11)
        {
            messageTextArray.Add(lastMessage);
        }
        else return;
    }

    public void AnswerOne()
    {
        if(releaseTheAnswers)
        {
            messageTextArray.Add(messageThree);
            messageTextArray.Add(messageFour);
            //blockMessage = false;
            button1.SetActive(false);
        }
    }
    public void AnswerTwo()
    {
        if (releaseTheAnswers)
        {
            messageTextArray.Add(messageFive);
            messageTextArray.Add(messageSix);
            //blockMessage = false;
            button2.SetActive(false);
        }
    }
    public void AnswerThree()
    {
        if (releaseTheAnswers)
        {
            messageTextArray.Add(messageSeven);
            messageTextArray.Add(messageEight);
            //blockMessage = false;
            button3.SetActive(false);
        }
    }
    private bool CheckIfIsNull()
    {
        if (textWriterSingle != null && textWriterSingle.IsActive())
        {
            //Currently active writer
            textWriterSingle.WriteAllAndDestroy();
            return true;
        }
        else return false;
    }
    private void StartTalking()
    {
        talkingAudioSource.Play();
    }
    private void StopTalking()
    {
        talkingAudioSource.Stop();
        blockMessage = false;
    }
}
