﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Console
{
    public abstract class ConsoleCommand
    {
        public abstract string Name { get; protected set; }

        public abstract string Command { get; protected set; }

        public abstract string Description { get; protected set; }

        public abstract string Help { get; protected set; }

        public abstract string Example { get; protected set; }


        public void AddCommandToConsole()
        {
            DevConsole.AddCommandsToConsole(Command, this);
            // string addMessage = " command has been added to the console.";
            // DevConsole.AddStaticMessageToConsole(Name + addMessage);
        }

        public abstract void RunCommand(string[] data);
    }

    public class DevConsole : MonoBehaviour
    {
        public static DevConsole Instance { get; set; }

        public static Dictionary<string, ConsoleCommand> Commands { get; set; }

        [SerializeField]
        private string Mode;

        [SerializeField]
        private GameObject _consoleCanvas;

        [SerializeField]
        private CameraController _cameraControl;

        [SerializeField]
        private ScrollRect _scrollRect;

        [SerializeField]
        private Text _consoleText;

        [SerializeField]
        private Text _inputText;

        [SerializeField]
        private InputField _consoleInput;

        [SerializeField]
        [Tooltip("Define how many commands can be hold in the clipboard. If set to 0, clipboard will be off.")]
        private int _clipboardSize;

        private string[] _clipboard;

        private int _clipboardIndexer = 0;

        private int _clipboardCursor = 0;

        [SerializeField]
        [Tooltip("Specify minimum amount of characters for autocomplete key(TAB) to work.")]
        private int _tabMinCharLength = 3;

        #region Colors

        public static string RequiredColor = "#FA8072";

        public static string OptionalColor = "#00FF7F";

        public static string WarningColor = "#ffcc00";

        public static string ExecutedColor = "#e600e6";

        #endregion

        #region Typical Console Messages

        public static string NotRecognized = $"Command not <color={WarningColor}>recognized</color>";

        public static string ExecutedSuccessfully = $"Command executed <color={ExecutedColor}>successfully</color>";

        public static string ParametersAmount = $"Wrong <color={WarningColor}>amount of parameters</color>";

        public static string TypeNotSupported = $"Type of command <color={WarningColor}>not supported</color>";

        public static string Error = $"Typed Command caused <color={WarningColor}>ERROR</color>";

        public static string SceneNotFound = $"Scene <color={WarningColor}>not found</color>." +
                                             $" Make sure that you have placed it inside <color={WarningColor}>build settings</color>";

        public static string ClipboardCleared = $"\nConsole clipboard <color={OptionalColor}>cleared</color>";
        #endregion

        private void Awake()
        {
            if (Instance != null)
            {
                return;
            }

            Instance = this;

            Commands = new Dictionary<string, ConsoleCommand>();
        }

        private void Start()
        {

            _clipboard = new string[_clipboardSize];

            _consoleCanvas.gameObject.SetActive(false);

            var primary = "#F9F0E6";
            var secondary = "#B3E6F9";

            CreateCommands();
        }

        private void CreateCommands()
        {
            CommandHelp.CreateCommand();

            var commandClearList = CommandClearConsole.CreateCommand();
            commandClearList.ConsoleTextRef = _consoleText;
            commandClearList.ConsoleStartingInfo = _consoleText.text;
            
            if(Mode == "Advance"){ // Advance Mode
                //.CreateCommand To Enable Command Creation
            } else if(Mode == "Normal"){ // Survival Mode;
                
            }
        }

        public static void AddCommandsToConsole(string name, ConsoleCommand command)
        {
            if (!Commands.ContainsKey(name))
            {
                Commands.Add(name, command);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if(!_consoleCanvas.gameObject.activeInHierarchy) {  }

                _consoleCanvas.gameObject.SetActive
                    (!_consoleCanvas.gameObject.activeInHierarchy);

                _cameraControl._enableMovement = !_consoleCanvas.gameObject.activeInHierarchy;
                _consoleInput.ActivateInputField();
                _consoleInput.Select();
            }

            if (_consoleCanvas.gameObject.activeInHierarchy)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (string.IsNullOrEmpty(_inputText.text) == false)
                    {
                        AddMessageToConsole(_inputText.text);
                        
                        ParseInput(_inputText.text);

                        if (_clipboardSize != 0)
                        {
                            StoreCommandInTheClipboard(_inputText.text);
                        }
                    }

                    // Clears input
                    _consoleInput.text = "";

                    _consoleInput.ActivateInputField();
                    _consoleInput.Select();
                }

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (_clipboardSize != 0 && _clipboardIndexer != 0)
                    {
                        if (_clipboardCursor == _clipboardIndexer)
                        {
                            _clipboardCursor--;
                            _consoleInput.text = _clipboard[_clipboardCursor];
                        }
                        else
                        {
                            if (_clipboardCursor > 0)
                            {
                                _clipboardCursor--;
                                _consoleInput.text = _clipboard[_clipboardCursor];
                            } else
                            {
                                _consoleInput.text = _clipboard[0];
                            }
                        }
                        _consoleInput.caretPosition = _consoleInput.text.Length;
                    }
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (_clipboardSize != 0 && _clipboardIndexer != 0)
                    {
                        if (_clipboardCursor < _clipboardIndexer)
                        {
                            _clipboardCursor++;
                            _consoleInput.text = _clipboard[_clipboardCursor];
                            _consoleInput.caretPosition = _consoleInput.text.Length;
                        }
                    }
                }

                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    int inputLength = _consoleInput.text.Length;
                    
                    if(inputLength >= _tabMinCharLength && _consoleInput.text.Any(char.IsWhiteSpace) == false)
                    {
                        foreach (var command in Commands)
                        {
                            string commandKey =
                                command.Key.Length <= inputLength ? command.Key : command.Key.Substring(0, inputLength);

                            if (_consoleInput.text.ToLower().StartsWith(commandKey.ToLower()))
                            {
                                _consoleInput.text = command.Key;
                                
                                _consoleInput.caretPosition = command.Key.Length;
                                break;
                            }
                        }
                    }
                }
            }

            if (_consoleCanvas.gameObject.activeInHierarchy == false)
            {
                _consoleInput.text = "";
            }
        }

        private IEnumerator ScrollDown()
        {
            yield return new WaitForSeconds(0.1f);
            _scrollRect.verticalNormalizedPosition = 0f;
        }

        private void StoreCommandInTheClipboard(string command)
        {
            _clipboard[_clipboardIndexer] = command;

            if (_clipboardIndexer < _clipboardSize - 1)
            {
                _clipboardIndexer++;
                _clipboardCursor = _clipboardIndexer;
            } 
            else if (_clipboardIndexer == _clipboardSize - 1)
            {
                // Clear clipboard & reset 
                _clipboardIndexer = 0;
                _clipboardCursor = 0;
                for(int i = 0; i < _clipboardSize; i++)
                {
                    _clipboard[i] = string.Empty;
                }

                AddStaticMessageToConsole(ClipboardCleared);
            }
        }

        private void AddMessageToConsole(string msg)
        {
            _consoleText.text += msg + "\n";
        }

        ///<>
        public static void AddStaticMessageToConsoleColored(string msg, string color){
            Instance._consoleText.text += $"<color=#{color}>{msg}</color>";
        }

        public static void AddStaticMessageToConsole(string msg)
        {
            Instance._consoleText.text += msg + "\n";
        }

        private void ParseInput(string input)
        {
            string[] commandSplitInput = input.Split(null);

            if (string.IsNullOrWhiteSpace(input))
            {
                AddMessageToConsole(NotRecognized);
                return;
            }

            if (Commands.ContainsKey(commandSplitInput[0]) == false)
            {
                AddMessageToConsole(NotRecognized);
            }
            else
            {
                Commands[commandSplitInput[0]].RunCommand(commandSplitInput);
            }

            // Force scroll
            StartCoroutine(ScrollDown());
        }
    }
}