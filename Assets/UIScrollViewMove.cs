using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class UIScrollViewMove : MonoBehaviour
{
    private ScrollRect scrollRect;

    [SerializeField]
    private float speed = 2;

    private void Awake()
    {
        this.scrollRect = this.GetComponent<ScrollRect>();
    }

    private void Update()
    {
        if (EventSystem.current?.currentSelectedGameObject?.transform?.GetSiblingIndex() != null)
        {
            float normalizePosition =
                (float)EventSystem.current.currentSelectedGameObject.transform.GetSiblingIndex() /
             ((float)this.scrollRect.content.transform.childCount - 1);

            this.scrollRect.horizontalNormalizedPosition =
                Mathf.Lerp(
                    this.scrollRect.horizontalNormalizedPosition,
                    normalizePosition,
                    Time.deltaTime * this.speed);
        }
    }
}
