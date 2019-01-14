using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Kmin
{
    //написать алгоритм нахождения k-th smallest элемента в массиве целых чисел
    //сделать так, чтобы алгоритм работал за O(n)
    //пример[3, 2, 6, 0, 8, 10, 20]
    //1st smallest = 0
    //2nd smallest = 2
    //3rd smallest = 3
    //4th smallest = 6

    class Program
    {
        static void Main(string[] args)
        {
            string input = System.Environment.GetEnvironmentVariable("input");
            string output = System.Environment.GetEnvironmentVariable("output");

            string allText = File.ReadAllText(input);
            var splittedValues = allText.Split(new char[] {' ', '\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
            var k = int.Parse(splittedValues[0]);

            var array = splittedValues.Skip(1).Select(x => int.Parse(x)).ToArray();
            var maxHeap = new MaxHeap(k);

            array.Take(k).ForEach(x => maxHeap.Add(x));
            array.Skip(k).ForEach(x =>
            {
                if (maxHeap.Peek() > x)
                {
                    maxHeap.Pop();
                    maxHeap.Add(x);
                }
            });

            var sb = new StringBuilder();
            maxHeap.GetArray().ForEach(x => sb.Append(x).Append(" "));
            File.WriteAllText(output, sb.ToString());
        }
    }

    public static class Extension
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var element in enumerable)
            {
                action(element);
            }

            return enumerable;
        }
    }
}
