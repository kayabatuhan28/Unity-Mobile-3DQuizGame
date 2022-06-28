using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    GameObject Oyuncu;

    GameManager gameManager;

    SesManager sesManager;

    GeriSay�mManager geriSayimManager;

    public string ad;// meshlere A B C D dogruse�ene�i alg�latmak i�in tan�mlad�k bunla dogrucevaptan gelen stringi kar��la�t�rd�k

   

    private void Awake()
    {
        gameManager = Object.FindObjectOfType<GameManager>();
        sesManager = Object.FindObjectOfType<SesManager>();
        Oyuncu = GameObject.Find("Oyuncu"); // Sahnedeki Oyuncu isimli nesneyi bul ve bunu gameobject oyuncu i�ine at
        geriSayimManager = Object.FindObjectOfType<GeriSay�mManager>();
    }

    private void OnMouseDown() // Mouseye bas�ld��� anda ger�ekle�en
    {
        if (!gameManager.soruCevaplansinmi) // false ise return yapar bu fonksiyon �al��maz
        {
            return;
        }

        // z ileri y�n mousenin z si oyuncunun z sinden b�y�k oldu�u s�rece ko�ar yani geri d�n��� engelledik,> dedigimiz i�in abcd ayn� z de oldu�u i�in yanage�i�ide engelleriz >= deseydik yanada ge�erdi
        //this.transform.position.z < Oyuncu.transform.position.z+2 ile oyuncunun 2 eksen ileri gitmesini engelledik her 4 l� se�ene�in z si 1  2 3 .. gidecke �ekilde ayarlad�k
        if (this.transform.position.z > Oyuncu.transform.position.z && this.transform.position.z < Oyuncu.transform.position.z+2) 
        {
            geriSayimManager.StopCoroutine(geriSayimManager.geriSayimEnumerator);
            print("basildi");
            Vector3 mousePos = this.transform.position;
            Oyuncu.GetComponent<OyuncuHareketManager>().HareketEt(mousePos, 0.5f);
            gameManager.SonucuKontrolEt(ad);
            gameManager.soruCevaplansinmi = false;
            sesManager.ButonSesiCikar();
        }
        
    }
}
