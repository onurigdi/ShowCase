using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class AlignedCamera : MonoBehaviour
{
    public static AlignedCamera instance;

    private void Awake()
    {
        if (!instance)
            instance = this;
    }

    protected Vector2 m_gameBoard;

    public void ReCalculate()
    {
        float vFov = Camera.main.fieldOfView / 2f;
        float cameraDistance = Mathf.Abs(transform.position.z);
        float lastAngle = 180f - 90f - vFov;

        float vSize = ((Mathf.Sin(vFov * Mathf.Deg2Rad) * cameraDistance) / Mathf.Sin(lastAngle * Mathf.Deg2Rad)) * 2f;


        float hSize = (Screen.width * vSize) / Screen.height;


        transform.position = new Vector3(hSize / 2f, vSize / 2f, transform.position.z);

        //Debug.Log("resolution(" + Screen.width + " x " + Screen.height + ") gameBoard(" + hSize + " x " + vSize + ")");
        m_gameBoard = new Vector2(hSize, vSize);
    }
    void Start()
    {

        ReCalculate();
    }

    public Vector2 getGameBoardSize()
    {
        return m_gameBoard;
    }

    

}