using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Form.Models
{
    public class ResetPasswordRequest
    {
        public string EmailID { get; set; }
        public string Password { get; set; }
    }

    public class ResetPasswordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
