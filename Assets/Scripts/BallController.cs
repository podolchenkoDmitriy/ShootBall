using System.Collections;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody _rb;
    private Transform _playerBall;

    private void Awake()
    {
        Application.targetFrameRate = 300;
        _rb = GetComponent<Rigidbody>();
        gameObject.SetActive(false);
        _playerBall = FindObjectOfType<SizeController>().transform;


    }

    private IEnumerator CountDistance()
    {
        while (gameObject.activeInHierarchy)
        {
            if (Vector3.Distance(transform.position, _playerBall.position) > 50f)
            {
                Default();
                SizeController._canShoot = true;
            }
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator DefaultState()
    {
        yield return new WaitForFixedUpdate();
        gameObject.SetActive(false);
        transform.rotation = Quaternion.identity;
        transform.localScale = Vector3.one * 0.1f;
        _rb.ResetInertiaTensor();
        _rb.velocity = Vector3.zero;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<ObstacleController>())
        {
            Default();
        }
    }
    private void OnEnable()
    {
        transform.position = _playerBall.position;

        _rb.AddForce(_playerBall.transform.forward * 50f, ForceMode.Impulse);
        StartCoroutine(CountDistance());
    }

    private void Default()
    {
        StartCoroutine(DefaultState());
    }
}
