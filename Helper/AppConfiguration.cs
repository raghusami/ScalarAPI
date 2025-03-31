namespace ScalarAPI.Helper
{
    public class AppConfiguration
    {
        public ApiCredential apiCredential { get; set; }
        public DBConfiguration DBConfiguration { get; set; }

        public LoggerConfiguration loggerConfiguration { get; set; }

    }
    public record DBConfiguration
    {
        public string CoreDBConnectionString { get; set; }
    }
    public record ApiCredential
    {
        public string UserName { get; set; }

        public string Password { get; set; }

    }

    public record LoggerConfiguration
    {
        public string LogFilePath { get; set; }

        public bool LogsWriteToFile { get; set; }

        public string LogsWriteToSeqURL { get; set; }
    }
}
  
