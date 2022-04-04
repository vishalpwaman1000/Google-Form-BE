using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Form.Models
{
    public class EmailIDAvailabilityRequest
    {
        public string UserInput { get; set; }
    }

    public class EmailIDAvailabilityResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
