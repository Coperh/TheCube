using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using System.Collections;


public class ToggleSwitch : MonoBehaviour, IPointerDownHandler
{


    [SerializeField]
    private bool _isOn = false;

    public bool isOn 
    { 
        get { return _isOn; }
    
    }

    [SerializeField]
    RectTransform toggleIndicator;

    [SerializeField]
    private Image bacgkgroundImage;

    [SerializeField]
    private Color onColor;

    [SerializeField]
    private Color offColor;

    [SerializeField]
    private float tweenTime = 0.25f;


    private float offx;
    private float onx;

    public delegate void ValueChanged(bool value);

    public event ValueChanged valueChanged;




    private IEnumerator disableGyro;
    private IEnumerator enableGyro;

    public void OnPointerDown(PointerEventData eventData)
    {
        Toggle(!isOn);  
    }

    // Start is called before the first frame update
    void Start()
    {
        offx = toggleIndicator.anchoredPosition.x;
        onx = bacgkgroundImage.rectTransform.rect.width - toggleIndicator.rect.width;

        enableGyro = GyroSetup.StartGyro();
        disableGyro = GyroSetup.StopGyro();



    }


    private void OnEnable()
    {
        ToggleColor(isOn);

    }




    private void Toggle(bool value, bool playSFX = false) {

        if (value != isOn) {

            _isOn = value;

            ToggleColor(isOn);
            MovedIndicator(isOn);
            ToggleGyro(isOn);

            if (valueChanged != null)
                valueChanged(isOn);
            

        }
    }


    private void ToggleColor(bool value)
    {
        if (value)
            bacgkgroundImage.DOColor(onColor, tweenTime);
        else
            bacgkgroundImage.DOColor(offColor, tweenTime);

    }

    private void MovedIndicator(bool value) {

        if (value)
            toggleIndicator.DOAnchorPosX(onx, tweenTime);
        else
            toggleIndicator.DOAnchorPosX(offx, tweenTime);
    }


    private void ToggleGyro(bool value) {



        if (value)
        {
            StartCoroutine(enableGyro);
            StopCoroutine(disableGyro);
        }
        else {   
            StartCoroutine(disableGyro);
            StopCoroutine(enableGyro);
        }
    
    
    }



    // Update is called once per frame
    void Update()
    {



    }
}
