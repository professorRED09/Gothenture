using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IObserver
{
    [Header("Subjects")]
    [SerializeField] Subject playerHealth;
    [SerializeField] Subject playerInteract;
    [SerializeField] AudioClip themeSound;
    [SerializeField] AudioClip gameOverSound;
    AudioSource audioSource;

    [Header("Ref")]
    [SerializeField] GameObject gameOverPopup;
    public LevelLoader level;

    public static GameManager instance;

    //[SerializeField] GameObject inventory;
    //[SerializeField] bool isToggleOn;
    //[SerializeField] GameObject pauseMenu;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        // disable the game over window at the beginning.
        if (gameOverPopup != null){
            gameOverPopup.SetActive(false);            
        }
        audioSource.clip = themeSound;
        audioSource.Play();
       
    }

    // do sth when got notified from subjects
    public void OnNotify(PlayerAction action)   
    {
        switch (action)
        {
            case (PlayerAction.Pause):
                PauseGame();
                return;

            case (PlayerAction.Resume):
                ResumeGame();
                return;

            case (PlayerAction.Dead):   
                print("Showing GAME OVER");
                audioSource.clip = gameOverSound;
                audioSource.Play();
                StartCoroutine(ShowGameOver());
                return;           

            default:
                return;
        }
    }  

    public void ResumeGame()    // to assign on Resume Button
    {
        Time.timeScale = 1f;
    }

    public void PauseGame()     // to assign on Pause Button
    {
        Time.timeScale = 0;
    }

    public void Restart()       // to assign on Restart Button
    {
        SceneManager.LoadScene("Gameplay");
        level.LoadNextScene();
    }

    public void BackToTitle()   // to assign on Back to Title Button  
    {
        SceneManager.LoadScene("Title");
        level.LoadNextScene();
    }

    //public void ShowPauseMenu()
    //{
    //    pauseMenu.SetActive(true);
    //    PauseGame();
    //}

    IEnumerator ShowGameOver()  // delay amount of time before showing game over window
    {
        yield return new WaitForSeconds(2f);
        gameOverPopup.SetActive(true);
    }

    void OnEnable()
    {
        if(playerHealth != null)
        {
            playerHealth.AddObserver(this);
            playerInteract.AddObserver(this);
        }        
    }

    void OnDisable()
    {
        if (playerHealth != null)
        {
            playerHealth.RemoveObserver(this);
            playerInteract.RemoveObserver(this);
        }        
    }


}
