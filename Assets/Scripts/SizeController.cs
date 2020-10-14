using System.Collections;
using UnityEngine;

public class SizeController : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform _playerBall;
    public float _increaseConst = 0.01f;
    public Transform _ballPrefab;
    public float _aimDistance = 50f;
    private Transform _winZone;
    private Transform _camera;
    private bool _win = false;
    private Ray _crossRay;
    private bool _canIncrease = false;
    private bool _loose = false;
    private Vector3 _startSize;
    private Renderer rend;
    public Material material;
    public static bool _canShoot = true;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        _playerBall = Instantiate(_ballPrefab, transform.position, Quaternion.identity);
        _winZone = FindObjectOfType<WinZone>().transform;
        _camera = Camera.main.transform;

        StartCoroutine(IncreaseSizeRoutine());
    }


    // Update is called once per frame
    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && _canShoot && !_canIncrease)
        {
            _startSize = transform.localScale;
            _canIncrease = true;
        }
        if (Input.GetMouseButtonUp(0) && _canIncrease)
        {
            _canShoot = false;
            _canIncrease = false;
            _playerBall.gameObject.SetActive(true);
            localsize = Vector3.zero;

        }


    }


    private Ray Hit()
    {
        Vector3 shootDir = new Vector3(Input.mousePosition.normalized.x - 0.5f, 0, 1);
        Ray hit = new Ray(transform.position, shootDir * _aimDistance);
        Debug.DrawRay(hit.origin, hit.direction * _aimDistance, Color.green);
        _crossRay = hit;

        return _crossRay;
    }

    private IEnumerator StartMoveToWin()
    {
        while (Vector3.Distance(transform.position, _winZone.position) > 10f)
        {
            yield return new WaitForFixedUpdate();
            Vector3 dir = new Vector3(_winZone.position.x, 0, _winZone.position.z) - transform.position;
            transform.Translate(dir.normalized * Time.fixedDeltaTime * 10f);
            _camera.position = Vector3.MoveTowards(_camera.position, transform.position, 0.1f);
        }
        UIInstance.instance.winPanel.gameObject.SetActive(true);
    }

    private Vector3 localsize;
    private IEnumerator IncreaseSizeRoutine()
    {
        while (!_win)
        {
            Hit();
            Physics.Raycast(_crossRay, out RaycastHit hit);

            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<WinZone>())
                {
                    if (RoadDetecting._canWin && !_loose)
                    {
                        _win = true;
                        StartCoroutine(StartMoveToWin());
                    }


                }
            }

            transform.LookAt(_crossRay.GetPoint(_aimDistance));
            if (_canIncrease)
            {

                Vector3 size = Vector3.one * _increaseConst;
                localsize += size;

                yield return new WaitForFixedUpdate();

                if (_startSize.magnitude > localsize.magnitude * 2f)
                {


                    if (transform != null)
                    {


                        transform.localScale -= size;
                        rend.material.Lerp(rend.material, material, Time.fixedDeltaTime * 0.1f);
                        transform.position -= new Vector3(0, _increaseConst / 2, -_increaseConst * 5f);
                        if (transform.localScale.magnitude < 1.5 && !_loose)
                        {
                            _loose = true;
                            UIInstance.instance.losePanel.gameObject.SetActive(true);
                        }
                    }

                    yield return new WaitForFixedUpdate();

                    _playerBall.localScale += size;
                }
            }
            else
            {
                yield return new WaitForFixedUpdate();

            }

        }
    }
}
