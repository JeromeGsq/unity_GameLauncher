using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeColorAssigner : MonoBehaviour
{
    [SerializeField]
    private Image image;

    private void Awake()
    {
        if (this.image == null)
        {
            this.image = this.GetComponent<Image>();
        }

        if (this.image != null)
        {
            this.image.color = App.MainColor;
        }
    }
}
