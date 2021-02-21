using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{

    private static CountDownTimer Instance;

    private Dictionary<string, TimerWrapper> Timers = new Dictionary<string, TimerWrapper>();


    private bool stopExecution=false;

    private void Start()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }

    public void CountDown(float timeRemaining,  DoOnTime doOntimme)
    {

        string id = System.Guid.NewGuid().ToString();

        this.CountDown(id, timeRemaining, (t)=> { }, doOntimme);
    }



    public void CountDown(float timeRemaining, PublishTimeRemaining publishTimeRemaining , DoOnTime doOntimme)
    {

        string id = System.Guid.NewGuid().ToString() ;

        this.CountDown(id, timeRemaining, publishTimeRemaining, doOntimme);
    }


    public void CountDown(string id, float timeRemaining,  DoOnTime doOntimme)
    {

        if (Timers.ContainsKey(id))
        {
            return;
        }

        TimerWrapper wrapper = new TimerWrapper(id, timeRemaining, (t) => { }, doOntimme);

        Timers.Add(id, wrapper);

    }



    public void CountDown(string id, float timeRemaining, PublishTimeRemaining publishTimeRemaining, DoOnTime doOntimme)
    {

        if (Timers.ContainsKey(id))
        {
            return;
        }

        TimerWrapper wrapper = new TimerWrapper(id, timeRemaining, publishTimeRemaining, doOntimme);

        Timers.Add(id, wrapper);

    }

    public void AddTime(string id, float timeToAdd)
    {
        if (!Timers.ContainsKey(id))
        {
            return;
        }

        Timers[id].UpdateTime(-timeToAdd);
    }

    public void ClearAllTimers()
    {
        this.stopExecution = true;
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        List<string> dones = new List<string>();

        foreach(string timerKey in Timers.Keys)
        {
            TimerWrapper wrapper = Timers[timerKey];
            if (stopExecution)
            {
                dones.Add(timerKey);
            }
            else if (wrapper.TimeIsUp())
            {
                if (!stopExecution)
                {
                    wrapper.DoTask();
                }
                dones.Add(timerKey);
            }
            else
            {
                wrapper.UpdateTime(deltaTime);
            }
        }

        foreach (string done in dones)
        {
            Timers.Remove(done);
        }
    }


    public class TimerWrapper
    {
        private DoOnTime DoOnTime;
        private PublishTimeRemaining publishTimeRemaining;
        private float timeRemaining;
        private bool Done;
        private string id;

        public TimerWrapper(string id ,float timeRemaining, PublishTimeRemaining publishTimeRemaining, DoOnTime doOnTime)
        {
            this.timeRemaining = timeRemaining;
            this.DoOnTime = doOnTime;
            this.id = id;

            if(publishTimeRemaining == null)
            {
                this.publishTimeRemaining = (t) => { Debug.Log("Time Remaining For Timer:" + id + " is: " + t); };
            }
            else
            {
                this.publishTimeRemaining = publishTimeRemaining;
            }
        }

        public void UpdateTime(float DeltaTime)
        {
            timeRemaining -= DeltaTime;
            publishTimeRemaining.Invoke(timeRemaining);
        }

        public bool TimeIsUp()
        {
            return timeRemaining <= 0;
        }

        public void DoTask()
        {
            if (!Done)
            {
                Debug.Log("Doing timer job");
                DoOnTime.Invoke();
                Done = true;
            }

        }
    }

    
    public static CountDownTimer GetInstance()
    {
        return Instance;
    }


    public delegate void DoOnTime();
    public delegate void PublishTimeRemaining(float timeRemaining);
    
}
