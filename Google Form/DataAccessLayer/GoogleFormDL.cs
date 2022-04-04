

using Google_Form.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using RestSharp;
using System;
using System.Data.Common;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Google_Form.DataAccessLayer
{
    public class GoogleFormDL : IGoogleFormDL
    {
        public readonly IConfiguration _configuration;
        public readonly MySqlConnection _mySqlConnection;
        public GoogleFormDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mySqlConnection = new MySqlConnection(_configuration["ConnectionStrings:MySqlConnectionString"]);
        }

        public async Task<EmailIDAvailabilityResponse> EmailID_Availability(EmailIDAvailabilityRequest request)
        {
            EmailIDAvailabilityResponse response = new EmailIDAvailabilityResponse();
            response.IsSuccess = true;
            response.Message = "Information Fount";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"SELECT * 
                                    FROM googleform.GoogleAccount_UserDetail 
                                    WHERE RecoveryEmail=@UserInput OR MobileNumber=@UserInput";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@UserInput", request.UserInput);
                    using (DbDataReader dbDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (!dbDataReader.HasRows)
                        {
                            response.IsSuccess = false;
                            response.Message = "Recovery Input Not Exist";
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<FindFirstLastNameResponse> FindFirstLastName(FindFirstLastNameRequest request)
        {
            FindFirstLastNameResponse response = new FindFirstLastNameResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"SELECT * 
                                    FROM googleform.GoogleAccount_UserDetail 
                                    WHERE (MobileNumber=@UserName_EmailID OR RecoveryEmail=@UserName_EmailID) AND 
                                          FirstName=@FirstName AND 
                                          LastName=@LastName";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@UserName_EmailID", request.EmailID);
                    sqlCommand.Parameters.AddWithValue("@FirstName", request.FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", request.LastName);
                    using (DbDataReader dbDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (!dbDataReader.HasRows)
                        {
                            response.IsSuccess = false;
                            response.Message = "Invalid First Name & Last Name";
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<RegistrationResponse> Registration(RegistrationRequest request)
        {
            RegistrationResponse response = new RegistrationResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"INSERT INTO googleform.GoogleAccount_UserDetail 
                                    (FirstName, LastName, UserName_EmailID, Password, MobileNumber, RecoveryEmail)
                                    VALUES
                                    (@FirstName, @LastName, @UserName_EmailID, @Password, @MobileNumber, @RecoveryEmail)";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@FirstName", request.FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", request.LastName);
                    sqlCommand.Parameters.AddWithValue("@UserName_EmailID", request.UserName);
                    sqlCommand.Parameters.AddWithValue("@Password", request.Password);
                    sqlCommand.Parameters.AddWithValue("@MobileNumber", request.MobileNumber);
                    sqlCommand.Parameters.AddWithValue("@RecoveryEmail", request.RecoveryEmail);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Registration Query Not Executed";
                    }
                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry"))
                {
                    response.Message = request.UserName + " Email Id Already Exist.";
                }
                else
                {
                    response.Message = ex.Message;
                }
                response.IsSuccess = false;

            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new ResetPasswordResponse();
            response.IsSuccess = true;
            response.Message = "Successfully Password Reset.";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"UPDATE googleform.GoogleAccount_UserDetail
                                    SET Password=@Password
                                    WHERE UserName_EmailID=@UserName_EmailID";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@UserName_EmailID", request.EmailID);
                    sqlCommand.Parameters.AddWithValue("@Password", request.Password);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Password Reset Failed.";
                    }

                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<SendVerificationCodeOnEmailResponse> SendVerificationCodeOnEmail(SendVerificationCodeOnEmailRequest request)
        {
            SendVerificationCodeOnEmailResponse response = new SendVerificationCodeOnEmailResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                int VerificationCode = new Random().Next(100000, 999999);
                if (request.EmailID.ToLower().Contains("@gmail.com"))
                {
                    response = SendEmail(request.EmailID, VerificationCode.ToString(), "flag1");
                }
                else
                {
                    response = await SendOtpFunction(request.EmailID, VerificationCode.ToString(), "flag1");
                }

                if (!response.IsSuccess)
                {
                    return response;
                }

                string SqlQuery = @"UPDATE googleform.GoogleAccount_UserDetail
                                    SET VarificationCode=@VarificationCode
                                    WHERE RecoveryEmail=@UserName_EmailID OR MobileNumber=@UserName_EmailID";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@UserName_EmailID", request.EmailID);
                    sqlCommand.Parameters.AddWithValue("@VarificationCode", VerificationCode);
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Send Verification Code On Email Failed.";
                    }

                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        }

        public SendVerificationCodeOnEmailResponse SendEmail(string ToEmail, string Code, string flag)
        {
            SendVerificationCodeOnEmailResponse response = new SendVerificationCodeOnEmailResponse();
            response.IsSuccess = true;
            response.Message = "Send Email Successfully.";
            string MailBody = string.Empty, Subject = string.Empty;
            try
            {

                string to = ToEmail; //To address    
                string from = _configuration["Email:FromEmail"]; //From address    
                MailMessage message = new MailMessage(from, to);

                /*if (flag.ToLower() == "flag1")
                {
                    MailBody = "VCoder Verification Code : ";
                }
                else
                {
                    MailBody = "Your Email ID : ";
                }*/

                MailBody = flag.ToLower() == "flag1" ? "VCoder Verification Code : " + Code : "Your Email ID : " + Code; 
                Subject = flag.ToLower() == "flag1" ? "Verification Otp" : "Your Email ID";
                string mailbody = MailBody;
                message.Subject = Subject;
                message.Body = mailbody;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = false;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential(_configuration["Email:FromEmail"], _configuration["Email:FromEmailPassword"]);
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicCredential1;
                client.Send(message);

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<SendVerificationCodeOnEmailResponse> SendOtpFunction(string MobileNumber, string Code, string flag)
        {

            SendVerificationCodeOnEmailResponse response1 = new SendVerificationCodeOnEmailResponse();
            response1.IsSuccess = true;
            response1.Message = "OTP Send Successfully";
            string Message = string.Empty, Variable=string.Empty;
            try
            {
                Message = flag.ToLower() == "flag1" ? "Your Verification Code " : "Your Email ID : ";
                Variable = Code.ToString();
                var client = new RestClient("https://www.fast2sms.com/dev/bulkV2");
                var request = new RestRequest("https://www.fast2sms.com/dev/bulkV2", Method.Post);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddHeader("authorization", _configuration["SMSAuthentication:ApiAuthorizationKey"]);
                request.AddParameter("message", Message);
                request.AddParameter("variables_values", Variable);
                request.AddParameter("route", "otp");
                request.AddParameter("numbers", MobileNumber.ToString());
                RestResponse response = await client.ExecuteAsync(request);
                if (response.ResponseStatus.ToString() == "Error")
                {
                    response1.IsSuccess = false;
                    response1.Message = response.ErrorMessage;
                }
            }
            catch (Exception ex)
            {
                response1.IsSuccess = false;
                response1.Message
                    = ex.Message;
            }

            return response1;
        }

        public async Task<SignInResponse> SignIn(SignInRequest request)
        {

            SignInResponse response = new SignInResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"SELECT * FROM googleform.GoogleAccount_UserDetail 
                                    WHERE UserName_EmailID=@UserName_EmailID AND Password=@Password";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@UserName_EmailID", request.UserName);
                    sqlCommand.Parameters.AddWithValue("@Password", request.Password);
                    using (DbDataReader dbDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (!dbDataReader.HasRows)
                        {
                            response.IsSuccess = false;
                            response.Message = "Login UnSuccessful";
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;

        }

        public async Task<VerifyEmailVerificationCodeResponse> VerifyEmailVerificationCode(VerifyEmailVerificationCodeRequest request)
        {
            VerifyEmailVerificationCodeResponse response = new VerifyEmailVerificationCodeResponse();
            response.IsSuccess = true;
            response.Message = "Code Verification Successful";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"SELECT UserName_EmailID FROM googleform.GoogleAccount_UserDetail 
                                    WHERE (RecoveryEmail=@UserName_EmailID OR MobileNumber=@UserName_EmailID) AND 
                                          VarificationCode=@VarificationCode";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@UserName_EmailID", request.EmailID);
                    sqlCommand.Parameters.AddWithValue("@VarificationCode", request.VerificationCode);
                    using (DbDataReader dbDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (!dbDataReader.HasRows)
                        {
                            response.IsSuccess = false;
                            response.Message = "Enter Valid Verification Code";
                        }
                        else
                        {
                            await dbDataReader.ReadAsync();
                            response.EmailID = dbDataReader["UserName_EmailID"] != DBNull.Value ? dbDataReader["UserName_EmailID"].ToString() : string.Empty;
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        }

        public async Task<SendEmailIdOnRecoveryAccountResponse> SendEmailIdOnRecoveryAccount(SendEmailIdOnRecoveryAccountRequest request)
        {

            SendEmailIdOnRecoveryAccountResponse response = new SendEmailIdOnRecoveryAccountResponse();
            SendVerificationCodeOnEmailResponse response1 = new SendVerificationCodeOnEmailResponse();
            response.IsSuccess = true;
            response.Message = "Send Email ID Successful";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                //int VerificationCode = new Random().Next(100000, 999999);
                if (request.EmailID.ToLower().Contains("@gmail.com"))
                {
                    response1 = SendEmail(request.EmailID, request.UserRecoveryAccount,"flag2");
                }
                else
                {
                    response1 = await SendOtpFunction(request.EmailID, request.UserRecoveryAccount, "flag2");
                }

                response.Message = response1.Message;

                if (!response1.IsSuccess)
                {
                    response.IsSuccess = false;
                    return response;
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;

        }

        public async Task<SignInEmailIDResponse> SignIn_EmailID(SignInEmailIDRequest request)
        {
            SignInEmailIDResponse response = new SignInEmailIDResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"SELECT FirstName, LastName, MobileNumber, RecoveryEmail
                                    FROM googleform.GoogleAccount_UserDetail 
                                    WHERE UserName_EmailID=@UserName_EmailID";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@UserName_EmailID", request.EmailID);
                    using (DbDataReader dbDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (!dbDataReader.HasRows)
                        {
                            response.IsSuccess = false;
                            response.Message = "Email ID Not Found.";
                        }
                        else
                        {
                            await dbDataReader.ReadAsync();
                            response.FirstName = dbDataReader["FirstName"] != DBNull.Value ? dbDataReader["FirstName"].ToString() : string.Empty;
                            response.LastName = dbDataReader["LastName"] != DBNull.Value ? dbDataReader["LastName"].ToString() : string.Empty;
                            response.MobileNumber = dbDataReader["MobileNumber"] != DBNull.Value ? dbDataReader["MobileNumber"].ToString() : string.Empty;
                            response.RecoveryEmail = dbDataReader["RecoveryEmail"] != DBNull.Value ? dbDataReader["RecoveryEmail"].ToString() : string.Empty;
                        }
                        
                    }

                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                await _mySqlConnection.CloseAsync();
                await _mySqlConnection.DisposeAsync();
            }

            return response;
        }
    }
}
