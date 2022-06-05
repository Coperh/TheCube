using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlPlayer : MonoBehaviour
{




    private Controls controls;

    private Camera _camera;

    private AudioSource _audioSource;

    public Transform player;



    private GameManager gm;

    private TargetHandler th;

    private VInputManager input;



    [Header("Player")]
    [Tooltip("Rotation speed of the character")]

    public const float roation_speed = 1.0f;
    [Tooltip("Rotation speed of the character")]
    public const float gyro_speed = 2.0f;


    private const float EPISOLON = 0.2E-4f;
    private float y_roation;


    private bool gyro_enabled;


    private void Awake()
    {

        controls = new Controls();

        controls.Player.Enable();

        _camera = GetComponent<Camera>();

    }


    // Start is called before the first frame update
    void Start()
    {

        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        th = GameObject.Find("Targets").GetComponent<TargetHandler>();
        input = GameObject.Find("GameManager").GetComponent<VInputManager>();


        gyro_enabled = gm.gyro_enabled;

        _audioSource = this.transform.parent.GetComponent<AudioSource>();

        y_roation = 0.0f;

    }

    






    // Update is called once per frame
    void Update()
    {

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        if (controls.Player.Fire.triggered) OnFire(ray);

        if (gm.game_started) Move();




        Debug.DrawRay(ray.origin, ray.direction * 10.0f, Color.red);


    }











    public void Move() {



        Vector2 stick = input.look;



        y_roation -= Time.deltaTime * stick.y * roation_speed;


        
        
        player.Rotate(0.0f, Time.deltaTime * stick.x * roation_speed, 0.0f);



        if (gyro_enabled)
        {
            Vector3 rotaiton = UnityEngine.InputSystem.Gyroscope.current.angularVelocity.ReadValue();

            if (rotaiton.sqrMagnitude > EPISOLON)
            {

                y_roation -= rotaiton.x * gyro_speed;


                player.Rotate(0.0f, -rotaiton.y * gyro_speed, 0.0f);
            }

        }

        y_roation = Mathf.Clamp(y_roation, -90.0f, 90.0f);

        //Debug.Log(y_roation);

        transform.localRotation = Quaternion.Euler(y_roation, 0.0f, 0.0f);

    }


    public void OnFire(Ray ray)
    {

        
        Debug.DrawRay(ray.origin, ray.direction * 10.0f, Color.yellow, 10.0f);

        //Debug.Log(ray.origin + " " + ray.direction);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {

            if (hit.transform.tag == "Target" && hit.transform.gameObject.activeInHierarchy)
            {
                gm.score++;

                hit.transform.gameObject.SetActive(false);
                _audioSource.Play();

                gm.ReduceTargetCount();
            }
            else
            {

                Debug.Log("I hit " + hit.collider.name);
            }
        }

        gm.shots_fired++;

        gm.accuracy = gm.score * 100 / gm.shots_fired;

    }

















    public void OnLook(InputAction.CallbackContext context)
    {


        Vector2 v = context.ReadValue<Vector2>();

        
        
    }


}
