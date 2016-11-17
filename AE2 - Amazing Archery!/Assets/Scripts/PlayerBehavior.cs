using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour
{
    // The Phone's orientation in the real world (for detecting acceleration)
    Quaternion PhoneOrientation;

    // To see if there was any alteration from the previous orientation
    Quaternion LastPhoneOrientation;

    // For debugging:
    float TestTimer = 0.0f;
    string Message;

    // Initilise this script:
    void Start ()
    {
        // You are required to explicitly enable the gyro-sensor (Nick would not know why either):
        Input.gyro.enabled = true;

        Screen.orientation = ScreenOrientation.Portrait;

        // For checking in Update (to detect alterations):
        if (SystemInfo.supportsGyroscope)
        {
            //Message += "Initilisation Succesful";
            LastPhoneOrientation = Input.gyro.attitude;
        }
    }
	
	// Handle Updates in the game:
	void Update ()
    {
        TestTimer += Time.deltaTime;

        if (SystemInfo.supportsGyroscope)
        {
            //Message += "But this device supports a gyroscope...";
            PhoneOrientation = Input.gyro.attitude;
        }

        if (PhoneOrientation != LastPhoneOrientation)
        {
            //Message += "...time to adjust then...";
            AdjustAimPointForPhoneOrientation();
        }

        if (TestTimer >= 1000.0f)
        {
            TestTimer = 0.0f;
            Message = "";
        }
    }

    void AdjustAimPointForPhoneOrientation()
    {
        /**
         * Only analyse rotation around the X and Z axes:
        */

        /**
        // Obtain the different for pitch first 
        // (moving the Player if there is a difference):
        
        Multiply to add or subtract a quaternion (a x b = c to add, Inverse(a) x b = c to subtract):

        //Quaternion a;
        //Quaternion b;
        //Quaternion c = Quaternion.Inverse(a) * b;
        */
        float UpDownDirectionMagnitude = PhoneOrientation.eulerAngles.x - LastPhoneOrientation.eulerAngles.x;

        //Message += PhoneOrientation;//transform.position;//"CurrentOrientation: " + PhoneOrientation + " LastOrientation: " + LastPhoneOrientation;

        if (UpDownDirectionMagnitude != 0.0f)
        {
            MoveCrosshairX(UpDownDirectionMagnitude);
        }

        // Then roll:
        float LeftRightDirectionMagnitude = PhoneOrientation.eulerAngles.z - LastPhoneOrientation.eulerAngles.z;

        //Message += LeftRightDirectionMagnitude;

        if (LeftRightDirectionMagnitude != 0.0f)
        {
            MoveCrosshairZ(LeftRightDirectionMagnitude);
        }
    }

    void MoveCrosshairX(float UpDownDirectionMagnitude)
    {
        // For a 'normalised' direction:
        float FinalUpDownDirectionMagnitude = 0.0f;

        if (UpDownDirectionMagnitude > 0.0f)
        {
            FinalUpDownDirectionMagnitude = 0.01f;
        }
        else if (UpDownDirectionMagnitude < 0.0f)
        {
            FinalUpDownDirectionMagnitude = -0.01f;
        }

        transform.Translate(0.0f, FinalUpDownDirectionMagnitude, 0.0f);
    }

    void MoveCrosshairZ(float LeftRightDirectionMagnitude)
    {
        // For a 'normalised' direction:
        float FinalLeftRightDirectionMagnitude = 0.0f;

        if (LeftRightDirectionMagnitude > 0.0f)
        {
            FinalLeftRightDirectionMagnitude = 0.01f;
        }
        else if (LeftRightDirectionMagnitude < 0.0f)
        {
            FinalLeftRightDirectionMagnitude = -0.01f;
        }

        transform.Translate(FinalLeftRightDirectionMagnitude, 0.0f, 0.0f);
    }

    // For testing:
    void OnGUI()
    {
        // In order to set the font and its respective size:
        GUIStyle DebugStyle = new GUIStyle();
        DebugStyle.fontSize = 36;

        GUI.Label(new Rect(100, 10, 150, 100), Message, DebugStyle);
    }
}
