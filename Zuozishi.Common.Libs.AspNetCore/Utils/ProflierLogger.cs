using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Zuozishi.Common.Libs.AspNetCore.Utils;


/// <summary>
/// 性能分析
/// </summary>
public class ProflierLogger
{
	private readonly ILogger _logger;

	public bool IsPrint { get; set; } = true;

	public ProflierLogger(ILogger logger)
	{
		_logger = logger;
	}

	public ProflierStep Step(string message, params object?[] args)
		=> new(_logger, message, args);
}

public class ProflierStep : IDisposable
{
	private readonly ILogger _logger;
	private readonly string _message;
	private readonly object?[] _args;
	private readonly Stopwatch stopwatch;

	public ProflierStep(ILogger logger, string message, params object?[] args)
	{
		_logger = logger;
		_message = message;
		_args = args;
		stopwatch = new Stopwatch();
		_logger.LogInformation("[{Action}]" + message, "START", args);
		stopwatch.Start();
	}

	public void Dispose()
	{
		stopwatch.Stop();
		var time = TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds);
		if (_args.Length == 0)
			_logger.LogInformation("[{Action}]" + _message + " [{Time}]", "END", time);
		else
			_logger.LogInformation("[{Action}]" + _message + " [{Time}]", "END", _args, time);
	}
}