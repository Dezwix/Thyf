using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public sealed class PlayerMovement : MonoBehaviour
{
    #region Public Fields

    [SerializeField]
    [Range(0f, float.MaxValue)]
    private float Speed = 15;

    [SerializeField]
    [Range(0f, float.MaxValue)]
    private float JumpForce = 5f;

    [SerializeField]
    private GravityStagesCollection GravitySettings;

    private GravityDefinition currentGravity;
    #endregion

    #region Private Fields
    // private variables
    private Rigidbody rb;
    private bool isJumping = false;
    #endregion

    #region Delegates
    // Delegate variables
    private event Action onFixedUpdate = () => { };
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        this.onFixedUpdate = MoveWithAxes;
    }

    void FixedUpdate() => onFixedUpdate();

    private void Update()
    {
        CheckJump();
    }

    private void CheckJump()
    {
        if (isJumping)
            return;

        if (!Input.GetKeyDown(KeyCode.Space))
            return;

        isJumping = true;
        currentGravity = GravitySettings.JumpGravity;
        rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.VelocityChange);
        onFixedUpdate += CheckForJumpBegin;
    }

    private void CheckForJumpBegin()
    {
        if (rb.velocity.y <= currentGravity.MinVelocity)
            return;

        rb.useGravity = false;
        onFixedUpdate += DoGravity;

        onFixedUpdate -= CheckForJumpBegin;
        onFixedUpdate += CheckForFall;
    }

    private void CheckForFall()
    {
        if (rb.velocity.y > currentGravity.MinVelocity)
            return;

        onFixedUpdate -= CheckForFall;

        currentGravity = GravitySettings.FallGravity;
        onFixedUpdate += CheckForGround;
    }

    private void DoGravity() => rb.AddForce(new Vector3(0f, -currentGravity.Gravity, 0f));

    private void CheckForGround()
    {
        if (isJumping)
            return;

        rb.useGravity = true;

        onFixedUpdate -= DoGravity;
        onFixedUpdate -= CheckForGround;
        rb.velocity.Scale(new Vector3(1f, 0f, 1f));
    }

    void MoveWithAxes()
    {
        Vector3 downVel = new Vector3(0f, rb.velocity.y, 0f);
        Vector3 input = new Vector3(Input.GetAxis("Horizontal") * Speed, 0f, 0f);

        rb.velocity = transform.InverseTransformDirection(input) + downVel;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Ground")
            return;

        isJumping = false;
    }


    [Serializable]
    private class GravityStagesCollection
    {
        [SerializeField]
        private GravityDefinition jumpGravity;
        public GravityDefinition JumpGravity => jumpGravity;

        [SerializeField]
        private GravityDefinition hangTimeGravity;
        public GravityDefinition HangTimeGravity => hangTimeGravity;

        [SerializeField]
        private GravityDefinition fallGravity;
        public GravityDefinition FallGravity => fallGravity;
    }


    [Serializable]
    private class GravityDefinition
    {
        [SerializeField]
        [Range(0f, 10000f)]
        private float gravity = 50f;
        public float Gravity => gravity;

        [SerializeField]
        [Range(-2f, 2f)]
        private float minVelocity = 0.25f;
        public float MinVelocity => minVelocity;


    }
}
