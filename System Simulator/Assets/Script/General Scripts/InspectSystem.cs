using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InspectSystem : MonoBehaviour
{
    public GameObject _mapInspection, _mapMore;
    public GameObject _mapTexts;

    private void Start() {
        _mapMore.SetActive(false);
        _mapInspection.SetActive(false);
    }

    private void Update() {
        if(Input.GetKey(KeyCode.Mouse1) && _mapInspection.activeInHierarchy == true){
            DisableMap();
        }
    }

    public void InspectMap(Capital _map){
        _mapInspection.gameObject.SetActive(!_mapInspection.activeInHierarchy);
        _mapTexts.transform.GetChild(0).GetComponent<TMP_Text>().text = $"Name: {_map._capitalName}";
        _mapTexts.transform.GetChild(1).GetComponent<TMP_Text>().text = $"Rarity: {_map._capitalRarity}";
        //_mapTexts.transform.GetChild(2).GetComponent<TMP_Text>().text = $"Prestige: {_map._capitalPrestige}";
    }

    public void DisableMap(){
        _mapInspection.gameObject.SetActive(false);
        _mapMore.SetActive(false);
    }

    public void Specific(string Type){
        switch(Type){
            case "More":
                _mapMore.SetActive(!_mapMore.activeInHierarchy);
                return;
        }
    }
}
