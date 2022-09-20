using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Text_Writer : MonoBehaviour
{
    public static Text_Writer instance;
    public UI_Assistant assistant;

    private List<TextWriterSingle> textWriterSingleList; //To be able to handle multiples texts
    private void Awake()
    {
        instance = this;
        textWriterSingleList = new List<TextWriterSingle>();
    }

    public static TextWriterSingle AddWriterStatic(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, bool removeWriterBeforeAdd ,Action onCompleteWriting)
    {
        if (removeWriterBeforeAdd) { instance.RemoveWriter(uiText); }

        return instance.AddWriter(uiText, textToWrite, timePerCharacter, invisibleCharacters , onCompleteWriting);
    }
    private TextWriterSingle AddWriter(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, Action onCompleteWriting)
    {
        TextWriterSingle textWriterSingle = new TextWriterSingle(uiText, textToWrite, timePerCharacter, invisibleCharacters , onCompleteWriting);
        textWriterSingleList.Add(textWriterSingle);
        return textWriterSingle;
    }

    public static void RemoveWriterStatic(TextMeshProUGUI uiText)
    {
        instance.RemoveWriter(uiText);
    }

    private void RemoveWriter(TextMeshProUGUI uiText)
    {
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            if(textWriterSingleList[i].GetText() == uiText)
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < textWriterSingleList.Count; i++)
        {
            bool destroyInstace = textWriterSingleList[i].Update();
            if(destroyInstace)
            {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    public class TextWriterSingle
    {
        private TextMeshProUGUI uiText;
        private string textToWrite;
        private int characterIndex;
        private float timePerCharacter, timer;
        private bool invisibleCharacters;
        private Action onCompleteWriting;
        public TextWriterSingle(TextMeshProUGUI uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, Action onCompleteWriting)
        {
            this.uiText = uiText;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            this.invisibleCharacters = invisibleCharacters;
            this.onCompleteWriting = onCompleteWriting;
            characterIndex = 0;
        }

        public bool Update()
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                //Display the next character
                timer += timePerCharacter;
                characterIndex++;
                string text = textToWrite.Substring(0, characterIndex);

                if (invisibleCharacters)
                {
                    //Make the character apper in the left of the textBox
                    text += "<color=#00000000>" + textToWrite.Substring(characterIndex) + "<color>";
                }

                uiText.text = text;


                if (characterIndex >= textToWrite.Length)
                {
                    //Entire string displayed
                    if(onCompleteWriting != null) onCompleteWriting();
                    return true;
                }
            }
            return false;
        }
        public TextMeshProUGUI GetText()
        {
            return uiText;
        }
        public bool IsActive()
        {
            return characterIndex < textToWrite.Length;
        }
        public void WriteAllAndDestroy()
        {
            uiText.text = textToWrite;
            characterIndex = textToWrite.Length;
            if (onCompleteWriting != null) onCompleteWriting();
            RemoveWriterStatic(uiText);
        }
    }

}
