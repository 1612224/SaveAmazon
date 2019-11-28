using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupController : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Text text;

    [Range(0,1)]
    public float showSpeed;
    [Range(0,1)]
    public float hideSpeed;
    private int showHide;

    public void SetMessage(string msg)
    {
        text.text = msg;
    }

    private void Start()
    {
        canvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = showHide > 0 ? showSpeed : hideSpeed;
        canvasGroup.alpha += speed * showHide * Time.deltaTime;
        canvasGroup.alpha = Mathf.Clamp01(canvasGroup.alpha);
        if (canvasGroup.alpha > 0.99f)
        {
            showHide = -1;
        }
    }

    public void Show()
    {
        showHide = 1;
    }
}
