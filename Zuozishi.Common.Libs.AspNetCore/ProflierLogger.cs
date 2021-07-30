using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace Zuozishi.Common.Libs.AspNetCore
{
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

        public ProflierStep Step(string name)
            => new ProflierStep(_logger, name);
    }

    public class ProflierStep : IDisposable
    {
        private readonly ILogger _logger;
        private readonly string _name;
        Stopwatch stopwatch;

        public ProflierStep(ILogger logger, string name)
        {
            _logger = logger;
            _name = name;
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public void Dispose()
        {
            stopwatch.Stop();
            _logger.LogInformation($"{_name} - [{stopwatch.ElapsedMilliseconds}ms]");
        }
    }
}