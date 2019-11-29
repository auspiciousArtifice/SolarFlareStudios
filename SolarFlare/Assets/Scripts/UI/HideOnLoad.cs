using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CanvasGroup canvasGroup = gameObject.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            Debug.LogWarning("Missing Pause Menu Canvas Group reference");
            return;
        }
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0f;
    }
}
