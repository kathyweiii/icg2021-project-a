using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarEntity : MonoBehaviour{

    public GameObject wheelFrontLeft;
    public GameObject wheelFrontRight;
    public GameObject wheelBackLeft;
    public GameObject wheelBackRight;
    
    // Car Steering
    float m_FrontWheelAngle = 0;
    const float WHEEL_ANGLE_LIMIT = 40f;
    public float turnAngularVelocity = 40f;


    // Accelerate and decelerate
    float m_Velocity = 0;
    float acceleration = 2f;
    float deceleration = 10f;
    float maxVelocity = 20f;
    float maxVelocityBackward = -3f;

    float m_DeltaMovement; // 儲存移動了多少
    public const float WHEEL_DISTANCE = 1f;


    // Color
    Color initialColor;
    SpriteRenderer m_TargetRenderer;

    void Start() {
        m_TargetRenderer = this.GetComponent<SpriteRenderer>();
        initialColor = m_TargetRenderer.color;
        // Debug.Log(initialColor);
    }

    void UpdateWheels(){
        // Update wheels by m_FrontWheelAngle (把輪子轉彎畫出來)
        Vector3 localEulerAngles = new Vector3(0f, 0f, m_FrontWheelAngle);
        wheelFrontRight.transform.localEulerAngles = localEulerAngles;
        wheelFrontLeft.transform.localEulerAngles = localEulerAngles;
    }

    void Stop(){
        m_Velocity = 0;
    }

    // Change color when colliding
    [SerializeField] SpriteRenderer[] m_Renderers = new SpriteRenderer[5];
    void ResetColor(){
        // ChangeColor(Color.white);
        ChangeColor(initialColor);
    }
    void ChangeColor(Color color){
        foreach(SpriteRenderer r in m_Renderers){
            r.color = color;  // 換成指定的顏色
        }
    }

    // Car reaction when colliding obstacle
    void OnCollisionEnter2D(Collision2D collision){
        Stop();
        ChangeColor(Color.red);
    }
    void OnCollisionStay2D(Collision2D collision){
        Stop();
    }
    void OnCollisionExit2D(Collision2D collision){
        ResetColor();
    }


    IEnumerator biggerSize() {
        float temp_Velocity = this.m_Velocity;
        Stop();
        yield return new WaitForSeconds(0.2f);
        this.transform.localScale += new Vector3(0.1f, 0.5f, 0f);
        // yield return new WaitForSeconds(0.1f);
        // this.transform.localScale -= new Vector3(0.1f, 0.1f, 0f);
        yield return new WaitForSeconds(0.2f);
        this.transform.localScale += new Vector3(0.1f, 0.5f, 0f);
        yield return new WaitForSeconds(0.2f);
        this.transform.localScale += new Vector3(0.1f, 0.5f, 0f);
        this.m_Velocity = temp_Velocity;
    }
    

    void OnTriggerEnter2D(Collider2D other){
        // Trigger chekpoint
        CheckPoint checkPoint = other.gameObject.GetComponent <CheckPoint>();
        if(checkPoint != null){
            ChangeColor(Color.green);
            this.Invoke("ResetColor", 0.1f);
        }

        // Trigger mushroom
        Mushroom mushroom = other.gameObject.GetComponent <Mushroom>();
        if(mushroom != null){
            ChangeColor(Color.green);
            StartCoroutine(biggerSize());
            this.Invoke("ResetColor", 0.1f);
        }

    }


    // Update is called once per frame
    void FixedUpdate(){
        if(Input.GetKey(KeyCode.UpArrow)){
            // Speed up
            m_Velocity = Mathf.Min(maxVelocity, m_Velocity + Time.fixedDeltaTime * acceleration);
        }
        if(Input.GetKey(KeyCode.DownArrow)){
            if(m_Velocity > 0){  // Brake
                m_Velocity = m_Velocity - Time.fixedDeltaTime * deceleration;
            }else{  // Backward
                m_Velocity = Mathf.Max(maxVelocityBackward, m_Velocity - Time.fixedDeltaTime * deceleration * 0.5f);
            }
        }
        
        m_DeltaMovement = m_Velocity * Time.fixedDeltaTime;

        if(Input.GetKey(KeyCode.LeftArrow)){
            // Turn left
            m_FrontWheelAngle = Mathf.Clamp(
                m_FrontWheelAngle + Time.fixedDeltaTime * turnAngularVelocity,
                - WHEEL_ANGLE_LIMIT,  // minimum
                WHEEL_ANGLE_LIMIT  // Maximum
            );
            UpdateWheels();
        }
        if(Input.GetKey(KeyCode.RightArrow)){
            // Turn right
            m_FrontWheelAngle = Mathf.Clamp(
                m_FrontWheelAngle - Time.fixedDeltaTime * turnAngularVelocity,
                - WHEEL_ANGLE_LIMIT,  // minimum
                WHEEL_ANGLE_LIMIT  // Maximum
            );
            UpdateWheels();
        }

        // 沒有按左右的時候，車慢慢回正
        if(!(Input.GetKey(KeyCode.RightArrow) | Input.GetKey(KeyCode.LeftArrow))){
            if(Mathf.Abs(m_FrontWheelAngle) < 0.05){
                m_FrontWheelAngle = 0;
            }
            
            if(m_FrontWheelAngle > 0){  // turning left
                m_FrontWheelAngle -= Time.fixedDeltaTime * turnAngularVelocity * 1.5f;
            }else if(m_FrontWheelAngle < 0){  // turning right
                m_FrontWheelAngle += Time.fixedDeltaTime * turnAngularVelocity * 1.5f;
            }
            
            UpdateWheels();
            //Debug.Log(this.m_Velocity);
        }


        // Update car transform
        float bodyAngleDelta = 1 / WHEEL_DISTANCE * Mathf.Tan(Mathf.Deg2Rad * m_FrontWheelAngle) * m_DeltaMovement;
        this.transform.Rotate(0f, 0f, bodyAngleDelta * Mathf.Rad2Deg);  // 旋轉
        this.transform.Translate(Vector3.up * m_DeltaMovement);  // 移動
    }
}
