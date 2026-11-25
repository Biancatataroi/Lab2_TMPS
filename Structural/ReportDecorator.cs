// DECORATOR: adds optional text to a report without editing existing reports.
public abstract class ReportDecorator : IAcademicReport
{
    protected readonly IAcademicReport InnerReport;

    protected ReportDecorator(IAcademicReport innerReport) => InnerReport = innerReport;

    public abstract string Generate(Student student);
}
