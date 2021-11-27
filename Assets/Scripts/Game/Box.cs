using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using UnityEngine;
[DefaultExecutionOrder(300)]
public class Box : MonoBehaviour, IPointerDownHandler
{
    Transform refTransform;
    public Vector2 MatrisAdress;
    public GameObject Cross;


    

    private void OnEnable()
    {
        ActivateCross(false);
        GameManager.instance.EventManager.reseted += GameReseted;
    }


    private void OnDisable()
    {
        GameManager.instance.EventManager.reseted -= GameReseted;
    }


    void GameReseted()
    {
        gameObject.SetActive(false);
    }

    public bool isCrossed()
    {
        return Cross.activeSelf;
    }

    private void Awake()
    {
        refTransform = transform;
    }


    public Transform GetTransform()
    {
        return refTransform;
    }

    public void SetScale(Vector3 scale)
    {
        transform.localScale = scale;
    }


    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }


    public void ActivateCross(bool val)
    {
        Cross.SetActive(val);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ActivateCross(true);
        GameManager.instance.CheckNearByBoxes(this);
    }


   
}
