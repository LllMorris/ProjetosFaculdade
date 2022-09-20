using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public AnswerControl control;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("test");
            control = new Answers();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            control = new AnswerTwo();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            control = new AnswerThree();
        }
    }

}
