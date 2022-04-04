using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Form.Models
{
    public class SendEmailIdOnRecoveryAccountRequest
    {
        public string UserRecoveryAccount { get; set; }
        public string EmailID { get; set; }
    }

    public class SendEmailIdOnRecoveryAccountResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
