// Six simple self-contained unit checks: two for each structural pattern.
// Run: dotnet run -- --test
public static class PatternTests
{
    private static int _passed;
    private static int _failed;

    private static Student MakeStudent(params int[] grades)
    {
        var student = new StudentBuilder().SetId(900).SetName("Test Student")
            .SetFaculty("FCIM").SetGroup("FAF-242")
            .SetStudyYear(2).SetEmail("test@example.com").Build();
        student.Grades.AddRange(grades);
        return student;
    }

    private static void Check(string name, Action test)
    {
        try { test(); Console.WriteLine($"PASS: {name}"); _passed++; }
        catch (Exception ex) { Console.WriteLine($"FAIL: {name} - {ex.Message}"); _failed++; }
    }

    private static void Assert(bool condition)
    {
        if (!condition) throw new Exception("Assertion failed");
    }

    public static void Run()
    {
        Check("Decorator: header adds information", () =>
        {
            var student = MakeStudent(8, 10);
            IAcademicReport report = new HeaderDecorator(new AverageReport());
            string result = report.Generate(student);
            Assert(result.Contains("ACADEMIC REPORT") && result.Contains("9.00"));
        });

        Check("Decorator: two wrappers work in either order", () =>
        {
            var student = MakeStudent(9, 9);
            IAcademicReport first = new HeaderDecorator(new HonorsDecorator(new AverageReport()));
            IAcademicReport second = new HonorsDecorator(new HeaderDecorator(new AverageReport()));
            Assert(first.Generate(student).Contains("Classification: Excellent"));
            Assert(second.Generate(student).Contains("ACADEMIC REPORT"));
            Assert(second.Generate(student).Contains("Classification: Excellent"));
        });

        Check("Proxy: blocks students without grades", () =>
        {
            var student = MakeStudent();
            IAcademicReport report = new ReportProxy(new AverageReport());
            bool blocked = false;
            try { report.Generate(student); }
            catch (InvalidOperationException) { blocked = true; }
            Assert(blocked);
        });

        Check("Proxy: returns unchanged report for valid students", () =>
        {
            var student = MakeStudent(7, 9);
            IAcademicReport real = new AverageReport();
            IAcademicReport proxy = new ReportProxy(real);
            Assert(proxy.Generate(student) == real.Generate(student));
        });

        Check("Facade: generates a decorated report", () =>
        {
            var registry = StudentRegistry.Instance;
            var student = MakeStudent(10, 10);
            if (registry.FindById(student.Id) is null) registry.Add(student);
            var facade = new AcademicReportFacade(registry);
            string result = facade.Generate(student.Id, new AverageReportCreator(), true, true);
            Assert(result.Contains("ACADEMIC REPORT"));
            Assert(result.Contains("Average grade: 10.00"));
            Assert(result.Contains("Classification: Excellent"));
        });

        Check("Facade: refuses unknown student ID", () =>
        {
            var facade = new AcademicReportFacade(StudentRegistry.Instance);
            bool blocked = false;
            try { facade.Generate(-1, new HighestGradeReportCreator()); }
            catch (InvalidOperationException) { blocked = true; }
            Assert(blocked);
        });

        Console.WriteLine($"\nTests: {_passed} passed, {_failed} failed.");
        if (_failed > 0) Environment.ExitCode = 1;
    }
}
