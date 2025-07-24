using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using payrallproject.Data;
using payrallproject.Models.Domains;
using payrallproject.Models.Dtos;
using payrallproject.Models.Helpter;
using payrallproject.Services.AuthService;
using payrallproject.Services.EmailServices;
using RestSharp;
using System.Net;
using System.Security.Principal;

namespace payrallproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly ITokenRepository tokenRepository;
        private readonly IEmailService emailService;
        private readonly AuthDbContext _dbContext;
        public AuthController(AuthDbContext _dbContext, IConfiguration configuration, ITokenRepository tokenRepository, IEmailService emailService)
        {
            this.tokenRepository = tokenRepository;
            this.configuration = configuration;
            this.emailService = emailService;
            this._dbContext = _dbContext;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var identityUser = await _dbContext.User.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (identityUser != null)
            {
                var checkPasswordResult = BCrypt.Net.BCrypt.Verify(request.Password, identityUser.PasswordHash);
                if (checkPasswordResult)
                {
                    var userRoleIds = _dbContext.UserRoles
                                                .Where(ur => ur.UserId == identityUser.Id)
                                                .Select(ur => ur.RolesId)
                                                .ToList();
                    var roles = await _dbContext.Roles
                                                .Where(r => userRoleIds.Contains(r.Id))
                                                .Select(r => r.Name)
                                                .ToListAsync();
                    var jwtToken = tokenRepository.CreateJwtToken(identityUser, roles.ToList());
                    var response = new LoginResponseDto()
                    {
                        Email = request.Email,
                        Token = jwtToken,
                        Roles = roles
                    };
                    return Ok(response);
                }
                ModelState.AddModelError("", "Password Incorrect");
                return ValidationProblem(ModelState);
            }
            ModelState.AddModelError("", "Email Incorrect");
            return ValidationProblem(ModelState);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            if (request.Password != request.ConfirmPassword)
            {
                ModelState.AddModelError("", "Passwords do not match");
                return ValidationProblem(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _dbContext.User.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("", "Email Is Already registered");
                return ValidationProblem(ModelState);
            }

            var role = await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == request.Role);
            if (role == null)
            {
                ModelState.AddModelError("", "Specified role does not exist");
                return ValidationProblem(ModelState);
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User
            {
                UserName = request.Email?.Trim(),
                Email = request.Email?.Trim(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                PasswordHash = passwordHash
            };

            _dbContext.User.Add(user);
            await _dbContext.SaveChangesAsync();
            var xx = user.Id;

            var userRole = new UserRoles
            {
                UserId = user.Id,
                RolesId = role.Id
            };
            _dbContext.UserRoles.Add(userRole);
            await _dbContext.SaveChangesAsync();

            SendRegistrationEmail(user.Email, request.Password, request.Role);
            return Ok(new { Message = "User registered successfully" });
        }

        private async Task SendRegistrationEmail(string email, string password, string role)
        {
            try
            {
                Mailrequest mailrequest = new Mailrequest();
                mailrequest.ToEmail = email;
                mailrequest.Subject = "Welcome to Arithmos payrall System";
                mailrequest.Body = GetHtmlcontent(email, password, role);
                await emailService.SendEmailAsync(mailrequest);

            }
            catch (Exception e)
            {
                throw;
            }
        }
        private string GetHtmlcontent(string email, string password, string role)
        {
            return $@"
                <div style='width:100%;background-color:lightblue;text-align:center;'>
                    <h1>Welcome to Corzent !</h1>
                    <img src='https://scontent.fcmb8-1.fna.fbcdn.net/v/t39.30808-6/270563705_111714484713625_2133773990074388665_n.jpg?_nc_cat=110&ccb=1-7&_nc_sid=6ee11a&_nc_ohc=ZwDOt1-6YQsQ7kNvwHp-o6Q&_nc_oc=Adl-LvOeaFwWPwu9qBPDpxgMG8S-TjfZhtrrfwIZoV5e4MUhZfYIt7Nt6fE-S8vDbYw&_nc_zt=23&_nc_ht=scontent.fcmb8-1.fna&_nc_gid=Bcrar5fGyyQ_kPVTwIBwIg&oh=00_AfQRrwFuoTZTu3Myg84kEEhmrwUJlnBzIBbi23AVoylZ8A&oe=6886966C' alt='Arithmos Logo' style='max-width:100%;height:auto;' />
                    <h2>Dear {email},</h2>
                    <p>You have been registered to the system with the following details:</p>
                    <p><strong>Password:</strong> {password}</p>
                    <p><strong>Role:</strong> {role}</p>
                    <p>Please change your password after your first login.</p>
                    <h2>Thank You!</h2>
                    <div><h4>Contact us : arithmos@gmail.com</h4></div>
                </div>";
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Email == forgotPasswordDto.Email);
            if (user == null)
            {
                return Ok(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "user does not exist with this email",
                });
            }
            string token = Guid.NewGuid().ToString();
            var expiry = DateTime.UtcNow.AddHours(1);
            var resetToken = new PasswordResetTokens
            {
                UserId = user.Id.Value,
                Token = token,
                ExpiryDate = expiry
            };
            _dbContext.PasswordResetTokens.Add(resetToken);
            await _dbContext.SaveChangesAsync();
            var resetLink = $"http://localhost:3000/reset-password?email={user.Email}&token={WebUtility.UrlEncode(token)}";
            var client = new RestClient("https://send.api.mailtrap.io/api/send");
            var request = new RestRequest
            {
                Method = Method.Post,
                RequestFormat = DataFormat.Json
            };
            request.AddHeader("Authorization", "Bearer 1859b9b50120ad358d1a9512e3d5c88d");
            request.AddJsonBody(new
            {
                from = new { email = "mailtrap@demomailtrap.com" },
                to = new[] { new { email = user.Email } },
                template_uuid = "7bc90853-71ba-4aee-b476-560b145336be",
                template_variables = new { user_email = user.Email, pass_reset_link = resetLink }
            });
            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                return Ok(new AuthResponseDto
                {
                    IsSuccess = true,
                    Message = "Email sent with password reset link. please check your email."
                });
            }
            else
            {
                return BadRequest(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "failed to send email."
                });
            }
        }

        [AllowAnonymous]
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
            {
                return BadRequest(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Passwords do not match",
                });
            }
            var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Email == resetPasswordDto.Email);
            if (user == null)
            {
                return BadRequest(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "User does not exist with this Email",
                });
            }
            var tokenEntry = await _dbContext.PasswordResetTokens
                .FirstOrDefaultAsync(t => t.UserId == user.Id && t.Token == resetPasswordDto.Token);
            if (tokenEntry == null || tokenEntry.ExpiryDate < DateTime.UtcNow)
            {
                return BadRequest(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Invalid or expired reset token.",
                });
            }
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(resetPasswordDto.NewPassword);
            _dbContext.PasswordResetTokens.Remove(tokenEntry);
            _dbContext.User.Update(user);
            await _dbContext.SaveChangesAsync();

            return Ok(new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Password reset successfully.",
            });
        }

        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Email == changePasswordDto.Email);
            if (user == null)
            {
                return BadRequest(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "User does not exists with this email"
                });
            }
            bool isMatch = BCrypt.Net.BCrypt.Verify(changePasswordDto.CurrentPassword, user.PasswordHash);
            if (!isMatch)
            {
                return BadRequest(new AuthResponseDto
                {
                    IsSuccess = false,
                    Message = "Current password is incorrect"
                });
            }
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
            _dbContext.User.Update(user);
            await _dbContext.SaveChangesAsync();

            return Ok(new AuthResponseDto
            {
                IsSuccess = true,
                Message = "Password changed successfully"
            });
        }
    }
}
