using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InspectSystem : MonoBehaviour
{
    public GameObject _mapInspection, _mapState, _mapContent, _mapTools, _mapMore;
    public GameObject _mapTexts;

    private void Start() {
        _mapContent.SetActive(false);
        _mapState.SetActive(false);
        _mapTools.SetActive(false);
        _mapMore.SetActive(false);
        _mapInspection.SetActive(false);
    }

    public void InspectMap(Capital _map){
        _mapInspection.gameObject.SetActive(!_mapInspection.activeInHierarchy);
        //_mapTexts.transform.GetChild(0).GetComponent<TMP_Text>().text = $"Name: {_map._capitalName}";
        //_mapTexts.transform.GetChild(1).GetComponent<TMP_Text>().text = $"Rarity: {_map._capitalRarity}";
        //_mapTexts.transform.GetChild(2).GetComponent<TMP_Text>().text = $"Prestige: {_map._capitalPrestige}";
    }

    public void Specific(string Type){
        switch(Type){
            case "State":
                _mapState.SetActive(!_mapState.activeInHierarchy);
                return;
            case "Content":
                _mapContent.SetActive(!_mapContent.activeInHierarchy);
                return;
            case "Tools":
                _mapTools.SetActive(!_mapTools.activeInHierarchy);
                return;
            case "More":
                _mapMore.SetActive(!_mapMore.activeInHierarchy);
                return;
        }
    }
}
