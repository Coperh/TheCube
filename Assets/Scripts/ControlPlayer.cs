using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlPlayer : MonoBehaviour
{


    private Controls controls;

    private Camera _camera;


    public Transform player;


    [Header("Player")]
    [Tooltip("Rotation speed of the character")]

    public const float roation_speed = 80.0f;
    [Tooltip("Rotation speed of the character")]
    public const float gyro_speed = 2.0f;


    private const float EPISOLON = 0.2E-4f;
    private float y_roation;



    private void Awake()
    {


        controls = new Controls();


        controls.Player.Enable();


        _camera = GetComponent<Camera>();




    }


    // Start is called before the first frame update
    void Start()
    {



        y_roation = 0.0f;





    }



    // Update is called once per frame
    void Update()
    {

        if (controls.Player.Fire.triggered) OnFire();


        if (!UnityEngine.InputSystem.Gyroscope.current.enabled) InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);






        Vector2 stick = controls.Player.Look.ReadValue<Vector2>();


        Vector3 rotaiton = UnityEngine.InputSystem.Gyroscope.current.angularVelocity.ReadValue();


        if (stick.sqrMagnitude > EPISOLON) {

            y_roation -= Time.deltaTime * stick.y * roation_speed;


            player.Rotate(0.0f, Time.deltaTime * stick.x * roation_speed, 0.0f);

        }


        if (rotaiton.sqrMagnitude > EPISOLON) {

            y_roation -= rotaiton.x * gyro_speed;



            player.Rotate(0.0f, -rotaiton.y * gyro_speed, 0.0f);

        }



        //transform.Rotate(stick);
        //Debug.Log(rotaiton);
        //Debug.Log(stick);







        y_roation = Mathf.Clamp(y_roation, -90.0f, 90.0f);

        Debug.Log(y_roation);

        transform.localRotation = Quaternion.Euler(y_roation, 0.0f, 0.0f);
    }






    public void OnLook(InputAction.CallbackContext context)
    {

        Vector2 v = context.ReadValue<Vector2>();

        
        
    }


    public void OnFire()
    {

        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("I hit " + hit.collider.name);

            if (hit.transform.tag == "Target")
            {
                hit.transform.GetComponent<Renderer>().material.color = Color.red;
            }
        }





        Debug.Log("fire");



    }



}
