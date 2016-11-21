using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour
{
    // The Phone's Acceleration in the real world (for detecting acceleration)
    Vector3 PhoneAcceleration;

    // To see if there was any alteration from the previous Acceleration
    Vector3 LastPhoneAcceleration;

    // For altering the sensitivity of movement:
    const float MOVEMENT_MULTIPLYER = 0.1f;

    // For debugging:
    float TestTimer = 0.0f;
    string Message;

    // Initilise this script:
    void Start ()
    {
        // You are required to explicitly enable the gyro-sensor (Nick would not know why either):
        //Input.gyro.enabled = true;

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

        if (SystemInfo.supportsAccelerometer)
        {
            //Message += "But this device supports a gyroscope...";
            PhoneAcceleration = Input.acceleration;
        }

        if (PhoneAcceleration != LastPhoneAcceleration)
        {
            //Message += "...time to adjust then...";
            //AdjustAimPointForPhoneAcceleration();

            // Simply, one call to this function is all that is required, it seems: 
            transform.Translate(Input.acceleration.x * MOVEMENT_MULTIPLYER, Input.acceleration.y * MOVEMENT_MULTIPLYER, 0.0f);
        }

        if (TestTimer >= 1000.0f)
        {
            TestTimer = 0.0f;
            Message = "";
        }
    }

    /**
    void AdjustAimPointForPhoneAcceleration()
    {
        
        Only analyse acceleration around the X and Z axes:
        
        float FinalLeftRightDirectionMagnitude = 0.0f;
        float FinalUpDownDirectionMagnitude = 0.0f;

        
        // VOID
        Obtain the different for pitch first 
        // (moving the Player if there is a difference):
        
        Multiply to add or subtract a quaternion (a x b = c to add, Inverse(a) x b = c to subtract):

        //Quaternion a;
        //Quaternion b;
        //Quaternion c = Quaternion.Inverse(a) * b;
        

        // Along Y first
        // (Get the absolute value):
        float UpDownDirectionMagnitude = Mathf.Abs(PhoneAcceleration.y - LastPhoneAcceleration.y);

        //Message += PhoneAcceleration;//transform.position;//"CurrentAcceleration: " + PhoneAcceleration + " LastAcceleration: " + LastPhoneAcceleration;

        if (UpDownDirectionMagnitude != 0.0f)
        {
            FinalUpDownDirectionMagnitude = GetFinalUpDownDirectionMagnitude(UpDownDirectionMagnitude);
        }

        // Then X
        // (Get the absolute value):
        float LeftRightDirectionMagnitude = Mathf.Abs(PhoneAcceleration.x - LastPhoneAcceleration.x);

        //Message += LeftRightDirectionMagnitude;

        if (LeftRightDirectionMagnitude != 0.0f)
        {
            FinalLeftRightDirectionMagnitude = GetFinalLeftRightDirectionMagnitude(LeftRightDirectionMagnitude);          
        }
        //Message += "X: " + FinalLeftRightDirectionMagnitude + " Y: " + FinalUpDownDirectionMagnitude;
        transform.Translate(FinalLeftRightDirectionMagnitude, FinalUpDownDirectionMagnitude, 0.0f);
    }
    
    float GetFinalLeftRightDirectionMagnitude(float LeftRightDirectionMagnitude)
    {
        // For a 'normalised' direction:
        float FinalLeftRightDirectionMagnitude = 0.0f;

        if (LeftRightDirectionMagnitude > 0.0f)
        {
            FinalLeftRightDirectionMagnitude = LeftRightDirectionMagnitude * MOVEMENT_MULTIPLYER;
        }
        else if (LeftRightDirectionMagnitude < 0.0f)
        {
            FinalLeftRightDirectionMagnitude = -(LeftRightDirectionMagnitude) * MOVEMENT_MULTIPLYER;
        }
        Message += FinalLeftRightDirectionMagnitude;
        return FinalLeftRightDirectionMagnitude;
    }
    
    float GetFinalUpDownDirectionMagnitude(float UpDownDirectionMagnitude)
    {
        For a 'normalised' direction:
        float FinalUpDownDirectionMagnitude = 0.0f;

        if (UpDownDirectionMagnitude > 0.0f)
        {
            FinalUpDownDirectionMagnitude = UpDownDirectionMagnitude * MOVEMENT_MULTIPLYER;
        }
        else if (UpDownDirectionMagnitude < 0.0f)
        {
            FinalUpDownDirectionMagnitude = -(UpDownDirectionMagnitude) * MOVEMENT_MULTIPLYER;
        }

        Message += FinalUpDownDirectionMagnitude;
        return FinalUpDownDirectionMagnitude;
        
    }
    
    
    For testing:
    void OnGUI()
    {
        // In order to set the font and its respective size:
        GUIStyle DebugStyle = new GUIStyle();
        DebugStyle.fontSize = 36;

        GUI.Label(new Rect(100, 10, 150, 100), Message, DebugStyle);
    }
    */
}
