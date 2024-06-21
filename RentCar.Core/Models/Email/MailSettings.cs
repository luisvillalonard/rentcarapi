namespace Diversos.Core.Models.Email
{
    public class MailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool DefaultCredentials { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public bool SSL { get; set; }
    }
}
