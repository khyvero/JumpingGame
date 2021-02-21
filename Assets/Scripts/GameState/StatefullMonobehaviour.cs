using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatefullMonobehaviour : MonoBehaviour
{

    protected string _state;

    public delegate void StateSubscriber();
    private Dictionary<string, List<StateSubscriber>> _stateSubscribers = new Dictionary<string, List<StateSubscriber>>();

    // Start is called before the first frame update
    protected void Start()
    {
        this.SetState(GetInitialState());

        DoStart();
    }

    // Update is called once per frame
    protected void Update()
    {
        HandleStateSubscriptions();

        DoUpdate();
    }

    protected abstract void DoStart();

    protected abstract void DoUpdate();

    protected abstract string GetInitialState();

    protected void SetState(string _state)
    {
        this._state = _state;
    }


    public void SubscribeToState(string state, StateSubscriber stateSubscriber)
    {
        if (_stateSubscribers.ContainsKey(state))
        {
            _stateSubscribers[state].Add(stateSubscriber);
        }
        else
        {
            List<StateSubscriber> stateSubscribers = new List<StateSubscriber>();
            stateSubscribers.Add(stateSubscriber);
            _stateSubscribers.Add(state, stateSubscribers);
        }
    }


    private void HandleStateSubscriptions()
    {

        if (_stateSubscribers.ContainsKey(_state))
        {

            foreach(StateSubscriber stateSubscriber in _stateSubscribers[_state])
            {
                stateSubscriber.Invoke();
            }

        }

    }



}
