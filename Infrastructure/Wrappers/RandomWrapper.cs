using Application.Contracts;

namespace Infrastructure.Wrappers;

public class RandomWrapper : IRandomWrapper
{
    private readonly Random _random;
    
    public RandomWrapper()
    {
        _random = new Random();
    }
    
    public int RandomFromRange(int from, int to)
    {
        return _random.Next(from, to + 1);
    }
}