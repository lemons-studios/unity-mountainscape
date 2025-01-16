using UnityEngine;
using UnityEngine.InputSystem;

public class UserUIManager : MonoBehaviour
{
    private UIInput uiInput;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        uiInput = new UIInput();
        uiInput.UI.CloseActiveMenu.performed += CloseActiveMenu;
        
        uiInput.Enable();
    }

    private void CloseActiveMenu(InputAction.CallbackContext obj)
    {
        GetActiveSubmenus()[0].SetActive(false);
    }

    public void ToggleUI(GameObject ui)
    {
        // Disable any active menus before the new ui gets toggled
        var disabledSubmenu = GetActiveSubmenus()[0];
        if (disabledSubmenu != ui)
        {
            disabledSubmenu.SetActive(false);
        }
        
        ui.SetActive(!ui.activeSelf);
    }

    public void QuitGame()
    {
        Cursor.lockState = CursorLockMode.None;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    
    private GameObject[] GetActiveSubmenus()
    {
        return GameObject.FindGameObjectsWithTag("UIElement");
    }
    
    private void OnDestroy()
    {
        uiInput.Dispose();
    }
}
