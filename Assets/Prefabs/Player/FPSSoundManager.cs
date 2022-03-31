using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSSoundManager : MonoBehaviour
{
    public float minVolume = 0.3f;
    public float maxVolume = 0.5f;
    public AudioClip[] walkSounds;
    public AudioClip[] runSounds;
    public AudioClip jumpSound;

    public enum MoveState {NONE, WALK, RUN};

    public MoveState state;

    private float moveInterval = 1f;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (state != MoveState.NONE){
            timer -= Time.deltaTime;
            if (timer <= 0f){
                PlayMoveSound(0);
                timer = moveInterval;
            }
        }else{
            timer = moveInterval / 2f;
        }
    }

    public void SetMoveInterval(float val){
        if (moveInterval != val){
            moveInterval = val;
            timer = moveInterval / 2f;
        }
    }

    public void ChangeState(int id){
        switch(id){
            case 0:
                state = MoveState.NONE;
                break;
            case 1:
                state = MoveState.WALK;
                break;
            case 2:
                state = MoveState.RUN;
                break;
        }
    }

    public void RandomizePitch(float min, float max){
        float pitch = Random.Range(min, max);
        GetComponent<AudioSource>().pitch = pitch;
    }

    public void PlayMoveSound(int id){
        float volume = Random.Range(minVolume, maxVolume);
        if (id == 0){
            int index = Random.Range(0, walkSounds.Length);
            RandomizePitch(0.8f, 1.2f);
            GetComponent<AudioSource>().PlayOneShot(walkSounds[index], volume);
        }
    }

    public void PlayJumpSound(float volume){
        RandomizePitch(0.95f, 1.3f);
        GetComponent<AudioSource>().PlayOneShot(jumpSound, Mathf.Clamp(volume, 0f, 1f));
    }
}
