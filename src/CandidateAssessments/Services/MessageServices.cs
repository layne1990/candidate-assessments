using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateAssessments.Services
{
    // Might use this class in the future to notify that an assessment has been completed
    public class AuthMessageSender : IEmailSender 
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
        
    }
}
