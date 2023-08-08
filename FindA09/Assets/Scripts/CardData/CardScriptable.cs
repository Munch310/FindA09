using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardScriptable", menuName = "FindA09/CardScriptable", order = 0)]
public class CardScriptable : ScriptableObject 
{

    public int      _index;
    public string   _cardName;
    public string   _desc;
    public Sprite   _sprite;

}
