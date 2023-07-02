using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PortalEffect : MonoBehaviour
{
    public UnityEvent onPortalOpenStart, onPortalOpenEnd;
    public float openTime = 3;
    public Material portalMat;
    bool isOpen = false;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void OpenPortal()
    {
        if (isOpen) { return; }

        isOpen = true;
        gameObject.SetActive(true);
        StartCoroutine(DoPortal());
    }

    public IEnumerator DoPortal()
    {
        onPortalOpenStart?.Invoke();

        float timer = 0;

        while (timer < openTime)
        {
            portalMat.SetFloat("_Cutoff", Mathf.Lerp(0.2f, 0.005f, timer / openTime));
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        onPortalOpenEnd?.Invoke();
    }

}
