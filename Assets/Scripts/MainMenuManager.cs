using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class MainMenuManager : MonoBehaviour
{

    [SerializeField]
    GameObject gameTitle, baslaBtn, cikisBtn;

    public string sahneAdi;

    [SerializeField]
    GameObject panel;
    bool basildi;

    private void Start()
    {
       StartCoroutine(componentleriAc());    
    }

    public void OyunaBasla()
    {
        if(basildi == false)
        {
            basildi = true;
            StartCoroutine(panelAc());
        }
        
       
    }

    public void OyundanCik()
    {
        Application.Quit();
    }

    IEnumerator componentleriAc()
    {
        yield return new WaitForSeconds(0.1f);
        gameTitle.GetComponent<CanvasGroup>().DOFade(1, 0.4f);
        yield return new WaitForSeconds(0.4f);
        baslaBtn.GetComponent<CanvasGroup>().DOFade(1, 0.4f);
        yield return new WaitForSeconds(0.4f);
        cikisBtn.GetComponent<CanvasGroup>().DOFade(1, 0.4f);

    }

    IEnumerator panelAc()
    {
        yield return new WaitForSeconds(0.1f);
       
        panel.GetComponent<CanvasGroup>().DOFade(1, 1f).SetEase(Ease.InFlash);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sahneAdi);
    }
}
