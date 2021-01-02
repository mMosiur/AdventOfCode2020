using System;
using System.IO;
using System.Runtime.Serialization;

namespace Day08
{
    public class GameConsole
    {
        public string[] Program { get; set; }

        public int Accumulator { get; private set; }

        public bool Run(bool throwExceptions = false)
        {
            try
            {
                Accumulator = 0;
                int pc = 0;
                bool[] commandsRun = new bool[Program.Length];
                while (pc < Program.Length)
                {
                    if (commandsRun[pc])
                        throw new InfiniteLoopException();
                    else commandsRun[pc] = true;
                    string[] command = Program[pc].Split(' ');
                    if (command.Length != 2)
                        throw new CorruptedProgramException();
                    int arg;
                    if (!int.TryParse(command[1], out arg))
                        throw new CorruptedProgramException();
                    switch (command[0])
                    {
                        case "acc":
                            Accumulator += arg;
                            pc++;
                            break;
                        case "jmp":
                            pc += arg;
                            break;
                        case "nop":
                            pc++;
                            break;
                        default:
                            throw new CorruptedProgramException();
                    }
                }
                return true;
            }
            catch (Exception)
            {
                if (throwExceptions)
                    throw;
                else return false;
            }
        }

        internal class InfiniteLoopException : Exception
        {
            public InfiniteLoopException() : base()
            {
            }

            public InfiniteLoopException(string message) : base(message)
            {
            }

            public InfiniteLoopException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected InfiniteLoopException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }

        internal class CorruptedProgramException : Exception
        {
            public CorruptedProgramException() : base()
            {
            }

            public CorruptedProgramException(string message) : base(message)
            {
            }

            public CorruptedProgramException(string message, Exception innerException) : base(message, innerException)
            {
            }

            protected CorruptedProgramException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }


    class Program
    {

        static bool RepairProgram(GameConsole gc)
        {
            for (int i = 0; i < gc.Program.Length; i++)
            {
                string[] command = gc.Program[i].Split(' ');
                switch (command[0])
                {
                    case "jmp":
                        gc.Program[i] = "nop " + command[1];
                        if (gc.Run()) return true;
                        gc.Program[i] = "jmp " + command[1];
                        break;
                    case "nop":
                        gc.Program[i] = "jmp " + command[1];
                        if (gc.Run()) return true;
                        gc.Program[i] = "nop " + command[1];
                        break;
                    default:
                        break;
                }
            }
            return false;
        }

        static void Main(string[] args)
        {
            FileStream fileStream = new("input.txt", FileMode.Open);
            GameConsole gc = new();
            using (StreamReader reader = new(fileStream))
            {
                gc.Program = reader.ReadToEnd().Split('\n', StringSplitOptions.RemoveEmptyEntries);
            }
            gc.Run();
            Console.WriteLine($"Acc before infinite loop: {gc.Accumulator}");
            RepairProgram(gc);
            //gc.Run(); // Not needed because "RepairProgram" in itself runs after every iteration
            Console.WriteLine($"Acc after successful program run: {gc.Accumulator}");
        }
    }
}
