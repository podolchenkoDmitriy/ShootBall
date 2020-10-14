using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialObs : MonoBehaviour
{
    // Start is called before the first frame update
    public Material material;
    private Material startMat;
    private void Start()
    {
        startMat = GetComponent<Renderer>().material;
    }
    // Update is called once per frame
    public void MaterialChange()
    {
        StartCoroutine(ChangeMat());
    }
    private IEnumerator ChangeMat()
    {
        yield return new WaitForFixedUpdate();

        Renderer rend = gameObject.GetComponent<Renderer>();
        float elapsed = 0;
        float timeToDetect = 0.5f;
        while (elapsed < timeToDetect)
        {
            elapsed += Time.fixedDeltaTime;
            rend.material.Lerp(startMat, material, Time.fixedDeltaTime * 10f);

            yield return new WaitForFixedUpdate();

        }

        yield return new WaitForFixedUpdate();
        gameObject.SetActive(false);

        SizeController._canShoot = true;
    }
}
