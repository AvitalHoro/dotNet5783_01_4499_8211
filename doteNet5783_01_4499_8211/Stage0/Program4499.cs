// See https://aka.ms/new-console-template for more information

using System;

namespace Targil0;

partial class Program
{
     static void Main(string[] args)
    {
        Welcome4499();
        Welcome8211();
        Console.ReadKey();
    }

    private static void Welcome4499()
    {
        string s;
        Console.WriteLine("Enter your name: ");
        s = Console.ReadLine();
        Console.WriteLine("{0}, welcome to my first console application", s);
    }
    static partial void Welcome8211();
}

