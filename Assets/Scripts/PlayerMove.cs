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
    public Text scoreText; // スコアの UI
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

    // 玉が他のオブジェクトにぶつかった時に呼び出される
    void OnTriggerEnter(Collider other)
    {
        // ぶつかったオブジェクトが収集アイテムだった場合
        if (other.gameObject.CompareTag("CleaItem"))
        {
            // その収集アイテムを非表示にします
            other.gameObject.SetActive(false);

            // スコアを加算します
            score = score + 1;

            // UI の表示を更新します
            SetCountText();
        }
    }

    void SetCountText()
    {
        // スコアの表示を更新
        scoreText.text = "Count: " + score.ToString();

        // すべての収集アイテムを獲得した場合
        if (score >= 1)
        {
            // リザルトの表示を更新
            winText.text = "You Win!";
        }
    }
}