using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Form.Models
{
    public class SendVerificationCodeOnEmailRequest
    {
        [Required]
        public string EmailID { get; set; }
    }

    public class SendVerificationCodeOnEmailResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
