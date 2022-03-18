using System;
using UnityEngine;

public class Plot : MonoBehaviour
{
    private int id = 0;
    private bool finished;

    public enum Type {NONE, INT, FLOAT, BOOL, STRING};

    [Serializable]
    public struct Node {
        public string text;
        public string func;
        public Type valType;
        public string val;
    }
    public Node[] newThoughts;

    public string[] thoughts;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsFinished(){
        return finished;
    }

    public string GetCurrentText(){
        return thoughts[id].Replace("\\n", "\n");
    }

    public string Advance(){
        if (id < thoughts.Length - 1){
            id += 1;
        }else{
            finished = true;
        }
        return thoughts[id].Replace("\\n", "\n");
    }
}
