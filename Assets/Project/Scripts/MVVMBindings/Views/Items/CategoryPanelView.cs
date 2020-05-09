using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CategoryPanelViewModel))]
public class CategoryPanelView : BaseView<CategoryPanelViewModel>
{
    protected override bool SetAndStretchToParent => false;
}
