using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PicesScript : MonoBehaviour
{
    private Vector3 RightPosition;
    private Vector3 StartPosition;
    public float snapDistance = 2.0f;
    public bool inRightPosition = false;
    public bool selected = false;
    public float moveSpeed = 15f;
    private bool helper = true;
    //private float startTime;
    //private float duration = 5f;

    //public AudioSource sourceGood;
    public AudioSource sourceBad;

    //UnityEvent musicStartGoodEvent;
    //UnityEvent musicStopGoodEvent;
    UnityEvent musicStartBadEvent;
    UnityEvent musicStopBadEvent;

    // Start is called before the first frame update
    void Start()
    {
        RightPosition = transform.position;
        transform.position = new Vector3(Random.Range(1, 8), Random.Range(-4, 4), 0);
        StartPosition = transform.position;

        //if (musicStartGoodEvent == null)
        //    musicStartGoodEvent = new UnityEvent();

        //if (musicStopGoodEvent == null)
        //    musicStopGoodEvent = new UnityEvent();

        //musicStartGoodEvent.AddListener(MusicStartGood);
        //musicStopGoodEvent.AddListener(MusicStopGood);

        if (musicStartBadEvent == null)
            musicStartBadEvent = new UnityEvent();

        if (musicStopBadEvent == null)
            musicStopBadEvent = new UnityEvent();

        musicStartBadEvent.AddListener(MusicStartBad);
        musicStopBadEvent.AddListener(MusicStopBad);
    }

    private void Awake()
    {
        sourceBad = GetComponent<AudioSource>();
        //sourceGood = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, RightPosition) < snapDistance && helper)
        {
            if (!selected && helper)
            {
                transform.position = RightPosition;
                inRightPosition = true;
                //musicStartGoodEvent.Invoke();
                //startTime = Time.time;
                VictoryCondition();
            }
        }
        else
        {
            if (!selected)
            {
                transform.position = Vector3.Lerp(transform.position, StartPosition, Time.deltaTime * moveSpeed);
                musicStartBadEvent.Invoke();
                helper = false;
            }
        }
        if (transform.position == StartPosition && !helper)
        {
            helper = true;
            musicStopBadEvent.Invoke();
        }
        //if(startTime + duration >= Time.time)
        //{
        //    musicStopGoodEvent.Invoke();
        //}
    }
    void VictoryCondition()
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("piece");
        foreach (GameObject piece in pieces)
        {
            if (!piece.GetComponent<PicesScript>().inRightPosition)
            {
                return;
            }
        }
        GameObject winBanner = GameObject.FindGameObjectWithTag("win");
        winBanner.GetComponent<SpriteRenderer>().color = new Vector4(255,255,255,1);
    }

    //void MusicStartGood()
    //{
    //    if (!sourceGood.isPlaying)
    //    {
    //        sourceGood.Play();
    //    }
    //}

    //void MusicStopGood()
    //{
    //    sourceGood.Stop();
    //}

    void MusicStartBad()
    {
        if (!sourceBad.isPlaying)
        {
            sourceBad.Play();
        }
    }

    void MusicStopBad()
    {
        sourceBad.Stop();
    }
}
