using UnityEngine;

public class RandomCubesSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform _cube;
    public Transform _plane;
    public int _cubesCount = 100;
    private float _randomHeight;
    private float _width;
    private float _dept;

    private void Awake()
    {
        _width = _cube.transform.localScale.x;
        _dept = _cube.transform.localScale.z;
        SpawnCubes();
    }

    private void SpawnCubes()
    {
        for (int i = 0; i < _cubesCount; i++)
        {
            for (int j = 0; j < _cubesCount; j++)
            {
                float randomWidth = Random.Range(_width, _width + 1);
                _randomHeight = Random.Range(3, 6);
                Vector3 pos = new Vector3(-_cubesCount + _width * i * randomWidth, 0, _cubesCount * 2 + _dept * j * randomWidth);
                Transform obj = Instantiate(_cube, pos, Quaternion.identity, transform);
                obj.localScale = new Vector3(_width, _randomHeight, _dept);
            }
        }
    }
}
