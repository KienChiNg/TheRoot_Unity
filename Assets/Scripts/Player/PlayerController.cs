using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private FixedJoystick playerJoystick;
    private Rigidbody playerRb;
    private Animator playerAnimator;
    private PlayerAttribute playerAttribute;
    private ParticleSystem particleLeaves;
    private ParticleSystem particleSmoke;
    private ManagerGame gameManager;
    private Vector2 posInitJoystick;
    private Vector3 startTouch = Vector3.zero;
    private float speed;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerAttribute = GetComponent<PlayerAttribute>();
        particleLeaves = transform.GetChild(0).GetComponent<ParticleSystem>();
        particleSmoke = transform.GetChild(1).GetComponent<ParticleSystem>();
        posInitJoystick = playerJoystick.gameObject.transform.position;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManagerGame>();
        speed = playerAttribute.PlayerSpeed;
        // System.GC.Collect();
        // Resources.UnloadUnusedAssets();
    }
    private void Update()
    {
        if (gameManager.PauseGame) return;
    }
    private void FixedUpdate()
    {
        if (gameManager.PauseGame)
        {
            speed = 0;
            // return;
        }
        else speed = playerAttribute.PlayerSpeed;
        // if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && Time.timeScale != 0)
        // {
        //     startTouch = Input.mousePosition;
        //     playerJoystick.gameObject.transform.position = startTouch;

        // }
        // else if (startTouch != Vector3.zero && Input.GetMouseButton(0))
        // {
        //     playerRb.velocity = new Vector3(
        //         playerJoystick.Horizontal * speed
        //         , playerRb.velocity.y
        //         , playerJoystick.Vertical * speed
        //         );
        // }
        // else if (Input.GetMouseButtonUp(0))
        // {
        //     playerJoystick.transform.position = posInitJoystick;
        //     startTouch = Vector3.zero;

        // }

        playerRb.velocity = new Vector3(
                playerJoystick.Horizontal * speed
                , playerRb.velocity.y
                , playerJoystick.Vertical * speed
                );

        if (playerJoystick.Horizontal != 0 || playerJoystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(playerRb.velocity);
            // playerAnimator.SetBool("Run", true);
            // SetStateParticle(true);
        }
        else
        {
            // playerAnimator.SetBool("Run", false);
            // SetStateParticle(false);
        }
    }
    void SetStateParticle(bool state)
    {
        var em1 = particleLeaves.emission;
        em1.enabled = state;
        var em2 = particleSmoke.emission;
        em2.enabled = state;
    }
}
