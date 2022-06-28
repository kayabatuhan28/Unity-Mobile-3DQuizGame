using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    GameObject Oyuncu;

    GameManager gameManager;

    SesManager sesManager;

    GeriSayýmManager geriSayimManager;

    public string ad;// meshlere A B C D dogruseçeneði algýlatmak için tanýmladýk bunla dogrucevaptan gelen stringi karþýlaþtýrdýk

   

    private void Awake()
    {
        gameManager = Object.FindObjectOfType<GameManager>();
        sesManager = Object.FindObjectOfType<SesManager>();
        Oyuncu = GameObject.Find("Oyuncu"); // Sahnedeki Oyuncu isimli nesneyi bul ve bunu gameobject oyuncu içine at
        geriSayimManager = Object.FindObjectOfType<GeriSayýmManager>();
    }

    private void OnMouseDown() // Mouseye basýldýðý anda gerçekleþen
    {
        if (!gameManager.soruCevaplansinmi) // false ise return yapar bu fonksiyon çalýþmaz
        {
            return;
        }

        // z ileri yön mousenin z si oyuncunun z sinden büyük olduðu sürece koþar yani geri dönüþü engelledik,> dedigimiz için abcd ayný z de olduðu için yanageçiþide engelleriz >= deseydik yanada geçerdi
        //this.transform.position.z < Oyuncu.transform.position.z+2 ile oyuncunun 2 eksen ileri gitmesini engelledik her 4 lü seçeneðin z si 1  2 3 .. gidecke þekilde ayarladýk
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
