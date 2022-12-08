using Serilog.Core;
using Serilog.Events;
using System.Linq;

namespace Zuozishi.Common.Libs.AspNetCore.Utils;

public class MiniSourceContextEnricher : ILogEventEnricher
{
	public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
	{
		if (!logEvent.Properties.ContainsKey("SourceContext"))
			return;
		var sourceContext = logEvent.Properties["SourceContext"];
		if (sourceContext == null)
			return;
		var value = ((ScalarValue)sourceContext).Value;
		if (value == null)
			return;
		var typeStr = value.ToString();
		if (typeStr == null)
			return;
		var property = propertyFactory.CreateProperty("MiniSourceContext", typeStr.Split('.').Last());
		logEvent.AddPropertyIfAbsent(property);
	}
}
