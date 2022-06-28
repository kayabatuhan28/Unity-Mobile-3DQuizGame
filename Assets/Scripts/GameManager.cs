using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject soruPaneli;

    [SerializeField]
    GameObject dogruIcon, yanlisIcon;

    SoruManager soruManager;

    OyuncuHareketManager oyuncuHareketManager;

    GeriSayýmManager geriSayýmManager;

    SesManager sesManager;

    [SerializeField]
    GameObject robot_1, robot_2;

    [SerializeField]
    TMP_Text skorTxt;

    [HideInInspector]
    public int puan;

    [SerializeField]
    GameObject dogruSonuc, yanlisSonuc;

    public bool soruCevaplansinmi;

    public string dogruCevap;

    public int kalanHak;

    int dogruAdet;

    public string sahneAdi;
    private void Awake()
    {
        oyuncuHareketManager = Object.FindObjectOfType<OyuncuHareketManager>();
        soruManager = Object.FindObjectOfType<SoruManager>();
        sesManager = Object.FindObjectOfType<SesManager>();
        geriSayýmManager = Object.FindObjectOfType<GeriSayýmManager>();

    }
    private void Start()
    {
        StartCoroutine(OyunuAcRoutine());
        kalanHak = 2;
        dogruAdet = 0;
    }
    IEnumerator OyunuAcRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        sesManager.BaslaSesiCikar();
        soruPaneli.GetComponent<RectTransform>().DOAnchorPosX(30, 1f);

        yield return new WaitForSeconds(1.1f);
        soruManager.SorulariYazdir();
    }

    public void SonucuKontrolEt(string gelenCevap)
    {
        if(gelenCevap == dogruCevap)
        {
            //sonuc dogru 
            puan++;
            print(puan);
            dogruAdet++;
            sesManager.DogruSesiCikar();
            
            
            if(dogruAdet >= 19)
            {
                Invoke("DogruSonucGoster", 1.5f);
                Destroy(geriSayýmManager._timerImg);
                Destroy(geriSayýmManager._timerTxt);
                Destroy(geriSayýmManager._timerbackgroundImg);
            }
            else
            {
                soruManager.SorulariYazdir();
            }
            DogruIconuAktiflestir();
           
        }
        else
        {
            //sonuc yanlis
            BitisSkorEkrani();
            geriSayýmManager.StopCoroutine(geriSayýmManager.geriSayimEnumerator);
            sesManager.YanlisSesiCikar();
            YanlisIconuAktiflestir();
            StartCoroutine(OyuncuHataYaptiGeriGeldi());
            

        }

       
    }

    

    public void BitisSkorEkrani()
    {
        skorTxt.text = puan.ToString();
    }

    void DogruIconuAktiflestir()
    {
        dogruIcon.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
        Invoke("DogruIconuPasiflestir", .8f); 
    }

    void YanlisIconuAktiflestir()
    {
        yanlisIcon.GetComponent<CanvasGroup>().DOFade(1, 0.3f);
        Invoke("YanlisIconuPasiflestir", .8f);
    }

    void DogruIconuPasiflestir()
    {
        dogruIcon.GetComponent<CanvasGroup>().DOFade(0, 0.3f);
    }

    void YanlisIconuPasiflestir()
    {
        yanlisIcon.GetComponent<CanvasGroup>().DOFade(0, 0.3f);
    }

    public IEnumerator OyuncuHataYaptiGeriGeldi()
    {
        yield return new WaitForSeconds(1f);
        oyuncuHareketManager.OyuncuHataYapti();
        yield return new WaitForSeconds(1.4f);

        kalanHak--;
        hakKaybet();
        if(kalanHak > 0)
        {
            oyuncuHareketManager.OyuncuGeriGelsin();
            yield return new WaitForSeconds(1f);
            soruManager.SorulariYazdir();
            
        }
        else
        {
            //Oyun bitti
            Destroy(geriSayýmManager._timerImg);
            Destroy(geriSayýmManager._timerTxt);
            Destroy(geriSayýmManager._timerbackgroundImg);
            YanlisSonucGoster();
        }

        


    }

    public void hakKaybet()
    {
        if(kalanHak == 1)
        {
            robot_2.SetActive(false);
            robot_1.SetActive(true);
        }
        else if(kalanHak == 0)
        {
            
            robot_2.SetActive(false);
            robot_1.SetActive(false);
        }
    }

    public void DogruSonucGoster()
    {
        sesManager.BitisSesiCikar();

        soruPaneli.GetComponent<RectTransform>().DOAnchorPosX(-1100f, 1f);
        dogruSonuc.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        dogruSonuc.GetComponent<RectTransform>().DOScaleX(1.8f, .5f).SetEase(Ease.OutBack);
        dogruSonuc.GetComponent<RectTransform>().DOScaleY(1.3f, .5f).SetEase(Ease.OutBack);
        dogruSonuc.GetComponent<RectTransform>().DOScaleZ(1.5f, .5f).SetEase(Ease.OutBack);
    }

    public void YanlisSonucGoster()
    {
        sesManager.BitisSesiCikar();
        soruPaneli.GetComponent<RectTransform>().DOAnchorPosX(-1100f, 1f);
        yanlisSonuc.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        yanlisSonuc.GetComponent<RectTransform>().DOScaleX(1f, .5f).SetEase(Ease.OutBack);
        yanlisSonuc.GetComponent<RectTransform>().DOScaleY(1f, .5f).SetEase(Ease.OutBack);
        yanlisSonuc.GetComponent<RectTransform>().DOScaleZ(1f, .5f).SetEase(Ease.OutBack);
        
    }

    public void YenidenOynaButton()
    {
        SceneManager.LoadScene(sahneAdi);
    }

    public void OyundanCik()
    {
        Application.Quit();
    }

}
