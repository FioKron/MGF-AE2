using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{
    // For handling Player Movement:
    Vector3 CurrentPhoneAcceleration;
    Vector3 LastPhoneAcceleration;

    // Handling power level adjustment:
    Touch PointOfContact = new Touch();
    public Text PowerLevelLbl; // To refer to the power level label text
    Vector2 InitialPosition = new Vector2();

    // Player properties (that are shown to the Player):
    float CurrentPowerLevel = 0.0f;

    // For debugging:
    float TestTimer = 0.0f;
    string Message;

    // Constant values:
    const float MOVEMENT_MULTIPLIER = 0.1f;
    const float POWER_LEVEL_ADJUSTMENT_MULTIPLYER = 0.1f;

    // Initilise initilisation:
    void Start ()
    {
        Initilise();       
    }

    // For handling initilisation:
    void Initilise()
    {
        // Make sure the phone's orientation is not automatically updated
        // (or this could lead to errors/difficulty in controling the game)
        Screen.orientation = ScreenOrientation.Portrait;

        // For checking in Update (to detect alterations):
        if (SystemInfo.supportsAccelerometer)
        {
            //Message += "Initilisation Succesful";
            LastPhoneAcceleration = Input.acceleration;
        }
    }

    // Handle Updates in the game:
    void Update ()
    {
        TestTimer += Time.deltaTime;
        /**
            Given Nick's advice, keep in mind the phase of input
            plus only store the position for touches (the rest is not required)
            
            Keep in mind the order of execution, as well:    
        */

        // Handle Power Level Adjustment and movement of the Player:
        GetInitialTouchPosition();
        UpdateCurrentPhoneAcceleration();
        MovePlayer();
        UpdateCurrentPowerLevel();

        // Handle Debugging:
        HandleGameLoopDebugging();    
    }

    // Handle Power Level Adjustment:
    void UpdateCurrentPowerLevel()
    {
        // Check if the one and only point of contact is in the movement phase and so;
        // adjust the power level as appropriate (positivly or negativly):
        if ((Input.touches[0].phase == TouchPhase.Moved) && (Input.touchCount == 1))
        {
            CurrentPowerLevel += InitialPosition.y - Input.touches[0].position.y;

            // Make sure the power level is within the correct range of values:
            ValidateCurrentPowerLevel();

            PowerLevelLbl.text = CurrentPowerLevel.ToString();
        }
    }

    // Debugging handle:
    void HandleGameLoopDebugging()
    {
        // Debugging Sub-System:
        if (TestTimer >= MOVEMENT_MULTIPLIER)
        {
            TestTimer = 0.0f;
            Message = "";
        }
    }

    // If validation is successful...
    void MovePlayer()
    {
        if (CurrentPhoneAcceleration != LastPhoneAcceleration)
        {
            // Simply, one call to this function is all that is required, to move the Player as per the phone's acceleration: 
            transform.Translate(Input.acceleration.x * MOVEMENT_MULTIPLIER, Input.acceleration.y * MOVEMENT_MULTIPLIER, 0.0f);
        }
    }

    // Make sure the power level is between 0 and 100
    void ValidateCurrentPowerLevel()
    {
        if (CurrentPowerLevel > 100)
        {
            CurrentPowerLevel = 100;
        }
        else if (CurrentPowerLevel < 0)
        {
            CurrentPowerLevel = 0;
        }
    }

    // Get the contact point, the moment the screen is touched once:
    Touch GetContactPoint()
    {
        // return the contact point:
        return Input.touches[0];
    }

    void GetInitialTouchPosition()
    {
        /** 
           Get the initial point of contact:
       */
        if ((Input.touchCount == 1) && (Input.touches[0].phase == TouchPhase.Began))
        {
            InitialPosition = GetContactPoint().position;
            //Message += "Contact Point Update";
        }
    }

    // Only update this variable if the validation process, is successful:
    void UpdateCurrentPhoneAcceleration()
    {
        if (SystemInfo.supportsAccelerometer)
        {
            CurrentPhoneAcceleration = Input.acceleration;
        }
    }

    // For testing and/or debugging: 
    void OnGUI()
    {
        // In order to set the font and its respective size:
        GUIStyle DebugStyle = new GUIStyle();
        DebugStyle.fontSize = 36;

        GUI.Label(new Rect(100, 10, 150, 100), Message, DebugStyle);
    }
}
