
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LinqTest : MonoBehaviour
{
    private void Start()
    {
        List<Student> students = new List<Student>()
        {
            new Student() {Name = "������1", Age = 28, Gender = "��"},
            new Student() {Name = "������2", Age = 22, Gender = "��"},
            new Student() {Name = "������3", Age = 23, Gender = "��"},
            new Student() {Name = "������4", Age = 25, Gender = "��"},
            new Student() {Name = "������5", Age = 28, Gender = "��"},
            new Student() {Name = "������1", Age = 25, Gender = "��"},
            new Student() {Name = "������2", Age = 24, Gender = "��"},
            new Student() {Name = "������3", Age = 21, Gender = "��"},
            new Student() {Name = "������4", Age = 28, Gender = "��"},
            new Student() {Name = "������5", Age = 27, Gender = "��"},
            new Student() {Name = "������6", Age = 29, Gender = "��"},
        };

        // '�÷���'���� '������'�� '��ȸ(����)' �ϴ� ���� ����
        // C#�� �̷� ����� �۾��� ���ϰ� �ϱ� ���� LINQ ����
        // Language Intergrated Query
        // ����(Query): ���� (�����͸� ��û�ϰų� �˻��ϴ� ��ɹ�)

        // "FROM, IN, SELECT"
        var all = from student in students select student;
        all = students.Where((student) => true);

        foreach (var item in all)
        {
            Debug.Log(item);
        }

        var girls = from student in students where student.Gender == "��" select student;
        girls = students.Where((student) => student.Gender == "��");
        foreach (var item in girls)
        {
            Debug.Log(item.ToString());
        }
        var girls2 = from student in students
                     where student.Gender == "��"
                     orderby student.Age
                     select student;
        girls2 = students.Where((student) => student.Gender == "��").OrderBy(student => student.Age);

        foreach (var item in girls2)
        {
            Debug.Log(item.ToString());
        }

        //���������� Ŀ���� ����µ� ���߿� �����Ⱑ �� �޸� ���� Update���� ��� ����

        int mansCount = students.Count(student => student.Gender == "��");

        int totalAge = students.Sum(student => student.Age);

        int mansTotalAge = students.Where(student => student.Gender == "��").Sum(student => student.Age);
    }
}
