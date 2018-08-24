using UnityEngine;
using System.Collections;

public class DmgTextController : MonoBehaviour
{
    private static DmgText popupText;
    private static GameObject canvas;

    public static void Initialize()
    {
        canvas = GameObject.Find("Canvas");
        if(!popupText)
            popupText = Resources.Load<DmgText>("PopOpTextParent");
    }

    public static void CreateFloatingText(string text, Transform location)
    {
        DmgText instance = Instantiate(popupText);
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(location.position);
        
        instance.transform.SetParent(canvas.transform);
        instance.transform.position = screenPosition;
        instance.SetText(text);
    }
}