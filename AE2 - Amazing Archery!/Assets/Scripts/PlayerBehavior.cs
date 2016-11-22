using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // The Phone's Acceleration in the real world (for detecting acceleration)
    Vector3 PhoneAcceleration;

    // To see if there was any alteration from the previous Acceleration
    Vector3 LastPhoneAcceleration;

    // For altering the sensitivity of movement:
    const float MOVEMENT_MULTIPLYER = 0.1f;

    // Player properties (that are shown to the Player):
    float CurrentPowerLevel = 0.0f;

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
        Message = "";
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

            // Simply, one call to this function is all that is required, to move the Player as per the phone's acceleration: 
            transform.Translate(Input.acceleration.x * MOVEMENT_MULTIPLYER, Input.acceleration.y * MOVEMENT_MULTIPLYER, 0.0f);
        }

        if (TestTimer >= 1000.0f)
        {
            TestTimer = 0.0f;
            Message = "";
        }

        /** 
            The Player is dragging their touch implement across the screen 
            (if this is the case), so handle adjustment of power level:
        */
        /**if (Input.touchCount == 1)
        {
            HandlePowerLevelAdjustment();
        }
        */
        Message += Input.touches[0].position;
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
    */

    // Adjust the power level as appropriate:
    void HandlePowerLevelAdjustment()
    {
        // Check on the first and only touch input:
        Touch ContactPoint = Input.touches[0];
        Vector2 InitialContactPointPosition = ContactPoint.position;

        // Check this input for movement:
        if (ContactPoint.phase == TouchPhase.Moved)
        {
            Vector2 CurrentContactPointPosition = ContactPoint.position;
            
            if (CurrentContactPointPosition.y < InitialContactPointPosition.y)
            {

            }
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
