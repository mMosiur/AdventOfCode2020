using System;
using System.Collections.Generic;
using System.IO;

namespace Day18
{
    class Program
    {
        static string[] ReadFile(string path)
        {
            FileStream filestream = null;
            try
            {
                filestream = new FileStream(path, FileMode.Open);
                using (StreamReader reader = new StreamReader(filestream))
                {
                    return reader.ReadToEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries);
                }
            }
            finally
            {
                if (filestream != null)
                {
                    filestream.Dispose();
                }
            }
        }

        static Queue<char> CreateEquationQueue(string equation, bool precedence)
        {
            Queue<char> queue = new Queue<char>();
            Stack<char> stack = new Stack<char>();
            using (StringReader reader = new StringReader(equation))
            {
                while (reader.Peek() >= 0)
                {
                    char symbol = (char)reader.Read();
                    if (symbol == ' ') continue;
                    if (char.IsDigit(symbol))
                    {
                        queue.Enqueue(symbol);
                    }
                    else if (symbol == '(')
                    {
                        stack.Push(symbol);
                    }
                    else if (symbol == ')')
                    {
                        while (stack.Count > 0 && stack.Peek() != '(')
                        {
                            queue.Enqueue(stack.Pop());
                        }
                        if(stack.Peek() == '(') stack.Pop();
                    }
                    else // e is an operator
                    {
                        while (stack.Count > 0 && stack.Peek() != '(')
                        {
                            if (precedence && symbol == '+' && stack.Peek() == '*')
                                break;
                            queue.Enqueue(stack.Pop());
                        }
                        stack.Push(symbol);
                    }
                }
                while (stack.Count > 0)
                    queue.Enqueue(stack.Pop());
            }
            return queue;
        }

        static long ResolveEquation(Queue<char> equation)
        {
            Stack<long> stack = new Stack<long>();
            foreach (char symbol in equation)
            {
                if (char.IsDigit(symbol))
                {
                    stack.Push((long)symbol - (long)'0');
                }
                else // operator
                {
                    switch (symbol)
                    {
                        case '+':
                            stack.Push(stack.Pop() + stack.Pop());
                            break;
                        case '*':
                            stack.Push(stack.Pop() * stack.Pop());
                            break;
                        default:
                            throw new ArithmeticException("Unknown operator");
                    }
                }
            }
            if (stack.Count != 1) throw new Exception("Unresolvable equation");
            return stack.Pop();
        }

        static void Main(string[] args)
        {
            string[] lines = ReadFile("input.txt");
            long sum = 0;
            foreach (string line in lines)
            {
                Queue<char> equation = CreateEquationQueue(line, false);
                long result = ResolveEquation(equation);
                sum += result;
            }
            System.Console.WriteLine($"Sum 1: {sum}");
            sum = 0;
            foreach (string line in lines)
            {
                Queue<char> equation = CreateEquationQueue(line, true);
                long result = ResolveEquation(equation);
                sum += result;
            }
            System.Console.WriteLine($"Sum 2: {sum}");
        }
    }
}
