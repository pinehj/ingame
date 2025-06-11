using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Attendance : MonoBehaviour
{
    public Transform SlotContainer;
    public Transform SlotContainer2;
    private List<UI_AttendanceSlot> _slots;

    private void Start()
    {
        AttendanceManager.Instance.OnDataChanged += Refresh;
        Init();
        Refresh();
    }
    private void Init()
    {
        _slots = new List<UI_AttendanceSlot>(SlotContainer.GetComponentsInChildren<UI_AttendanceSlot>());
        _slots.AddRange(new List<UI_AttendanceSlot>(SlotContainer2.GetComponentsInChildren<UI_AttendanceSlot>()));
    }
    private void Refresh()
    {
        List<AttendanceDTO> _attendances = AttendanceManager.Instance.Attendances;
        for(int i = 0; i<_slots.Count; i++)
        {
            _slots[i].Refresh(_attendances[i]);
        }
    }

}
