using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeriSayımManager : MonoBehaviour
{
    [SerializeField]
    public Image _timerImg,_timerbackgroundImg;

    [SerializeField]
    public TMP_Text _timerTxt;

    InputManager inputManager;

    float _currentTime, _duration;

    GameManager gameManager;
    public IEnumerator geriSayimEnumerator;

    private void Awake()
    {
        gameManager = Object.FindObjectOfType<GameManager>();
        inputManager = Object.FindObjectOfType<InputManager>();
        geriSayimEnumerator = updateTime();
        
    }

    

    public void degerSetle()
    {
        _duration = 20f;
        _currentTime = _duration;
        _timerTxt.text = _currentTime.ToString();
    }

    private void Update()
    {
        
    }

    IEnumerator updateTime()
    {
        while (_currentTime >= 0)
        {
            
            
            _timerImg.fillAmount = Mathf.InverseLerp(0, _duration, _currentTime); 
            _timerTxt.text = _currentTime.ToString();
             yield return new WaitForSeconds(1f);
            _currentTime--;
                   

            if(_currentTime <= 0)
            {
                gameManager.StartCoroutine(gameManager.OyuncuHataYaptiGeriGeldi());
                          
            }

        }

        yield return null;
    }
}
