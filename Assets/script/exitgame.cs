using UnityEngine;
using UnityEngine.UI;

public class ExitGameButton : MonoBehaviour
{
    [Header("Настройки")]
    public bool showConfirmation = true;  // Показывать подтверждение выхода
    public GameObject confirmationPanel;   // Панель с предупреждением (опционально)
    
    private Button button;
    
    private void Start()
    {
        // Получаем компонент кнопки
        button = GetComponent<Button>();
        
        // Добавляем слушатель на нажатие
        button.onClick.AddListener(ExitGame);
    }
    
    public void ExitGame()
    {
        if (showConfirmation && confirmationPanel != null)
        {
            // Показываем панель подтверждения
            confirmationPanel.SetActive(true);
        }
        else
        {
            // Выходим сразу
            PerformExit();
        }
    }
    
    public void PerformExit()
    {
        #if UNITY_EDITOR
            // В редакторе Unity просто останавливаем игру
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // В собранной игре - закрываем приложение
            Application.Quit();
        #endif
    }
    
    // Метод для кнопки "Нет" на панели подтверждения
    public void CancelExit()
    {
        if (confirmationPanel != null)
            confirmationPanel.SetActive(false);
    }
}