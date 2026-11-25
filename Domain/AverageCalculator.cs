using System.Collections.Generic;
using System.Linq;

public class AverageCalculator : IGradeCalculator
{
    public double Calculate(List<int> grades) => grades.Average();
}
