using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour
{
    // The Phone's orientation in the real world (for detecting acceleration)
    Quaternion PhoneOrientation;

    // To see if there was any alteration from the previous orientation
    Quaternion LastPhoneOrientation;

	// Initilise this script:
	void Start ()
    {
        // For checking in Update (to detect alterations):
        if (SystemInfo.supportsGyroscope)
        {
            LastPhoneOrientation = Input.gyro.attitude;
        }
    }
	
	// Handle Updates in the game:
	void Update ()
    {
        if (SystemInfo.supportsGyroscope)
        {
            PhoneOrientation = Input.gyro.attitude;
        }

        if (PhoneOrientation != LastPhoneOrientation)
        {
            AdjustAimPointForPhoneOrientation();
        }
    }

    void AdjustAimPointForPhoneOrientation()
    {
        /**
         * Only analyse rotation around the X and Z axes:
        */

        // Obtain the different for pitch first 
        // (moving the Player if there is a difference):
        float UpDownDirectionMagnitude = PhoneOrientation.x - LastPhoneOrientation.x;

        if (UpDownDirectionMagnitude != 0.0f)
        {
            MoveCrosshairX(UpDownDirectionMagnitude);
        }

        // Then roll:
        float LeftRightDirectionMagnitude = PhoneOrientation.z - LastPhoneOrientation.z;

        if (LeftRightDirectionMagnitude != 0.0f)
        {
            MoveCrosshairZ(LeftRightDirectionMagnitude);
        }
    }

    void MoveCrosshairX(float UpDownDirectionMagnitude)
    {
        transform.Translate(0.0f, UpDownDirectionMagnitude, 0.0f);
    }

    void MoveCrosshairZ(float LeftRightDirectionMagnitude)
    {
        transform.Translate(LeftRightDirectionMagnitude, 0.0f, 0.0f);
    }
}
