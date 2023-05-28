using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowsUI : MonoBehaviour
{
    public RectTransform left, right, up, down;
    RectTransform selectedRect;
    List<RectTransform> rects;
    private void Start()
    {
        rects = new List<RectTransform> { left, right, up, down };
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            selectedRect = left;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            selectedRect = right;
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            selectedRect = down;
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            selectedRect = up;
        }
        else
        {
            selectedRect = null;
        }

        foreach (RectTransform rect in rects)
        {
            CanvasGroup cg = rect.GetComponent<CanvasGroup>();
            if (selectedRect == rect)
            {
                rect.localScale = Vector3.Lerp(rect.localScale, Vector3.one * 1.1f, Time.deltaTime * 7);
                cg.alpha = Mathf.Lerp(cg.alpha, 1, Time.deltaTime * 7);
            }
            else
            {
                rect.localScale = Vector3.Lerp(rect.localScale, Vector3.one, Time.deltaTime * 7);
                cg.alpha = Mathf.Lerp(cg.alpha, 0.8f, Time.deltaTime * 7);
            }
        }
    }
}
