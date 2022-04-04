using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Form.Models
{
    public class FindFirstLastNameRequest
    {
        public string EmailID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class FindFirstLastNameResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
