using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMGR : MonoBehaviour
{
    [SerializeField] Text killsText;
    [SerializeField] Text winLoseText;


    public void setKillText(int kills)
    {
        killsText.text = kills.ToString();
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
}
