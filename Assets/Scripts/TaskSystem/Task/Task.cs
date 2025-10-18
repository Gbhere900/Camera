using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewTask", menuName = "TaskSystem/Task")]
public class Task : ScriptableObject
{
    public enum TaskStatus
    {
        Pending = 0,    // ����ȡ
        InProgress = 1, // ������
        Completed = 2   // �����
    };
    
    [SerializeField] private int id;
    [SerializeField] private string name;
    [SerializeField] private string description;
    [SerializeField] private TaskItem taskItem;
    [FormerlySerializedAs("taskType")] [SerializeField] private TaskStatus taskStatus;

    private static readonly List<string> taskStatusString = new List<string>
    {
        "����ȡ",  // ����0 - Pending
        "������",  // ����1 - InProgress
        "�����"   // ����2 - Completed
    };
    
    //--------------------------���߷���--------------------------
    /// <summary>
    /// TaskStatusö��ת��Ϊ�ַ���
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public static string TaskStatus2String(TaskStatus status)
    {
        return taskStatusString[(int)status];
    }
    
    //--------------------------�����ӿ�--------------------------
    public int GetTaskId()
    {
        return id;
    }

    public string GetTaskName()
    {
        return name;
    }

    public string GetTaskDescription()
    {
        return description;
    }
    
    public TaskItem GetTaskItem()
    {
        return taskItem;
    }
    
    public void SetTaskType(TaskStatus taskStatus)
    {
        this.taskStatus = taskStatus;
    }

    public TaskStatus GetTaskStatus()
    {
        return taskStatus;
    }
}
