using DG.Tweening;
using InControl;
using System.Collections.Generic;
using System.ComponentModel;
using Toastapp.MVVM;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameItemPanelViewModel))]
public class GameItemPanelView : BaseView<GameItemPanelViewModel>
{
    protected new bool SetAndStretchToParent => false;

    protected override void OnEnable()
    {

    }
}
