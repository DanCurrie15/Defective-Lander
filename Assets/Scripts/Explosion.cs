using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public List<GameObject> Explosions = new List<GameObject>();

    void Start()
    {
        StartCoroutine("Explode");
    }

    IEnumerator Explode()
    {
        SoundManager.Instance.PlaySoundEffect(SoundEffect.ShipExplosion);
        Explosions[0].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Explosions[0].SetActive(false);
        Explosions[1].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Explosions[1].SetActive(false);
        Explosions[2].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
    }
}
