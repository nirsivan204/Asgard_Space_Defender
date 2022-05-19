using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMGR : MonoBehaviour
{
    [SerializeField] Text killsText;

    public void setKillText(int kills)
    {
        killsText.text = kills.ToString();
    }
}
