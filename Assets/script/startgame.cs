using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public string levelName = "Level1"; // Имя вашего уровня
    
    private Button button;
    
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LoadLevel);
    }
    
    void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}