using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSystem : MonoBehaviour
{
    public string characterName;
    public string location;
    [Space]
    public int _health, _maxHealth;
    [Space]
    public float _exp, exp_cap = 50;
    public int characterLevel = 1;
    [Space]
    public float defense;
    public float damage;

    #region Memory
    public string _lastSavedLocation;
    #endregion

    public void Update() {
        if(_exp >= exp_cap){
            characterLevel++;
            exp_cap += 50;
            _exp = 0;
        }
    }

    public void Died(){
        _exp = 0;
        location = _lastSavedLocation; // "Teleports" the player. 
    }
}
