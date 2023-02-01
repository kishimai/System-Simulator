using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WindowSystem : MonoBehaviour{
    public GameObject _informationWindow_Short;
    public GameObject _informationWindow_Long;
    [Space]
    public GameObject _windowParent;
    public Canvas _mainCanva; // Used as main Canva
    [Space]
    public GameManager _manager; // used to attach

    private List<GameObject> _windows = new List<GameObject>();

    private void Start() {
        req_window("short_", "Welcome To Stats.", true);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Space)){
            req_window("short_", "Debuging", true);
        }
    }

    public void req_window(string type, string value, bool randomized){
        Vector3 _randomVector3 = RandomVector3(new Vector3(1000, 600, 0), new Vector3(1200, 800, 0));
        if(type=="short_"){
            
            GameObject _quickInfo = Instantiate(_informationWindow_Short, _randomVector3, Quaternion.identity);

            // ASSIGNING
            _quickInfo.GetComponent<DragWindow>().canvas = _mainCanva;
            _quickInfo.transform.GetChild(0).GetComponent<TMP_Text>().text = value;
            _quickInfo.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);

            // FINALIZING
            _quickInfo.transform.parent = _windowParent.transform;
            _windows.Add(_quickInfo);
            req_cleanse("full"); // Clears the entire Window: for now.

        } else if(type=="long_"){
            GameObject _longInfo = Instantiate(_informationWindow_Long, _randomVector3, Quaternion.identity);
            _longInfo.transform.GetChild(0).GetComponent<TMP_Text>().text = value;
            _longInfo.transform.parent = _windowParent.transform;
            _windows.Add(_longInfo);
        }
    }

    public void req_cleanse(string type){
        if(type == "full"){ // Clears whole active windows and/or Minimizes Windows
            foreach(GameObject _window in _windows){
                Destroy(_window, 15);
            }
        }
    }

    public Vector3 RandomVector3(Vector3 Vec1, Vector3 Vec2){
        return new Vector3(Random.Range(Vec1.x, Vec2.x), Random.Range(Vec1.y, Vec2.y), Random.Range(Vec1.z, Vec2.z));
    }
}
