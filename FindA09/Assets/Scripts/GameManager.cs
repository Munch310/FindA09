using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance()
    {
        if(!instance)
        {
            instance = FindObjectOfType(typeof(GameManager)) as GameManager;

            if(instance == null)
                Debug.Log("No Singleton Objs");
            
        }
        return instance;
    }

    private void Awake() 
    {
        if(!instance)
            instance = this;

        if(instance != this)
            Destroy(gameObject);
    }
}
