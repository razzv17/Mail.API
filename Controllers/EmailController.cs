using System;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Mail.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public EmailController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromForm] string name, [FromForm] string email, [FromForm] string message)
        {
            try
            {
                // Set up your email client
                SmtpClient client = new SmtpClient(_configuration["SmtpSettings:Server"]);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_configuration["SmtpSettings:User"], _configuration["SmtpSettings:Pass"]);
                client.Port = int.Parse(_configuration["SmtpSettings:Port"]);
                client.EnableSsl = true;

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_configuration["SmtpSettings:Email"]);
                mailMessage.To.Add(_configuration["SmtpSettings:Email"]);
                mailMessage.Body = "Name: " + name + "\nEmail: " + email + "\nMessage: " + message;
                mailMessage.Subject = "New message from website";

                try
                {
                    await client.SendMailAsync(mailMessage);
                }
                catch (SmtpException ex)
                {
                    // Log the exception using your preferred logging framework
                    return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
                }
                catch (Exception ex)
                {
                    // Log the exception using your preferred logging framework
                    return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
                }

                return Ok("Message successfully sent!");
            }
            catch (Exception ex)
            {
                // Log the exception using your preferred logging framework
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
