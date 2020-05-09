using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class UIScrollViewMove : MonoBehaviour
{
    private ScrollRect scrollRect;

    private float normalizePosition;

    [SerializeField]
    private float speed = 2;

    private void Awake()
    {
        this.scrollRect = this.GetComponent<ScrollRect>();
    }

    private void Update()
    {
        try
        {
            var selected = EventSystem.current?.currentSelectedGameObject?.transform;
            if (selected != null && selected.parent == this.scrollRect.content.transform)
            {
                var index = selected.GetSiblingIndex();
                this.normalizePosition = (float)index / ((float)this.scrollRect.content.transform.childCount - 1);

                this.scrollRect.horizontalNormalizedPosition =
                        Mathf.Lerp(
                            this.scrollRect.horizontalNormalizedPosition,
                            normalizePosition,
                            Time.deltaTime * this.speed
                        );
            }

        }
        catch (System.Exception e)
        {
            Debug.LogWarning("UIScrollViewMove : Update() : Skip normalizePosition calculation. Is EventSystem.current?.currentSelectedGameObject null ?");
        }
    }
}
