using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MapOverseer : MonoBehaviour
{
    public InspectSystem _inspectSystem;
    [Space]
    public GameObject _camera;
    public GameObject _worldParent;
    [Space]
    public GameObject _capitalNormal;
    public GameObject _capitalLarge;
    public GameObject _townSmall;
    public GameObject _townLarge;
    [Space]
    public float _CapitalDis = 30;
    public int _maxCapital = 100;
    [Space]
    public int _maxTownPerCapital = 8;



    

    public void CreateSingularCapital(){
        GameObject Capital = Instantiate(_capitalNormal);

        Capital.transform.parent = _worldParent.transform;
        Capital.AddComponent<Capital>();
        Capital.GetComponent<Capital>()._inspectSystem = _inspectSystem;

        Capital.transform.rotation = Quaternion.Euler(0.0f, 0.0f , Random.Range(0.0f, 360)); // Randomized Rotation
        Capital.transform.position = GetPosition(Capital);
    }

    public bool GetChance(float chance, float total){
        float _generatedNum = Random.Range(0.000f, total);
        if(_generatedNum <= chance){
            return true;
        }

        return false;
    }

    public Vector2 GetPosition(GameObject _capital){
        float xPos = Random.Range(transform.GetChild(0).transform.position.x, transform.GetChild(1).transform.position.x);
        float yPos = Random.Range(transform.GetChild(0).transform.position.y, transform.GetChild(3).transform.position.y);

        return new Vector2(xPos, yPos);
    }

}
