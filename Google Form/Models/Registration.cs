using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Form.Models
{
    public class RegistrationRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class RegistrationResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
