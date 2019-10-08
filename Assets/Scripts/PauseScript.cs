using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [HideInInspector]
    public static bool paused = false;
    [HideInInspector]
    public bool pauseTime = false;
    public GameObject pauseMenu;

    public AudioSource bgMusic;
    private float initVolLevel;

    // Start is called before the first frame update
    void Start()
    {
        initVolLevel = bgMusic.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Pause"))
        {
           
            if(paused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if(Input.GetButtonDown("Action"))
        {
            pauseTime = false;
        }
        /*
        if(pauseTime)
        {
            Time.timeScale = 0.05f;
            
        }
        else
        {
            Time.timeScale = 1.0f;
            
        }
        */
    }

    public void New_game()
    {
        Resume();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        bgMusic.volume = 0.05f;
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Time.timeScale = 0.0f;
        paused = true;
    }

    public void Resume()
    {
        bgMusic.volume = initVolLevel;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1.0f;
        paused = false;
    }

    public void MainMenu()
    {
        Resume();
        SceneManager.LoadScene("StartMenu");
    }

    
    
}

