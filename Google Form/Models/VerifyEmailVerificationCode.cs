using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Form.Models
{
    public class VerifyEmailVerificationCodeRequest
    {
        public string EmailID { get; set; }
        public int VerificationCode { get; set; }
    }

    public class VerifyEmailVerificationCodeResponse
    {
        public string EmailID { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
