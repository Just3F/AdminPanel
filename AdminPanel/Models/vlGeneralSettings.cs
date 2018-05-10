namespace AdminPanel.Models
{
    public class vlGeneralSettings : DatabaseObject
    {
        public bool IsRequiredEmailVerification { get; set; }
        public bool IsRequiredPhoneVerification { get; set; }
    }
}
