using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class OyuncuHareketManager : MonoBehaviour
{
    bool hareketlimi;

    Vector3 hangiYon;
    Quaternion donusYonu;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void HareketEt(Vector3 hedefPos,float gecikmeZamani = 0.25f)
    {
        if (!hareketlimi)
        {
            StartCoroutine(hareketRoutine(hedefPos, gecikmeZamani));
        }
        
    }
    
    IEnumerator hareketRoutine(Vector3 hedefPos, float gecikmezamani = 0.25f)
    {
        hareketlimi = true;

        //Hareket esnasinda rotation karakterin rotation islemi
        hangiYon = new Vector3(hedefPos.x - transform.position.x, transform.position.y, hedefPos.z - this.transform.position.z);
        donusYonu = Quaternion.LookRotation(hangiYon); //hangiyön vektörüne döndürmeye yarar
        transform.DORotateQuaternion(donusYonu, 0.2f);
        anim.SetBool("hareketEtsinmi", true);

        yield return new WaitForSeconds(.2f);
        this.transform.DOMove(hedefPos, gecikmezamani);

        while (Vector3.Distance(hedefPos, this.transform.position) > 0.01f) 
        {
            yield return null; // break
        }
        //Yürüme bitme aný
        anim.SetBool("hareketEtsinmi", false);
        donusYonu = Quaternion.LookRotation(Vector3.zero);
        transform.DORotateQuaternion(donusYonu, 0.2f);
        this.transform.position = hedefPos;
        hareketlimi = false;
    }

    public void OyuncuHataYapti()
    {
        anim.SetBool("hataYapti", true);
    }

    public void OyuncuGeriGelsin()
    {
        
        anim.SetBool("hataYapti", false);
        Invoke("OyuncuPatlamadanSonraGelmeDelay", 0.75f);


    }

    public void OyuncuPatlamadanSonraGelmeDelay()
    {
        this.transform.position = Vector3.zero;
    }

}