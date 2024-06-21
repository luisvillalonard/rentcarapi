using Diversos.Core.Models.Email;
using System.Threading.Tasks;

namespace Diversos.Core.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
