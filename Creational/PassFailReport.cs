public class PassFailReport : IAcademicReport
{
    public string Generate(Student student)
    {
        var average = new GradeManager(new AverageCalculator()).GetResult(student);
        // Demonstration rule: an average of at least 5 means PASS.
        return $"Average: {average:F2} | Status: {(average >= 5 ? "PASS" : "FAIL")}";
    }
}
