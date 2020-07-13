using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float thrust;
    public float torque;
    public float speed;

    public GameObject thruster1;
    public GameObject thruster2;

    public List<Transform> StartingLocation = new List<Transform>();

    private const float FUEL_SPENDING = 0.04f;
    private const float FAILING_CONST = 3f;

    private bool thrustingFailingStart = true;

    public GameObject explosion;

    private void Start()
    {
        transform.position = StartingLocation[Random.Range(0, StartingLocation.Count)].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameOn)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && GameManager.Instance.thrustEnabled == State.Enabled)
            {
                SoundManager.Instance.PlaySoundEffect(SoundEffect.Thruster);
            }
            if (Input.GetKey(KeyCode.UpArrow) && GameManager.Instance.thrustEnabled == State.Enabled)
            {
                rb.AddForce(transform.up * thrust);
                GameManager.Instance.fuel -= FUEL_SPENDING;
                FireThrusters();
            }
            if(GameManager.Instance.thrustEnabled == State.Failing)
            {
                if (thrustingFailingStart)
                {
                    SoundManager.Instance.PlaySoundEffect(SoundEffect.Thruster);
                    thrustingFailingStart = false;
                }
                rb.AddForce(transform.up * thrust / FAILING_CONST);
                GameManager.Instance.fuel -= (FUEL_SPENDING / FAILING_CONST);
                FireThrusters();
            }
            if (Input.GetKeyUp(KeyCode.UpArrow) && GameManager.Instance.thrustEnabled != State.Failing)
            {
                StopThrusters();
            }
            if ((GameManager.Instance.thrustEnabled == State.Enabled ||
                GameManager.Instance.thrustEnabled == State.Disabled) &&
                !thrustingFailingStart)
            {
                StopThrusters();
                thrustingFailingStart = true;
            }


            if (Input.GetKey(KeyCode.RightArrow) && GameManager.Instance.rotateRightEnabled == State.Enabled)
            {
                rb.AddTorque(-torque);
                GameManager.Instance.fuel -= FUEL_SPENDING;
            }
            if (GameManager.Instance.rotateRightEnabled == State.Failing)
            {
                rb.AddTorque(-torque / FAILING_CONST);
                GameManager.Instance.fuel -= (FUEL_SPENDING / FAILING_CONST);
            }


            if (Input.GetKey(KeyCode.LeftArrow) && GameManager.Instance.rotateLeftEnabled == State.Enabled)
            {
                rb.AddTorque(torque);
                GameManager.Instance.fuel -= FUEL_SPENDING;
            }
            if (GameManager.Instance.rotateLeftEnabled == State.Failing)
            {
                rb.AddTorque(torque / FAILING_CONST);
                GameManager.Instance.fuel -= (FUEL_SPENDING / FAILING_CONST);
            }
        }
        speed = rb.velocity.magnitude;
    }

    private void FireThrusters()
    {        
        if (thruster1.activeInHierarchy)
        {
            thruster1.SetActive(false);
            thruster2.SetActive(true);
        }
        else
        {
            thruster1.SetActive(true);
            thruster2.SetActive(false);
        }
    }

    private void StopThrusters()
    {
        SoundManager.Instance.StopSoundEffect();
        thruster1.SetActive(false);
        thruster2.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopThrusters();
        if (GameManager.Instance.gameOn)
        {
            if (collision.gameObject.CompareTag("Planet"))
            {
                if (collision.relativeVelocity.magnitude < 5f)
                {
                    GameManager.Instance.WinGame();
                }
                else
                {
                    GameManager.Instance.LoseGame();
                    gameObject.SetActive(false);
                    Instantiate(explosion, transform.position, transform.rotation);
                }
            }
            else
            {
                GameManager.Instance.LoseGame();
                gameObject.SetActive(false);
                Instantiate(explosion, transform.position, transform.rotation);
            }
        }        
    }
}
