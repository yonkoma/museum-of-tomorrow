using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class MenuControl : MonoBehaviour
{

    private MLInputController _controller;

    private GameObject _modeSelectPanel;
    private GameObject _setUpToolsPanel;
    private GameObject _Scan;
    private GameObject _Light;
    private GameObject _PaintingInfo;
    private GameObject _ArtistInfo;
    private GameObject _RelatedWorks;
    private GameObject _Arrow;
    private GameObject _Settings;

    private Dictionary<int, GameObject> _screens;

    private int _curScreen;

    private bool _bumperPressed = false;
    private bool _homePressed = false;

    private int _storedScreen = -1;


    // Start is called before the first frame update
    void Start()
    {

        _screens = new Dictionary<int, GameObject>();

        _modeSelectPanel = GameObject.Find("MenuCanvas/MenuPanel/ModeSelectPanel");
        _modeSelectPanel.SetActive(true);

        _screens.Add(0, _modeSelectPanel);

        _setUpToolsPanel = GameObject.Find("MenuCanvas/MenuPanel/SetupToolsPanel");
        _setUpToolsPanel.SetActive(false);

        _screens.Add(1, _setUpToolsPanel);

        _Scan = GameObject.Find("MenuCanvas/MenuPanel/Scan");
        _Scan.SetActive(false);

        _screens.Add(2, _Scan);

        _Light = GameObject.Find("MenuCanvas/MenuPanel/Light");
        _Light.SetActive(false);

        _screens.Add(3, _Light);

        _PaintingInfo = GameObject.Find("MenuCanvas/MenuPanel/PaintingInfo");
        _PaintingInfo.SetActive(false);

        _screens.Add(4, _PaintingInfo);

        _ArtistInfo = GameObject.Find("MenuCanvas/MenuPanel/ArtistInfo");
        _ArtistInfo.SetActive(false);

        _screens.Add(5, _ArtistInfo);

        _RelatedWorks = GameObject.Find("MenuCanvas/MenuPanel/RelatedWorks");
        _RelatedWorks.SetActive(false);

        _screens.Add(6, _RelatedWorks);



        _Settings = GameObject.Find("MenuCanvas/MenuPanel/Settings");
        _Settings.SetActive(false);


        _curScreen = 0;

        MLInput.Start();
        MLInput.OnControllerButtonDown += OnButtonDown;
        MLInput.OnControllerButtonUp += OnButtonUp;


        _controller = MLInput.GetController(MLInput.Hand.Left);

        _Arrow = GameObject.Find("Arrow");
        _Arrow.SetActive(false);


    }

    void OnDestroy()
    {
        MLInput.OnControllerButtonDown -= OnButtonDown;
        MLInput.OnControllerButtonUp -= OnButtonUp;
        MLInput.Stop();

    }

    // Update is called once per frame
    void Update()
    {
        CheckControl();

    }

    void CheckControl()
    {


        if (_controller.TriggerValue > 0.2f)
        {

            if (_curScreen == 6)
            {

                _Arrow.SetActive(true);

            }
        }

    }



    void OnButtonDown(byte controller_id, MLInputControllerButton button)
    {

        // Bumper is left/default option, Home is right

        if (_bumperPressed & _homePressed)
        {

            _Arrow.SetActive(true);

            _storedScreen = _curScreen;
            _curScreen = -1;

            _screens[_storedScreen].SetActive(false);
            _Settings.SetActive(true);


        }

        else if (button == MLInputControllerButton.Bumper)
        {

            _bumperPressed = true;

            Debug.Log("_bumperPressed = True");

            switch (_curScreen)
            {
                case 0:

                    _screens[0].SetActive(false);
                    _screens[1].SetActive(true);
                    _curScreen = 1;
                    break;
                case 1:
                    _screens[1].SetActive(false);
                    _screens[2].SetActive(true);
                    _curScreen = 2;
                    break;
                case 2:
                    _screens[2].SetActive(false);
                    _screens[1].SetActive(true);
                    _curScreen = 1;
                    break;
                case 3:
                    _screens[3].SetActive(false);
                    _screens[1].SetActive(true);
                    _curScreen = 1;
                    break;
                case 4:
                    _screens[4].SetActive(false);
                    _screens[5].SetActive(true);
                    _curScreen = 5;
                    break;
                case 5:
                    _screens[5].SetActive(false);
                    _screens[4].SetActive(true);
                    _curScreen = 4;
                    break;
                case 6:
                    _screens[6].SetActive(false);
                    _screens[4].SetActive(true);
                    _curScreen = 4;
                    _Arrow.SetActive(false);
                    break;

                default:
                    break;


            }
        }

        else if (button == MLInputControllerButton.HomeTap)
        {

            _homePressed = true;


            switch (_curScreen)
            {
                case 0:

                    _screens[0].SetActive(false);
                    _screens[4].SetActive(true);
                    _curScreen = 4;
                    break;
                case 1:
                    _screens[1].SetActive(false);
                    _screens[3].SetActive(true);
                    _curScreen = 3;
                    break;
                case 4:
                    _screens[4].SetActive(false);
                    _screens[6].SetActive(true);
                    _curScreen = 6;
                    break;
                case 5:
                    _screens[5].SetActive(false);
                    _screens[6].SetActive(true);
                    _curScreen = 6;
                    break;
                case 6:
                    _screens[6].SetActive(false);
                    _screens[5].SetActive(true);
                    _curScreen = 5;
                    _Arrow.SetActive(false);
                    break;

                default:
                    Console.WriteLine("Default Case");
                    break;


            }
        }


    }

    void OnButtonUp(byte controller_id, MLInputControllerButton button)
    {

        if (button == MLInputControllerButton.Bumper)
        {
            _bumperPressed = false;

        }
        else if (button == MLInputControllerButton.HomeTap)
        {
            _homePressed = false;

        }

        if ( _curScreen == -1)
        {
            _Settings.SetActive(false);
            _screens[_storedScreen].SetActive(true);
            _curScreen = _storedScreen;
        }



    }

}
