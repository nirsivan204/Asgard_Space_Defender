using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMGR : MonoBehaviour
{
    [SerializeField] Text killsText;
    [SerializeField] Text winLoseText;
    [SerializeField] Text numOfLivesText;
    [SerializeField] Text timerText;
    [SerializeField] Text controlsText;


    public void setKillText(int kills)
    {
        killsText.text = kills.ToString();
    }

    public void SetLivesText(int lives)
    {
        numOfLivesText.text = lives.ToString();
    }

    public void OnLevelFinish(bool isWin)
    {
        string textToShow = "";
        if (isWin)
        {
            textToShow = "You Win";
        }
        else
        {
            textToShow = "You Lose";
        }
        winLoseText.text = textToShow;
        winLoseText.gameObject.SetActive(true);
    }

    public IEnumerator StartTimer(int timeToCount, string textToShow)
    {
        timerText.gameObject.SetActive(true);
        for (int i = timeToCount; i > 0; i--)
        {
            timerText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        timerText.text = textToShow.ToString();
        yield return new WaitForSeconds(1);
        timerText.gameObject.SetActive(false);
    }

    internal void ShowControls(bool isShow)
    {
        controlsText.gameObject.SetActive(isShow);
    }
}
