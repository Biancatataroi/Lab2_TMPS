using System.Collections.Generic;
using System.Linq;

public class HighestCalculator : IGradeCalculator
{
    public double Calculate(List<int> grades) => grades.Max();
}
