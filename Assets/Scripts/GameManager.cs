using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update




    


    public GameObject [][] targets;


    private GameObject menuCam;

    [Tooltip("Player Character Model")]
    [SerializeField]
    private GameObject player;

    private GameObject player_instance;

    [Tooltip("Start Menu")]
    [SerializeField]
    private GameObject menu;

    [Tooltip("Heads Up Display")]
    [SerializeField]
    private GameObject hud;

    [Tooltip("End Score Screen")]
    [SerializeField]
    private GameObject endScreen;

    [Tooltip("Time in seconds each round lasts")]
    [SerializeField]
    private const int RoundTime = 60;


    [SerializeField]
    private TMP_Text gyroStatusText;

    public TMP_Text scoreDisp;
    private int _score;

    public int score { 
        get { return _score; }
        set { _score = value; }
    }

    private int _shoshots_fired;
    public int shots_fired { 
        get { return _shoshots_fired; }
        set { _shoshots_fired = value; }
    }


    [SerializeField]
    private TMP_Text timeDisp;
    private int _time;
    public int time { 
        get { return _time; }
        set { _time = value; }
    }



    [SerializeField]
    private TMP_Text accDisp;
    private float _accuracy;

    public float accuracy
    {
        get { return _accuracy; }
        set { _accuracy = value; }
    }


    // Countdown hud elements
    private GameObject countdown;
    private TMP_Text count_Text;


    // Target handler for spawning targets
    private TargetHandler targetHandler;
    private IEnumerator th_spawn;

    // Coroutine for keeping track of match time.
    private IEnumerator time_keeper;




    //public TMP_Text text;

    public bool gyro_enabled;
    private bool last_enabled;

    private bool is_timed;
    public bool game_started = false;






    private void Awake()
    {
        menu.SetActive(true);
        endScreen.SetActive(false);
        hud.SetActive(false);

        menuCam = GameObject.Find("BackgroundCamera");
        gyroStatusText.text = "Gyro Disabled"; gyroStatusText.color = Color.red;
        last_enabled = false;
        gyro_enabled = false;




        targetHandler = GameObject.Find("Targets").GetComponent<TargetHandler>();
    }



    void Start()
    {


        countdown = hud.transform.Find("CountDown").gameObject;

        count_Text = countdown.transform.GetChild(1).GetComponent <TMP_Text>();



        th_spawn = targetHandler.RandomSpawn();


        if (SystemInfo.supportsGyroscope) Debug.Log("GyroScope Supported");
  
    }

    /**
     * Resets Info displayed on HUD
     */
    void ClearHUD()
    {
        _accuracy = 100.0f;
        _score = 0;
        _shoshots_fired = 0;
        _time = 0;

        scoreDisp.text = _score.ToString();
        accDisp.text = _accuracy.ToString() + "%";
        timeDisp.text = _time.ToString();
    }






    public void OnStart(bool timed) {


        is_timed = timed;


        ClearHUD();

        // Hide menu schreen
        menu.SetActive(false);
        // Show hud and activate Coundown screen
        hud.SetActive(true);
        countdown.SetActive(true);


        // Create player
        player_instance = Instantiate(player);

        // Disable Background Cam
        menuCam.SetActive(false);

        // Enable player camera and poistion player
        player_instance.transform.GetChild(1).GetComponent<Camera>().enabled = true;
        player_instance.transform.position = new Vector3(0.0f, 1.0f, 0.0f);


        targetHandler.Clear();


        StartCoroutine(FinalCountDown());

    }



    private IEnumerator FinalCountDown() {



        for (int i = 3; i > 0; i--) {

            count_Text.text = i.ToString();

            yield return new WaitForSeconds(1);
        }

        count_Text.text = "Start";

        yield return new WaitForSeconds(0.2f);


        game_started = true;
        countdown.SetActive(false);
        StartCoroutine(th_spawn);

        Debug.Log("Start");
        StartCoroutine(TimeKeep(is_timed));
        
    }






    public void OnExit() {


        StopCoroutine(th_spawn);
        targetHandler.Clear();


        endScreen.SetActive(true);

        hud.SetActive(false);

        menuCam.SetActive(true);

        player.transform.GetChild(1).GetComponent<Camera>().enabled = false;
        Destroy(player_instance);

        game_started = false;
    }

    public void ReturnMenu() {
        endScreen.SetActive(false);
        menu.SetActive(true);
    }


    /**
     * Keeps track of time
     */
    IEnumerator TimeKeep(bool timed) {

        Debug.Log("Boop, ITs TIme Time");

        // if round is timed
        if (timed)
        {
            for (int i = 0; i < RoundTime+1; i++)
            {

                _time = RoundTime - i;

                timeDisp.text = _time.ToString();
                yield return new WaitForSeconds(1);
            }
            OnExit();
        }
        // if free round
        else
        {

            while (game_started)
            {

                timeDisp.text = _time.ToString();

                yield return new WaitForSeconds(1);
                _time++;
            }
        }
        yield return null;
    }



    public void ReduceTargetCount() {

        targetHandler.current_active--;
    
    }




    // Update is called once per frame
    void Update()
    {

        gyro_enabled = UnityEngine.InputSystem.Gyroscope.current.enabled;

        if (last_enabled != gyro_enabled) { 

            if (gyro_enabled) { gyroStatusText.text = "Gyro Enabled";  gyroStatusText.color = Color.green; }
            else { gyroStatusText.text = "Gyro Disabled"; gyroStatusText.color = Color.red; }
        }

        last_enabled = gyro_enabled;

        if (game_started)
        {
            scoreDisp.text = _score.ToString();

            accDisp.text = _accuracy.ToString() + "%";
        }


    }



    


}


