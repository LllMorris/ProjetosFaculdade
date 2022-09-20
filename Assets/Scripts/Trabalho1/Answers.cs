using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Answers : MonoBehaviour , AnswerControl
{
    public string text1 = "just text";
    public void Answer()
    {
        Text_Writer.instance.assistant.messageTextArray.Add(text1);
    }
}
public class AnswerTwo : MonoBehaviour, AnswerControl
{
    public string text1 = "test two";
    public void Answer()
    {
        Text_Writer.instance.assistant.messageTextArray.Add(text1);
    }
}
public class AnswerThree : MonoBehaviour, AnswerControl
{
    public string text1 = "test three";
    public void Answer()
    {
        Text_Writer.instance.assistant.messageTextArray.Add(text1);
    }
}



