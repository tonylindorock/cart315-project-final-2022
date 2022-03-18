using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEvent : MonoBehaviour
{
    public string func;
    public string val;
    public enum Type {NONE, INT, FLOAT, BOOL, STRING};
    public Type valType;

    public void StartEvent(){
        if (valType == Type.NONE){
            SendMessage(func);
        }else{
            int i = 0;
            float f = 0f;
            bool b = false;

            bool result = true;
            switch(valType){
                case Type.INT:
                    result = int.TryParse(val, out i);
                    break;
                case Type.FLOAT:
                    result = float.TryParse(val, out f);
                    break;
                case Type.BOOL:
                    result = bool.TryParse(val, out b);
                    break;
            }
            if (result){
                switch(valType){
                    case Type.INT:
                        SendMessage(func, i);
                        break;
                    case Type.FLOAT:
                        SendMessage(func, f);
                        break;
                    case Type.BOOL:
                        SendMessage(func, b);
                        break;
                    case Type.STRING:
                        SendMessage(func, val);
                        break;
                }
            }else{
                Debug.Log("ERROR: Type " + valType + " convert failed!");
            }
        }
    }
}