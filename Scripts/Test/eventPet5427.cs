using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventPet5427 : MonoBehaviour
{
    public testSpine eventMgr;

    void hurt()
    {
        if (eventMgr != null)
        {
            eventMgr.ResetParameters();
        }
    }
}
