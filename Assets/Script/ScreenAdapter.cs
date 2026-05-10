using UnityEngine;
using UnityEngine.UI;

public class ScreenAdapter : MonoBehaviour
{
    public float widthRatio = 16f / 9f;
    public float heightRatio = 9f / 16f;

    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        AdaptScreen();
    }

    void AdaptScreen()
    {
        float screenRatio = (float)Screen.width / Screen.height;
        if (screenRatio > widthRatio)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.y * screenRatio, rectTransform.sizeDelta.y);
        }
        else if (screenRatio < heightRatio)
        {
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.x / screenRatio);
        }
    }
}