using System.Collections;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    // Start is called before the first frame update
    private float _radius = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<BallController>())
        {
            _radius = collision.collider.transform.localScale.magnitude * 2f;

            GetPoisoned();
        }

    }

    private IEnumerator Posioned()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider item in hitColliders)
        {
            yield return new WaitForFixedUpdate();
            if (item.GetComponent<ObstacleController>())
            {
                item.GetComponent<ChangeMaterialObs>().MaterialChange();

            }

        }
    }

    private void GetPoisoned()
    {
        StartCoroutine(Posioned());

    }


}
