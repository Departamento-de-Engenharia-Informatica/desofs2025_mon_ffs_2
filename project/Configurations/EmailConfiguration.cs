namespace AMAPP.API.Configurations
{
    public class EmailConfiguration
    {
        public string From { get; set; } = string.Empty;
        public string SmtpServer { get; set; } = string.Empty;
        public int Port { get; set; }
        public string EmailEnvUsername { get; set; } = string.Empty;
        public string EmailEnvPassword { get; set; } = string.Empty;
    }
}
