using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;

public class Core : MonoBehaviour
{
    [BoxGroup("Main Information")]
    public string _name, _location;
    [BoxGroup("Character Moodlet")]
    public float _content, _malnutrition, _fatigue; // Three Main Moodlets being Content, Hunger and Tirdenss

    void Start(){
        
    }
}
