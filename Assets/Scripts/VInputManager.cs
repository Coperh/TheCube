using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VInputManager : MonoBehaviour
{





    public Vector2 look;

    private GameManager gm;
    private AudioSource _audioSource; 


    // Start is called before the first frame update
    void Start()
    {
        look = Vector2.zero;


        gm = gameObject.GetComponent<GameManager>();
        _audioSource = gameObject.GetComponent<AudioSource>();


    }



    public void VirtualLookInput(Vector2 virtualLookDirection)
    {
        look = virtualLookDirection;



    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
