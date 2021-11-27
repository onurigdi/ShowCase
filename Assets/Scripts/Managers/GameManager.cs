using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(100)]
public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public GameSettings Settings;
    Box[,] GeneratedBoxMatris = new Box[1, 1];

    int _ScoreCount;
    int ScoreCount
    {
        get
        {
            return _ScoreCount;
        }
        set
        {
            _ScoreCount = value;
            UIManager.UpdateScoreText(ScoreCount);
        }
    }



    [Header("Managers")]
    public UIManager UIManager;
    public EventMaster EventManager;
    

    private void Awake()
    {
        if (!instance)
            instance = this;
    }


    private void Start()
    {
        GenerateNewGame();
    }



    int GetNValue()
    {
        int n = 0;
        if (!string.IsNullOrEmpty(UIManager.inputMatrisCount.text))
        {
            n = Mathf.Abs(int.Parse(UIManager.inputMatrisCount.text));
        }
        return n;
    }

    float GetDefaultBoxWidth()
    {
        return Settings.BoxWidth;
    }

    float GetDefaultHalfBoxWidth()
    {
        return Settings.BoxWidth / 2;
    }

    float GetGameBoardMaxX()
    {
        return AlignedCamera.instance.getGameBoardSize().x;
    }


    float GetGameBoardMaxY()
    {
        return AlignedCamera.instance.getGameBoardSize().y;
    }

    /*
    void DisableAllBoxes()
    {
        Box[] Boxes = (Box[])GameObject.FindObjectsOfType(typeof(Box));
        foreach (Box box in Boxes)
            box.Disable();
    }
    */

    private void Reset()
    {
        EventManager.CallReseted();
        ScoreCount = 0;        
        AlignedCamera.instance.ReCalculate();
        int n = GetNValue();
        GeneratedBoxMatris = new Box[n, n];
    }

    public void GenerateNewGame()
    {
        Reset();
        
        int n = GetNValue();
        for (int y = 0; y < n; y++)
        {
            for (int x = 0; x < n; x++)
            {
                Vector3 initialPos = Vector3.zero;
                Box newBox = ObjectPooler.instance.GetPooledObject("Box", initialPos, Quaternion.identity).GetComponent<Box>();
                newBox.MatrisAdress = new Vector2(x,y);
                GeneratedBoxMatris[x, y] = newBox;
                float newBoxWidth = ((GetGameBoardMaxX() / (float)n));
                float scale = newBoxWidth / GetDefaultBoxWidth();
                Vector3 newScale = new Vector3(scale, scale, 1);
                initialPos = new Vector3((newBoxWidth / 2) + (newBoxWidth * x), (GetGameBoardMaxY() - (newBoxWidth / 2)) - (y*newBoxWidth), 0);
                
                newBox.SetPosition(initialPos);
                newBox.SetScale(newScale);
            }
        }
    }


    public void CheckNearByBoxes(Box mainBox)
    {
        List<Box> DetectedBoxes = new List<Box>();        
        DetectedBoxes.Add(mainBox);
        int CrossedNearByCount = 0;
        int mixXCheck = Mathf.Clamp((int)mainBox.MatrisAdress.x - 1,0, GetNValue() -1);
        int maxXCheck = Mathf.Clamp((int)mainBox.MatrisAdress.x + 1,0, GetNValue() - 1);
        int minYCheck = Mathf.Clamp((int)mainBox.MatrisAdress.y - 1,0, GetNValue() - 1);
        int maxYCheck = Mathf.Clamp((int)mainBox.MatrisAdress.y + 1,0, GetNValue() - 1);

        for (int x = mixXCheck; x <= maxXCheck; x++)
        {
            for (int y = minYCheck; y <= maxYCheck; y++)
            {
                if (x == (int)mainBox.MatrisAdress.x && y == (int)mainBox.MatrisAdress.y)
                    continue; // this is My Adress (mainBox)

                if (GeneratedBoxMatris[x, y].isCrossed())
                {
                    CrossedNearByCount++;
                    DetectedBoxes.Add(GeneratedBoxMatris[x, y]);
                }
            }
        }

        if (CrossedNearByCount >= 3)
        {
            ScoreCount++;
            foreach(Box box in DetectedBoxes)
            {
                box.ActivateCross(false);
            }
        }
            
    }

}
