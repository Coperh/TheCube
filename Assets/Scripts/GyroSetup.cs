using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GyroSetup 
{


    public static IEnumerator StartGyro()
    {

      

        Debug.Log("Starting Gyroscope");

        while (UnityEngine.InputSystem.Gyroscope.current.enabled != true)
        {
            InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);

            Debug.Log("Trying");
            yield return new WaitForSeconds(4);

        }

        Debug.Log("Gyroscope Enabled");
        yield return null;
    }


    public static IEnumerator StopGyro() {



        Debug.Log("Disabling Gyroscope");

        while (UnityEngine.InputSystem.Gyroscope.current.enabled)
        {
            InputSystem.DisableDevice(UnityEngine.InputSystem.Gyroscope.current);

            Debug.Log("Trying");
            yield return new WaitForSeconds(4);

        }

        Debug.Log("Gyroscope Disabled");
        yield return null;





        yield return null;
    }






}
