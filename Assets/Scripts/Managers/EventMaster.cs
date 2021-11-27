using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(200)]
public class EventMaster : MonoBehaviour
{
    public delegate void Reset();
    public event Reset reseted;

    public void CallReseted()
    {
        if (reseted != null)
        {
            reseted();
        }
    }
}
