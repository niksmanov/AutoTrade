using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AutoTrade.Infrastructure
{
	public class EmailSender : IEmailSender
	{
		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			var client = new SmtpClient()
			{
				Port = 587,
				Host = "smtp.gmail.com",
				EnableSsl = true,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential("autotrade.noreply", "autotrade007")
			};
			var mailMessage = new MailMessage
			{
				From = new MailAddress("autotrade.noreply@autotrade.com"),
				Subject = subject,
				Body = htmlMessage
			};
			mailMessage.To.Add(email);
			return client.SendMailAsync(mailMessage);
		}
	}
}
