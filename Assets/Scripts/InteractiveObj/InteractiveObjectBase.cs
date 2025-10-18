using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveObjectBase : MonoBehaviour
{
    [Header("��������")]
    [SerializeField] protected float interactiveDistance;
    [SerializeField] protected float cooldownTime;
    [SerializeField] protected bool isHighlight = false;

    protected float lastInteractTime;

    protected virtual void Start() { Initialized(); }
    
    protected virtual void Update()
    {
        // �����Ƿ����
        isHighlight = CanInteract;
        // ����������
        CheckPlayerInput();
    }
    
    public bool CanInteract
    {
        get
        {
            if (cooldownTime > 0 && Time.time < lastInteractTime + cooldownTime)
            {
                return false;
            }
            return IsInteractionPossible();
        }
    }
    
    /// <summary>
    /// �жϽ����Ƿ����
    /// </summary>
    /// <returns></returns>
    protected abstract bool IsInteractionPossible();
    
    /// <summary>
    /// ��ʼ��
    /// </summary>
    protected abstract void Initialized();

    /// <summary>
    /// ����������
    /// </summary>
    protected virtual void CheckPlayerInput()
    {
        // F������
        if (Input.GetKeyDown(KeyCode.F) && CanInteract)
        {
            Interact();
        }
    }

    /// <summary>
    /// ��������
    /// </summary>
    protected virtual void Interact()
    {
        if (!CanInteract)
        {
            Debug.LogError("Can't interact with this object");
            return;
        }
        
        // ִ�о��彻���߼�
        PerformInteraction();
        // ��¼����ʱ��
        lastInteractTime = Time.time;
    }
    
    /// <summary>
    /// ���彻���߼�
    /// </summary>
    protected abstract void PerformInteraction();
}
