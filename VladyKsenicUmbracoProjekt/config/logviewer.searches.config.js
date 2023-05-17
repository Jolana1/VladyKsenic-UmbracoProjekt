[
  {
    "name": "Find all logs that are from the namespace 'Umbraco.Core'",
    "query": "StartsWith(SourceContext, 'Umbraco.Core')"
  },
  {
    "name": "Find all logs that use a specific log message template",
    "query": "@MessageTemplate = '[Timing {TimingId}] {EndMessage} ({TimingDuration}ms)'"
  },
  {
    "name": "Find logs where one of the items in the SortedComponentTypes property array is equal to",
    "query": "SortedComponentTypes[?] = 'Umbraco.Web.Search.ExamineComponent'"
  },
  {
    "name": "Find all logs that the message has localhost in it with SQL like",
    "query": "@Message like '%localhost%'"
  },
  {
    "name": "Find all logs that the message that starts with 'end' in it with SQL like",
    "query": "@Message like 'end%'"
  }
]