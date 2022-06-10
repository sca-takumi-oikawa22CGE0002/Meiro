using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 6.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
    public Text scoreText; // �X�R�A�� UI
    public Text winText;
    private int score;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();

        score = 0;
        SetCountText();
        winText.text = "";
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);
        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    // �ʂ����̃I�u�W�F�N�g�ɂԂ��������ɌĂяo�����
    void OnTriggerEnter(Collider other)
    {
        // �Ԃ������I�u�W�F�N�g�����W�A�C�e���������ꍇ
        if (other.gameObject.CompareTag("CleaItem"))
        {
            // ���̎��W�A�C�e�����\���ɂ��܂�
            other.gameObject.SetActive(false);

            // �X�R�A�����Z���܂�
            score = score + 1;

            // UI �̕\�����X�V���܂�
            SetCountText();
        }
    }

    void SetCountText()
    {
        // �X�R�A�̕\�����X�V
        scoreText.text = "Count: " + score.ToString();

        // ���ׂĂ̎��W�A�C�e�����l�������ꍇ
        if (score >= 1)
        {
            // ���U���g�̕\�����X�V
            winText.text = "You Win!";
        }
    }
}