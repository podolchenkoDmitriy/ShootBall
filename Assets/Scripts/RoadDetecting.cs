using UnityEngine;

public class RoadDetecting : MonoBehaviour
{
    public static bool _canWin = false;
    private float _elapsed = 0;
    private const float _timeToDetect = 1f;
    private void OnTriggerStay(Collider other)
    {
        if (!other.GetComponent<ObstacleController>())
        {
            _elapsed += Time.fixedDeltaTime;
            if (_elapsed > _timeToDetect)
            {
                _canWin = true;

            }
        }
        else
        {
            _elapsed = 0;
            _canWin = false;

        }
    }

}
