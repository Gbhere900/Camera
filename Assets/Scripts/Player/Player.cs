using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool canWalk = true;
    [SerializeField] private bool canTurn = true;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private CameraDetect cameraDetect;

    private float xRotation = 0f;

    private bool isCameraOn = false;

    private void Awake()
    {

        if (cameraTransform == null)
        {
            cameraTransform = transform.Find("MainCamera");
        }

        //��ʼ�ر����
        CloseCamera();
    }

    private void Update()
    {
        // ����E���л����״̬
        if (Input.GetKeyDown(KeyCode.E))
        {
           
            if (isCameraOn)
            {
                CloseCamera();
            }
            if (!isCameraOn)
            {
                OpenCamera();
            }
        }

        // ֻ���������ʱ�������ӽ���ת����ѡ�߼����������������
        if (canTurn && !isCameraOn)
        {
            HandleMouseLook();
        }

        if (canWalk)
        {
            HandleMovement();
        }
    }

    // �������״̬������/���� + ��ʾ/���أ�
    private void SetCursorState(bool locked)
    {
        if (locked)
        {
            // ������굽��Ļ���Ĳ�����
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            // ������겢��ʾ
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void OpenCamera()
    {
        isCameraOn = true;
        SetCursorState(false);
        cameraDetect.enabled = true;
        //TODO:�����������
    }

    public void CloseCamera()
    {
        isCameraOn = false;
        SetCursorState(true);
        cameraDetect.enabled = false;
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