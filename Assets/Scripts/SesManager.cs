using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SesManager : MonoBehaviour
{
    [SerializeField]
    AudioSource buton_SFX, dogru_SFX, finish_SFX, basla_SFX, yanlis_SFX;

   public void ButonSesiCikar()
    {
        buton_SFX.Play();
    }

    public void DogruSesiCikar()
    {
        dogru_SFX.Play();
    }

    public void YanlisSesiCikar()
    {
        yanlis_SFX.Play();
    }

    public void BitisSesiCikar()
    {
        finish_SFX.Play();
    }

    public void BaslaSesiCikar()
    {
        basla_SFX.Play();
    }


}
