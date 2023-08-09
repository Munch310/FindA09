using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global
{

    //싱글턴 입니다.
    private static Global I = new Global();
    private Global() { }
    public static Global Instance { get { return I; } }




    private int currentStage = 0;

    public int CurrentStage { set { currentStage = value; } get { return currentStage; } }

}
