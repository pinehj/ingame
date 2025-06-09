
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LinqTest : MonoBehaviour
{
    private void Start()
    {
        List<Student> students = new List<Student>()
        {
            new Student() {Name = "허정범1", Age = 28, Gender = "남"},
            new Student() {Name = "허정범2", Age = 22, Gender = "남"},
            new Student() {Name = "허정범3", Age = 23, Gender = "남"},
            new Student() {Name = "허정범4", Age = 25, Gender = "남"},
            new Student() {Name = "허정범5", Age = 28, Gender = "남"},
            new Student() {Name = "허정범1", Age = 25, Gender = "여"},
            new Student() {Name = "허정범2", Age = 24, Gender = "여"},
            new Student() {Name = "허정범3", Age = 21, Gender = "여"},
            new Student() {Name = "허정범4", Age = 28, Gender = "여"},
            new Student() {Name = "허정범5", Age = 27, Gender = "여"},
            new Student() {Name = "허정범6", Age = 29, Gender = "여"},
        };

        // '컬렉션'에서 '데이터'를 '조회(나열)' 하는 일이 많음
        // C#은 이런 빈번한 작업을 편하게 하기 위해 LINQ 문법
        // Language Intergrated Query
        // 쿼리(Query): 질의 (데이터를 요청하거나 검색하는 명령문)

        // "FROM, IN, SELECT"
        var all = from student in students select student;
        all = students.Where((student) => true);

        foreach (var item in all)
        {
            Debug.Log(item);
        }

        var girls = from student in students where student.Gender == "여" select student;
        girls = students.Where((student) => student.Gender == "여");
        foreach (var item in girls)
        {
            Debug.Log(item.ToString());
        }
        var girls2 = from student in students
                     where student.Gender == "여"
                     orderby student.Age
                     select student;
        girls2 = students.Where((student) => student.Gender == "여").OrderBy(student => student.Age);

        foreach (var item in girls2)
        {
            Debug.Log(item.ToString());
        }

        //내부적으로 커서를 만드는데 나중에 쓰레기가 됨 메모리 증가 Update에서 사용 비추

        int mansCount = students.Count(student => student.Gender == "남");

        int totalAge = students.Sum(student => student.Age);

        int mansTotalAge = students.Where(student => student.Gender == "남").Sum(student => student.Age);
    }
}
