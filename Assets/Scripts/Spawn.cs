using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject Enemy;
    public int DelaySpawn = 2;
    public int Count;

    [SerializeField] private Transform[] _spawnPoint;

    private IEnumerator _spawnEnemy;

    private void Start()
    {
        var childrenTransform = gameObject.GetComponentInChildren<Transform>();
        _spawnPoint = new Transform[childrenTransform.childCount];

        for (int i = 0; i < childrenTransform.childCount; i++)
        {
            _spawnPoint[i] = childrenTransform.GetChild(i);
        }

        _spawnEnemy = SpawnEnemy();
        StartCoroutine(_spawnEnemy);
    }

    private IEnumerator SpawnEnemy()
    {
        int indent = 0;

        while (Count > 0)
        {
            for (int index = 0; index < _spawnPoint.Length; index++)
            {
                yield return new WaitForSeconds(DelaySpawn);

                CreateEnemy(index, indent);
                Count--;

                if (index == _spawnPoint.Length - 1)
                {
                    indent++;
                }
            }
        }

        if (_spawnEnemy != null)
        {
            StopCoroutine(_spawnEnemy);
        }
    }

    private void CreateEnemy(int index, int indent)
    {
        Instantiate(Enemy, new Vector3(_spawnPoint[index].localPosition.x, _spawnPoint[index].localPosition.y, _spawnPoint[index].localPosition.z + indent), new Quaternion(0, 180, 0, 0));
    }
}
