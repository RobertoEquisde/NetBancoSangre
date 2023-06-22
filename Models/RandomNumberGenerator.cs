using System;

public static class RandomNumberGenerator
{
    private static readonly Random random = new Random();

    public static int GenerateRandomNumber()
    {
        int[] numbers = { 100, 150, 250, 400 };
        int index = random.Next(numbers.Length);
        return numbers[index];
    }
}
