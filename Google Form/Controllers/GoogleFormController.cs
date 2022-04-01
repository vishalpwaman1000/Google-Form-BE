using Google_Form.DataAccessLayer;
using Google_Form.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Form.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleFormController : ControllerBase
    {
        public readonly IGoogleFormDL _googleFormDL;
        public GoogleFormController(IGoogleFormDL googleFormDL)
        {
            _googleFormDL = googleFormDL;
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationRequest request)
        {
            RegistrationResponse response = new RegistrationResponse();
            try
            {

            }catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }
    }
}
