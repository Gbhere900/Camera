using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool canWalk = true;
    [SerializeField] private bool canTurn = true;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CameraDetect cameraDetect;
    [SerializeField] private CameraShake cameraShake;

    [SerializeField] private Animator cameraAnimator;

    private float xRotation = 0f;

    private bool isCameraOn = false;

    private void Awake()
    {

        if (cameraTransform == null)
        {
            cameraTransform = transform.Find("MainCamera");
        }
        //cameraDetect.OutPutToCamera();

        //��ʼ�ر����
        CloseCamera();
    }

    private void Update()
    {
        cameraDetect.OutPutToCamera();
        // ����E���л����״̬
        if (Input.GetKeyDown(KeyCode.E))
        {
           
            if (isCameraOn)
            {
                CloseCamera();
            }
            else
            {
                OpenCamera();
            }
        }

        // ֻ���������ʱ�������ӽ���ת����ѡ�߼����������������
        if (canTurn && !isCameraOn)
        {
            HandleMouseLook();
        }

        if (canWalk&&!isCameraOn)
        {
            HandleMovement();
        }
    }

    // �������״̬������/���� + ��ʾ/���أ�
    private void SetCursorState(bool locked)
    {
        if (locked)
        {
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void OpenCamera()
    {
        isCameraOn = true;
        SetCursorState(true);
        cameraDetect.gameObject.SetActive(true);

        cameraAnimator.SetBool("isCameraOn", true);
        cameraShake.StopShake();
        //TODO:�����������
    }

    public void CloseCamera()
    {
        isCameraOn = false;
        SetCursorState(false);
        cameraDetect.gameObject.SetActive(false);

        cameraAnimator.SetBool("isCameraOn",false);
        //TODO:�����������
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void HandleMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;
        moveDirection.Normalize();

        transform.position += moveDirection * Time.deltaTime * speed;

        if (moveDirection != Vector3.zero)
        {
            cameraShake.StartShake();
        }
        else
        {
            cameraShake.StopShake();
        }
       
    }

    private void OnDrawGizmosSelected()
    {
        if (cameraTransform != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(cameraTransform.position, 0.2f);
        }
    }
}