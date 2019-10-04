using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeKeeper : MonoBehaviour
{
    // Start is called before the first frame update
    public static float time
    {
        get { return instance.mTime; }
    }

    // Variable: timescale
    // Current timescale of the TimeKeeper.
    public static float timescale
    {
        get { return instance.mPaused ? 0 : instance.mTimescale; }
        set { instance.mTimescale = value; }
    }

    //=========================================================================
    private static TimeKeeper instance;

    private float mTime = 0.0f;
    private float mTimescale = 1.0f;
    private float mLastTimestamp = 0.0f;
    private bool mPaused = false;

    //=========================================================================
    void Start()
    {
        if (instance)
            Debug.LogError("Singleton violated");
        instance = this;
        instance.mLastTimestamp = Time.realtimeSinceStartup;
    }

    //=========================================================================
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Pause))
            this.mPaused = !this.mPaused;

        float realDelta = Time.realtimeSinceStartup - this.mLastTimestamp;
        this.mLastTimestamp = Time.realtimeSinceStartup;
        this.mTime += realDelta * timescale;
    }
}
