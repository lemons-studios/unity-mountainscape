using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
        
    }

    public void ToggleUI(GameObject ui)
    {
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

    public void JoinGameHelper()
    {
        Task.Run(JoinGame);
    }
    
    private async Task JoinGame()
    {
        Debug.Log("Triggered");
        var joinCode = GameObject.FindGameObjectWithTag("JoinMenu").GetComponentInChildren<TMP_InputField>().text;
        await GameServicesManager.StartClientConnection(joinCode);
    }

    public void StartHostingHelper()
    {
        Task.Run(StartHosting);
    }
    
    private async Task StartHosting()
    {
        PlayerPrefs.SetString("currentJoinKey", await GameServicesManager.StartHostRelay());
        SceneManager.LoadScene("Scenes/TheMountain/Main", LoadSceneMode.Single);
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
