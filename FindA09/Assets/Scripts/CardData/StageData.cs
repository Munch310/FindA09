using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "FindA09/StageData", order = 0)]
public class StageData : ScriptableObject 
{

    [System.Serializable]
    public struct STAGE_DATA
    {
        public int      cardNumber;
        public float    time;
    }

    [SerializeField] public STAGE_DATA[] array = null;

    public int maxStage { get { return array.Length - 1; } }

}
