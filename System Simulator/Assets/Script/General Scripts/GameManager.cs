using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [BoxGroup("Quick Keys")]
    public GameObject _characterStatistic;

    [BoxGroup("Attributes")]
    public float _health, _defense, _attack, _mana; // _attack total damage done with normal attack, _mana total castable

    [BoxGroup("Input")]
    public GameObject _windows; // Parent of the Windows, used for getting windows
    [BoxGroup("Input")]
    public TMP_Text _time;

    #region Time Attributes
    string _currentLocation;
    string _timeOfDay = "AM";
    int _hour = 1, _minute, _seconds;
    int _days, _months, _years;
    float timer = 0;
    #endregion

    public void Update() {
        _time.text = $"{_hour:00}:{_minute:00}{_timeOfDay}";

        TimeManager();
        if(Input.GetKeyDown(KeyCode.C)){
            _characterStatistic.SetActive(!_characterStatistic.activeInHierarchy);
        }
    }

    public void TimeManager() {
        if (timer >= 0.001f) {
            _seconds++;
            if (_seconds >= 60) {
                _seconds = 0;
                _minute++;
                if (_minute >= 60) {
                    _minute = 0;
                    _hour++;
                    if (_hour >= 12) {
                        _hour = 1;

                        if(_timeOfDay == "AM"){
                            _timeOfDay = "PM";
                        } else if(_timeOfDay == "PM"){
                            _timeOfDay = "AM";
                        }

                        _days++;
                        if (_days >= 32) {
                            _days = 0;
                            _months++;
                            if (_months >= 12) {
                                _months = 0;
                                _years++;
                            }
                        }
                    }
                }
            }
            timer = 0;
        }
        else {
            timer = Time.deltaTime;
        }
    }
}
