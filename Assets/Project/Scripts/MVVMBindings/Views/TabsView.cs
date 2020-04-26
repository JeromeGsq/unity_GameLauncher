using Toastapp.MVVM;
using UnityEngine;

[RequireComponent(typeof(UISelectableManager))]
public class TabsView<T> : BaseView<T> where T : UnityViewModel
{
    [SerializeField]
    private UISelectableManager uiSelectableManager;

    protected UISelectableManager UISelectableManager => this.uiSelectableManager;
}
