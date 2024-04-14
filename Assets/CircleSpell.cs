using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleSpell : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject prefab;

    [Header("Target")]
    public EnemyController _hero;

    [Header("AmountOfSummons")]
    public int summons;

    // Start is called before the first frame update
    void Start()
    {
        _hero = FindObjectOfType<EnemyController>();
        StartCoroutine(SpawnCircles());
    }

    private void SummonCircle()
    {
        GameObject circle = Instantiate(prefab);
        circle.transform.position = new Vector2(_hero.transform.position.x + randomInt(), _hero.transform.position.y + randomInt());
    }

    private int randomInt()
    {
        int randomNumber = Random.Range(-1, 1);

        return randomNumber;
    }

    private IEnumerator SpawnCircles()
    {
        for(int i = 0; i < summons; i++)
        {
            if (_hero != null)
            {
                SummonCircle();
                yield return new WaitForSeconds(0.3f);
            } else
            {
                Destroy(gameObject);
            }
        }
        Destroy(gameObject);
    }
}
