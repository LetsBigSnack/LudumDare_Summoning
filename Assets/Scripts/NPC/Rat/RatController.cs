using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatController : MonoBehaviour
{


    private Rat _rat;
    private Animator _anim;
    
    [Header("NavMash_Setting")]
    private UnityEngine.AI.NavMeshAgent _agentAi;
    public Vector3 pointOfInterest;
    [SerializeField]
    private float wiggleRoom = 1.25f;
    private LocationManager _locationManager;

    [Header("Target")]
    private Transform _player;

    [Header("Sounds")]
    public AudioSource[] sounds;
    private bool _canPlaySound = true;
    

    // Start is called before the first frame update
    void Start()
    {
        
        _rat = GetComponent<Rat>();
        _anim = GetComponentInChildren<Animator>();
        _player = FindObjectOfType<Player>().transform;
        _locationManager = FindObjectOfType<LocationManager>();
        _agentAi = GetComponent<UnityEngine.AI.NavMeshAgent>();
        _agentAi.updateRotation = false;
        _agentAi.updateUpAxis = false;
        _agentAi.avoidancePriority = 0;
        StartCoroutine(MakeSound());
        
        pointOfInterest = _locationManager.GetRandomPointOnNavMesh(this.transform);
    }

    private void Update()
    {
        SetAnimation();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_rat.GetBeingSacrificed())
        {
            _agentAi.ResetPath();
        }
        else
        {
            _agentAi.SetDestination(pointOfInterest);
            TravelToDestination();
        }
    }

    private void PlaySound()
    {
        sounds[Random.Range(0, sounds.Length - 1)].Play();
    }
    
    private void SetAnimation()
    {
        float x = _agentAi.velocity.x;
        float y = _agentAi.velocity.y;
        _anim.SetFloat("moveX", x);
        _anim.SetFloat("moveY", y);
    }

    public void TravelToDestination()
    {
        if (HasReachedDestination())
        {
            SwitchTarget();
            _agentAi.SetDestination(pointOfInterest);
        }
    }
    
    bool HasReachedDestination()
    {
        float distance = Vector3.Distance(transform.position, pointOfInterest);
        return distance < wiggleRoom;
    }

    void SwitchTarget()
    {
        pointOfInterest = _locationManager.GetRandomPointOnNavMesh(this.transform);
    }

    public IEnumerator MakeSound()
    {
        if (_canPlaySound)
        {
            _canPlaySound = false;
            yield return new WaitForSeconds((float)Random.Range(10, 30));
            PlaySound();
            yield return new WaitForSeconds((float)Random.Range(10, 30));
            _canPlaySound = true;
            StartCoroutine(MakeSound());
        }
    }
}
