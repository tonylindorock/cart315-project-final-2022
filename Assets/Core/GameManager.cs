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

    
    // Start is called before the first frame update
    void Start()
    {
        image = GameObject.Find("Canvas/Bg");
    }

    void Update(){
        
    }

    public void EndGame(){
        
    }

    public void GoToScene(int id){
        if(id == 0){
            Cursor.lockState = CursorLockMode.None;
        }
        SceneManager.LoadScene(id);
    }
}
