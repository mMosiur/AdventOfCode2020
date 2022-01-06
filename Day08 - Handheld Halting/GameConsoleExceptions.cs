using System;
using System.Runtime.Serialization;

namespace AdventOfCode.Year2020.Day08;

public partial class GameConsole
{
	public class InfiniteLoopException : Exception
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

	public class CorruptedProgramException : Exception
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
