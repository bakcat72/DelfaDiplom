using UnityEngine;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour
{
    [Header("Настройки")]
    public GameObject settingsPanel;  // Панель с настройками
    public bool startClosed = true;    // Начинать с закрытой панелью
    
    private Button button;
    private bool isOpen = false;
    
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ToggleSettings);
        
        if (settingsPanel != null && startClosed)
        {
            settingsPanel.SetActive(false);
            isOpen = false;
        }
    }
    
    public void ToggleSettings()
    {
        if (settingsPanel != null)
        {
            isOpen = !isOpen;
            settingsPanel.SetActive(isOpen);
        }
    }
    
    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            isOpen = true;
            settingsPanel.SetActive(true);
        }
    }
    
    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            isOpen = false;
            settingsPanel.SetActive(false);
        }
    }
}