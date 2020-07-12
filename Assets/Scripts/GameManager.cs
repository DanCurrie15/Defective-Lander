using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public State rotateRightEnabled { get; private set; } = State.Enabled;
    public State rotateLeftEnabled { get; private set; } = State.Enabled;
    public State thrustEnabled { get; private set; } = State.Enabled;
    public State speedTextEnabled { get; private set; } = State.Enabled;
    public State distanceTextEnabled { get; private set; } = State.Enabled;
    public State fuelGaugeEnabled { get; private set; } = State.Enabled;

    private float rateOfBreakDowns = 2f;
    private float nextBreakDown;
    public bool gameOn { get; private set; } = true;
    public float fuel = 100f;

    private int oldRandFunctionFail = 99;
    private int oldRandDisplayFail = 99;

    void Start()
    {
        nextBreakDown = Time.time + rateOfBreakDowns;
        fuel = 100f;
    }

    void Update()
    {
        if (Time.time > nextBreakDown && gameOn)
        {
            nextBreakDown = Time.time + rateOfBreakDowns;
            int randFunctionFail = Random.Range(0, 3);
            int randDisplayFail = Random.Range(0, 3);
            while(oldRandFunctionFail == randFunctionFail)
            {
                randFunctionFail = Random.Range(0, 3);
            }
            while (oldRandDisplayFail == randDisplayFail)
            {
                randDisplayFail = Random.Range(0, 3);
            }

            if (randFunctionFail == 0)
            {
                thrustEnabled = Random.Range(0,2) == 0 ? State.Disabled : State.Failing;
                rotateLeftEnabled = State.Enabled;
                rotateRightEnabled = State.Enabled;
            }
            if (randFunctionFail == 1)
            {
                rotateLeftEnabled = Random.Range(0, 2) == 0 ? State.Disabled : State.Failing;
                rotateRightEnabled = State.Enabled;
                thrustEnabled = State.Enabled;
            }
            if (randFunctionFail == 2)
            {
                rotateRightEnabled = Random.Range(0, 2) == 0 ? State.Disabled : State.Failing;
                thrustEnabled = State.Enabled;
                rotateLeftEnabled = State.Enabled;
            }
            if (randDisplayFail == 0)
            {
                speedTextEnabled = State.Disabled;
                distanceTextEnabled = State.Enabled;
                fuelGaugeEnabled = State.Enabled;
            }
            if (randDisplayFail == 1)
            {
                distanceTextEnabled = State.Disabled;
                speedTextEnabled = State.Enabled;
                fuelGaugeEnabled = State.Enabled;
            }
            if (randDisplayFail == 2)
            {
                fuelGaugeEnabled = State.Disabled;
                speedTextEnabled = State.Enabled;
                distanceTextEnabled = State.Enabled;
            }
            oldRandFunctionFail = randFunctionFail;
            oldRandDisplayFail = randDisplayFail;
            UIManager.Instance.UpdateStateImage();
        }

        if (fuel < 0)
        {
            OutOfFuel();
        }
    }

    public void WinGame()
    {
        Debug.Log("Win Game");
        gameOn = false;
        UIManager.Instance.WinGame();
    }

    public void LoseGame()
    {
        Debug.Log("Lose Game");        
        gameOn = false;
        UIManager.Instance.LoseGame();
    }

    public void OutOfFuel()
    {
        gameOn = false;
        UIManager.Instance.OutOfFuel();
    }
}

public enum State
{
    Enabled,
    Failing,
    Disabled
}

