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
        if (soundManager != null){
            bgMusic = soundManager.GetComponents<AudioSource>();
            foreach (AudioSource audio in bgMusic){
                if (audio.clip.name == "music_light"){
                    music = audio;
                }
            }
        }
    }

    void Update(){
        if (startAnim){
            if (image != null){
                a += 0.4f * Time.deltaTime;
                a = Mathf.Clamp(a, 0f, 1f);
                image.GetComponent<Image>().color = new Color(1f, 1f, 1f, a);

                if (a == 1f && !music.isPlaying){
                    GetComponent<Timer>().StartTimer();
                    startAnim = false;
                }
            }
        }
    }

    public void EndGame(){
        if (canvas != null){
            startAnim = true;
        }
    }

    public void GoToScene(int id){
        if(id == 0){
            Cursor.lockState = CursorLockMode.None;
        }
        SceneManager.LoadScene(id);
    }
}
