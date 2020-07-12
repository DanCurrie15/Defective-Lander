using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    public Image rotateRightImage;
    public Image rotateLeftImage;
    public Image thrustImage;

    public GameObject gameOverPanel;
    public Text gameOverText;
    public Text speedText;
    public Text fuelText;
    public Text altitudeText;

    public Player player;

    public Transform playerPos;
    public Transform planetPos;

    private void Update()
    {
        if (GameManager.Instance.speedTextEnabled == State.Enabled)
        {
            speedText.text = string.Format("{0:SPEED: 0.0}", player.speed);
        }
        if (GameManager.Instance.speedTextEnabled == State.Disabled)
        {
            speedText.text = "SPEED: ???";
        }

        if (GameManager.Instance.fuelGaugeEnabled == State.Enabled)
        {
            fuelText.text = string.Format("{0:FUEL: 0}", GameManager.Instance.fuel);
        }
        if (GameManager.Instance.fuelGaugeEnabled == State.Disabled)
        {
            fuelText.text = "FUEL: ???";
        }

        if (GameManager.Instance.distanceTextEnabled == State.Enabled)
        {
            altitudeText.text = string.Format("{0:ALTITUDE: 0.0}", Vector3.Distance(playerPos.position, planetPos.position));
        }
        if (GameManager.Instance.distanceTextEnabled == State.Disabled)
        {
            altitudeText.text = "ALTITUDE: ???";
        }
    }

    public void UpdateStateImage()
    {
        if (GameManager.Instance.rotateLeftEnabled == State.Enabled)
        {
            rotateLeftImage.color = Color.green;
        }
        if (GameManager.Instance.rotateLeftEnabled == State.Disabled)
        {
            rotateLeftImage.color = Color.red;
        }
        if (GameManager.Instance.rotateLeftEnabled == State.Failing)
        {
            rotateLeftImage.color = Color.yellow;
        }

        if (GameManager.Instance.rotateRightEnabled == State.Enabled)
        {
            rotateRightImage.color = Color.green;
        }
        if (GameManager.Instance.rotateRightEnabled == State.Disabled)
        {
            rotateRightImage.color = Color.red;
        }
        if (GameManager.Instance.rotateRightEnabled == State.Failing)
        {
            rotateRightImage.color = Color.yellow;
        }

        if (GameManager.Instance.thrustEnabled == State.Enabled)
        {
            thrustImage.color = Color.green;
        }
        if (GameManager.Instance.thrustEnabled == State.Disabled)
        {
            thrustImage.color = Color.red;
        }
        if (GameManager.Instance.thrustEnabled == State.Failing)
        {
            thrustImage.color = Color.yellow;
        }
    }

    public void WinGame()
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = "A WINNER IS YOU!";
    }

    public void LoseGame()
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = "YOU CRASHED :(";
    }

    public void OutOfFuel()
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = "YOU RAN OUT OF FUEL :(";
    }

    public void Retry()
    {
        SceneManager.LoadScene("GAME");
    }
}
