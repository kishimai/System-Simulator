using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;


public class Capital : MonoBehaviour
{
    public string _capitalName;
    public float _capitalPrestige; // Main Function to how Capitals are Identified.
    public string _capitalRarity; 
    [Space]
    public Dictionary<string, int> _villages = new Dictionary<string, int>(); // Randomly Give, Prestige will be based on Village & Capital Progress (Formula=total)
    public Dictionary<string, int> _capitalContent = new Dictionary<string, int>(); // Randomly assigned based on Rarity
    [Space]
    public bool _origin = false; // meaning the beginning of the generation or the most developed capital.
    [Space]
    public MapOverseer _manager;
    public List<GameObject> _nearCapitals = new List<GameObject>();
    public List<GameObject> _farCapitals = new List<GameObject>();

    public void Start() { //  Updates the Info

        #region  Collecting Collider Data
        Collider2D[] _nearColliders = Physics2D.OverlapCircleAll(this.transform.position, 50f);
        Collider2D[] _farColliders = Physics2D.OverlapCircleAll(this.transform.position, 150f);
        
        foreach(Collider2D _collider in _nearColliders){ // Removing closeCapitals lists content to far capital     
            if(_collider.gameObject != this.gameObject){
                _nearCapitals.Add(_collider.gameObject);
            }
        }

        foreach(Collider2D _collider in _farColliders){
            if(!_nearCapitals.Contains(_collider.gameObject) && _collider.gameObject != this.gameObject){
                _farCapitals.Add(_collider.gameObject);
            }
        }
        #endregion

        //Generating Capital Rarity
        if(GetChance(50)){
            _capitalRarity = "Normal";
            int _loc = Random.Range(2, 5);
            int _dun = Random.Range(1, 3);
        } else if(GetChance(30)){
            _capitalRarity = "Poor";
            int _loc = Random.Range(1, 3);
            int _dun = Random.Range(1, 3);
        } else if(GetChance(10)){
            _capitalRarity = "Rare";
            int _loc = Random.Range(3, 7);
            int _dun = Random.Range(4, 5);
        } else if(GetChance(6)){
            _capitalRarity = "Mythical";
            int _loc = 10;
            int _dun = 10;
        }
    }

    public void Hover(bool type){
        Debug.Log("Hover:" + type);
    }

    public bool GetChance(int num){ // A random generation of a number and checking if the percentile is higher out of 100
        int _randomNum = Random.Range(0, 100);
        if(_randomNum < num){
            return true;
        } else {
            return false;
        }
    }


}
