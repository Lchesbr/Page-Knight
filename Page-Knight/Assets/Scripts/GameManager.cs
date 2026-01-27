using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public Button titleScreenButton;
    public Button startButton;
    public TextMeshProUGUI title;
    public GameObject gameArea;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayGame()
    {
        gameArea.gameObject.SetActive(true); 
        title.gameObject.SetActive(false);
        startButton.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        titleScreenButton.gameObject.SetActive(true);
    }


    public void BackToTitle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
