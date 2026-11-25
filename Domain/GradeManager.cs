using System;

public class GradeManager
{
    private readonly IGradeCalculator _calculator;

    public GradeManager(IGradeCalculator calculator) => _calculator = calculator;

    public void AddGrade(Student student, int grade)
    {
        if (grade < 1 || grade > 10)
            throw new ArgumentOutOfRangeException(nameof(grade), "Grade must be between 1 and 10.");
        student.Grades.Add(grade);
    }

    public double GetResult(Student student)
    {
        if (student.Grades.Count == 0)
            throw new InvalidOperationException("Student has no grades.");
        return _calculator.Calculate(student.Grades);
    }
}
