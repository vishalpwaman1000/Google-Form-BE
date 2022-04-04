using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Form.Models
{
    public class SignInEmailIDRequest
    {
        public string EmailID { get; set; }
    }

    public class SignInEmailIDResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string RecoveryEmail { get; set; }
    }
}
