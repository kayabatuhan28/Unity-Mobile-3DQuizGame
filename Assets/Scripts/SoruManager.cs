using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //List
using TMPro;
using DG.Tweening;
using UnityEngine.UI;


public class SoruManager : MonoBehaviour
{
    [SerializeField]
    List<SoruItem> sorularList; 

    [SerializeField]
    TMP_Text soruTxt;

    [SerializeField]
    GameObject cevapPrefab;

    [SerializeField]
    Transform cevapContainer; 

    InputManager inputManager;

    GeriSay�mManager geriSay�mManager;
    int kacinciSoru;

    int cevapAdet;

    string[] secenekler = { "A-)", "B-)", "C-)", "D-)" };

    GameManager gameManager;

    private void Awake()
    {
        gameManager = Object.FindObjectOfType<GameManager>();
        inputManager = Object.FindObjectOfType<InputManager>();
        geriSay�mManager = Object.FindObjectOfType<GeriSay�mManager>();
    }

    private void Start()
    {
        sorularList = sorularList.OrderBy(i => Random.value).ToList(); //Listenin elemanlar�n� kar��t�r tekrar ayn� listeye ekle
       
    }

    public void SorulariYazdir()
    {      
        cevapAdet = 0; 

        soruTxt.text = sorularList[kacinciSoru].soru;
        soruTxt.GetComponent<CanvasGroup>().alpha = 0f;
        soruTxt.GetComponent<RectTransform>().localScale = Vector3.zero;
        CevaplariOlustur();
        geriSay�mManager.degerSetle();
        geriSay�mManager.StartCoroutine(geriSay�mManager.geriSayimEnumerator);
    }

    void CevaplariOlustur()
    {
        GameObject[] silinecekCevaplar = GameObject.FindGameObjectsWithTag("cevapTag");

        if(silinecekCevaplar.Length >= 0)
        {
            for (int i = 0; i < silinecekCevaplar.Length; i++)
            {
                DestroyImmediate(silinecekCevaplar[i]); // hemen yoket
            }
        }


        for (int i = 0; i < 4 ; i++)
        {
            GameObject cevapObje = Instantiate(cevapPrefab);                    
            cevapObje.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = sorularList[kacinciSoru].cevaplar[i].ToString();
            cevapObje.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = secenekler[i];
            cevapObje.transform.SetParent(cevapContainer);

            cevapObje.GetComponent<Transform>().localScale = Vector3.zero;
        }

        gameManager.dogruCevap = sorularList[kacinciSoru].dogruCevap;
        StartCoroutine(CevaplariAcRoutine());
       
    }


    IEnumerator CevaplariAcRoutine()
    {
        yield return new WaitForSeconds(.5f);
        soruTxt.GetComponent<CanvasGroup>().DOFade(1, .3f);
        soruTxt.GetComponent<RectTransform>().DOScale(1, .3f);
        yield return new WaitForSeconds(.4f);
        while(cevapAdet < 4)
        {
            cevapContainer.GetChild(cevapAdet).DOScale(1, .2f);
            yield return new WaitForSeconds(0.2f);
            cevapAdet++;
        }

        kacinciSoru++;
        gameManager.soruCevaplansinmi = true;
        
    }

   

}
