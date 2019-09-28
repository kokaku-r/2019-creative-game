using UnityEngine;

public class CharacterControl
{
    public static float speed;
    //落ちる速さ、重力
    private static float gravity = 10f;
    private static float jumpSpeed;

    //通常時のスピードとジャンプ力
    private static float normalSpeed = 5;
    private static float normalJump = 10;
    //左Shift押したときのスピードとジャンプ力
    private static float shiftSpeed = 10;
    private static float shiftJump = 20;

    //Playerの移動や向く方向を入れる
    public static Vector3 moveDirection;


    public static Vector3 GetMoveDirection(bool isGrounded)
    {
        //左右どちらかのShift押した場合と離している場合
        if (Input.GetKey(KeyCode.LeftShift)
            || Input.GetKey(KeyCode.RightShift))
        {
            speed = shiftSpeed;
            jumpSpeed = shiftJump;
        }
        else
        {
            speed = normalSpeed;
            jumpSpeed = normalJump;
        }

        //Playerが地面に設置していることを判定
        if (isGrounded)
        {
            //XZ軸の移動と向きを代入する
            //WASD,上下左右キー
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection *= speed;

        }

        // 重力を設定しないと落下しない
        moveDirection.y -= gravity * Time.deltaTime;
        return moveDirection;
    }


}