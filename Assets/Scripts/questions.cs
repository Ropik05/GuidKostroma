using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class questions : MonoBehaviour
{
    public Text Question;
    public Text answ0;
    public Text answ1;
    public Text answ2;
    public Text answ3;
    public GameObject canv;

    // Start is called before the first frame update

    void Update()
    {
        Question.text = Quest.points[Quest.CurrentPos].Question;
        answ0.text = Quest.points[Quest.CurrentPos].Answers[0];
        answ1.text = Quest.points[Quest.CurrentPos].Answers[1];
        answ2.text = Quest.points[Quest.CurrentPos].Answers[2];
        answ3.text = Quest.points[Quest.CurrentPos].Answers[3];
    }
    public void ChekAnsw(int i)
    {
        if (i == Quest.points[Quest.CurrentPos].CorrectIndex)
        {
            Quest.GameState = 6;
            canv.SetActive(false);
        }
    }
    // Update is called once per frame
}
