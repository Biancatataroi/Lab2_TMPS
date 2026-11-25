using System.Collections.Generic;

// SRP: Student stores student data; it does not calculate reports.
public class Student
{
    public int Id { get; }
    public string Name { get; set; }
    public string Faculty { get; set; }
    public string Group { get; set; }
    public int StudyYear { get; set; }
    public string Email { get; set; }
    public List<int> Grades { get; } = new();

    public Student(int id, string name, string faculty, string group, int studyYear, string email)
    {
        Id = id;
        Name = name;
        Faculty = faculty;
        Group = group;
        StudyYear = studyYear;
        Email = email;
    }
}
