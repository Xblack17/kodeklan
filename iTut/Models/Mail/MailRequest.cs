using iTut.Models.Users;

namespace iTut.Models.Mail
{
    public class MailRequest
    {
        public string Receiver { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public static class EmailSubject
    {
        public static string GetParentInviteSubject() => "iTUT Parent Invite";
        public static string GetComplaintCreatedSuubject(ParentUser parent) => $"Hi, {parent.FirstName}! Your complaint has been created";
    }
}
