using System.Collections.Generic;

// OCP/DIP: calculators can be extended via this abstraction.
public interface IGradeCalculator
{
    double Calculate(List<int> grades);
}
