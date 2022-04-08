using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    public GameObject soundManager;

    private AudioSource[] bgMusic;
    private AudioSource music;

    private GameObject image;
    private float a;
    private bool startAnim = false;

    private bool gamePaused;

    
    // Start is called before the first frame update
    void Start()
    {
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)) {
            gamePaused = !gamePaused;
            if (gamePaused){
                Cursor.lockState = CursorLockMode.None;
            }else{
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void StartGame(){
        GameObject.Find("MainMenu").SetActive(false);
        GameObject.Find("MainMenuCam").SetActive(false);
        player.SetActive(true);
    }

    public void EndGame(){
        GoToScene(0);
    }

    public void GoToScene(int id){
        if(id == 0){
            Cursor.lockState = CursorLockMode.None;
        }
        SceneManager.LoadScene(id);
    }
}
