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
    [Route("api/[controller]/[Action]")]
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

                response = await _googleFormDL.Registration(request);

            }catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn_EmailID(SignInEmailIDRequest request)
        {
            SignInEmailIDResponse response = new SignInEmailIDResponse();
            try
            {

                response = await _googleFormDL.SignIn_EmailID(request);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
            SignInResponse response = new SignInResponse();
            try
            {

                response = await _googleFormDL.SignIn(request);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> EmailID_Availability(EmailIDAvailabilityRequest request)
        {
            EmailIDAvailabilityResponse response = new EmailIDAvailabilityResponse();
            try
            {

                response = await _googleFormDL.EmailID_Availability(request);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new ResetPasswordResponse();
            try
            {

                response = await _googleFormDL.ResetPassword(request);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> FindFirstLastName(FindFirstLastNameRequest request)
        {
            FindFirstLastNameResponse response = new FindFirstLastNameResponse();
            try
            {

                response = await _googleFormDL.FindFirstLastName(request);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SendVerificationCodeOnRecoveryAccount(SendVerificationCodeOnEmailRequest request)
        {
            SendVerificationCodeOnEmailResponse response = new SendVerificationCodeOnEmailResponse();
            try
            {

                response = await _googleFormDL.SendVerificationCodeOnEmail(request);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> VerifyEmailVerificationCode(VerifyEmailVerificationCodeRequest request)
        {
            VerifyEmailVerificationCodeResponse response = new VerifyEmailVerificationCodeResponse();
            try
            {

                response = await _googleFormDL.VerifyEmailVerificationCode(request);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailIdOnRecoveryAccount(SendEmailIdOnRecoveryAccountRequest request)
        {
            SendEmailIdOnRecoveryAccountResponse response = new SendEmailIdOnRecoveryAccountResponse();
            try
            {

                response = await _googleFormDL.SendEmailIdOnRecoveryAccount(request);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }
    }
}
