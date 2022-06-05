using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreen : MonoBehaviour
{


    [SerializeField]
    private TMP_Text scoreDisp;

    [SerializeField]
    private TMP_Text firedDisp;

    [SerializeField]
    private TMP_Text timeDisp;

    [SerializeField]
    private TMP_Text accDisp;


    private GameManager gm;


    // Start is called before the first frame update
    void Awake()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

    }


    private void Start()
    {

       
        
        accDisp.text = gm.accuracy + "%";

        scoreDisp.text = gm.score.ToString();
        firedDisp.text = gm.shots_fired.ToString();


        timeDisp.text = gm.time.ToString();

    }


   


    // Update is called once per frame
    void Update()
    {
        
    }
}
