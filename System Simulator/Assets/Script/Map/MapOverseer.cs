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
    public int total_road;
    public int total_capital;
    public int total_towns;
    [Space]
    public GameObject _road;
    public GameObject _capitalNormal;
    public GameObject _capitalLarge;
    public GameObject _townSmall;
    public GameObject _townLarge;
    [Space]
    public float _CapitalDis = 30;
    public int _maxCapital = 100;
    [Space]
    public int _maxTownPerCapital = 8;

    [BoxGroup("Line Generation")]
    public LineRenderer _lineRenderer, _pathRenderer;

    [BoxGroup("Generation Values")]
    public int _worldSize;
    [BoxGroup("Generation Values")]
    public Vector2 _upperWorldSize, _lowerWorldSize; // gets the value in between for a randomized value in the area
    [BoxGroup("Generation Values")]
    public GameObject CapitalOrigin;
    [BoxGroup("Generation Values")]
    public int _minRoadInterval, _maxRoadInterval; // Capital Intervals
    [BoxGroup("Generation Values")]
    public float _villageChance; // Chances out of 100



    public void Start(){ // Starts the generation
        _worldSize = Random.Range(600, 1000);
        _upperWorldSize = new Vector2(_worldSize, 0); // Upper center
        _lowerWorldSize = new Vector2(-_worldSize, 0); // Lower center

        // MAP LINES INDICATOR
        transform.GetChild(0).transform.position = new Vector2(_lowerWorldSize.x, +_worldSize);
        transform.GetChild(1).transform.position = new Vector2(_upperWorldSize.x, +_worldSize);
        transform.GetChild(2).transform.position = new Vector2(_upperWorldSize.x, -_worldSize);
        transform.GetChild(3).transform.position = new Vector2(_lowerWorldSize.x, -_worldSize);

        WorldLineSetUp();
        CreateCapitals();
    }

    Vector2 _upperLeft, _lowerRight;
    public void CreateCapitals(){
        if(CapitalOrigin != null){
            int _gen = Random.Range(25, _maxCapital);
            Debug.Log($"{_gen} Capital Total");
            for (int i = 0; i < _gen; i++){
                GameObject Capital = Instantiate(_capitalNormal);

                Capital.transform.parent = _worldParent.transform;
                Capital.AddComponent<Capital>()._inspectSystem = _inspectSystem;

                Capital.transform.rotation = Quaternion.Euler(0.0f, 0.0f , Random.Range(0.0f, 360)); // Randomized Rotation

                Vector3 position = GetPosition(Capital);
                position = position + Random.onUnitSphere * _CapitalDis;
                Capital.transform.position = position;
            }

        } else{
            GameObject originCapital = Instantiate(_capitalLarge);

            originCapital.AddComponent<Capital>()._origin = true;
            originCapital.GetComponent<Capital>()._inspectSystem = _inspectSystem;

            originCapital.transform.position = GetPosition(originCapital);
            _camera.transform.position = new Vector3(originCapital.transform.position.x, originCapital.transform.position.y, -10);

            CapitalOrigin = originCapital;
            CapitalOrigin.transform.parent = _worldParent.transform;

            CreateCapitals();
        }
    }

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

    public void WorldLineSetUp(){
        _lineRenderer.positionCount = this.transform.childCount;
        for (int i = 0; i < this.transform.childCount; i++){
            _lineRenderer.SetPosition(i, transform.GetChild(i).position);
        }
    }


}
