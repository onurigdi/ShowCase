using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[DefaultExecutionOrder(300)]
public class UIManager : MonoBehaviour
{
    public Text txtMatchCount;
    public InputField inputMatrisCount;

    public void UpdateScoreText(int score)
    {
        txtMatchCount.text = "Match Count : " + score.ToString();
    }
}
