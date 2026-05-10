using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachSafeArea : MonoBehaviour
{
    public Canvas canvas;

    private void Start()
    {
        var safeArea =Screen.safeArea;
        var canvasRect = canvas.GetComponent<RectTransform>(); 
        var screenScale = canvasRect.rect.width/Screen.width;
        var max = Mathf.Max(safeArea.x, Screen.width - (safeArea.x + safeArea.width)) * screenScale;

        var rect = this.GetComponent<RectTransform>();
        rect.offsetMin=new Vector2(max,rect.offsetMin.y);
        rect.offsetMax=new Vector2(-max,rect.offsetMax.y);

    }
}
