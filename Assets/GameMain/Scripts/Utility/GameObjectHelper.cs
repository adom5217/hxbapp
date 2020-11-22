using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameObjectHelper
{
    public static void SetText(this GameObject go, string msg)
    {
        var Txt =go.GetComponentInChildren<Text>();
        if(Txt)
            Txt.text = msg;

    }

}
