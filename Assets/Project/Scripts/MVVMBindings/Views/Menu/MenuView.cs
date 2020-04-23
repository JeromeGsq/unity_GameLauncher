using UnityEngine;

[RequireComponent(typeof(MenuViewModel))]
public class MenuView : BaseView<MenuViewModel>
{
    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Close menu");
            this.ViewModel.CloseViewModel();
        }
    }
}
