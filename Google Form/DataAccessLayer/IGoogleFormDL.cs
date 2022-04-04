
using Google_Form.Models;
using System.Threading.Tasks;

namespace Google_Form.DataAccessLayer
{
    public interface IGoogleFormDL
    {
        public Task<RegistrationResponse> Registration(RegistrationRequest request);

        public Task<SignInEmailIDResponse> SignIn_EmailID(SignInEmailIDRequest request);

        public Task<SignInResponse> SignIn(SignInRequest request);

        public Task<EmailIDAvailabilityResponse> EmailID_Availability(EmailIDAvailabilityRequest request);

        public Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request);

        public Task<FindFirstLastNameResponse> FindFirstLastName(FindFirstLastNameRequest request);

        public Task<SendVerificationCodeOnEmailResponse> SendVerificationCodeOnEmail(SendVerificationCodeOnEmailRequest request);

        public Task<VerifyEmailVerificationCodeResponse> VerifyEmailVerificationCode(VerifyEmailVerificationCodeRequest request);

        public Task<SendEmailIdOnRecoveryAccountResponse> SendEmailIdOnRecoveryAccount(SendEmailIdOnRecoveryAccountRequest request);
    }
}
