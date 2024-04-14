using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour
{
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.localScale += new Vector3(_player._currentSacrificeRadius, _player._currentSacrificeRadius);
    }
}
