using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OmniPot.Data.Identity;
using OmniPot.Models.AccountViewModels;
using OmniPot.Services;
using OmniPot.ViewModels;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using OmniPot.Data;
//using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IEmailSender _emailSender;
    private readonly ISmsSender _smsSender;
    private readonly ILogger _logger;
    private readonly IAuthyService _authyService;
    private readonly ApplicationDbContext appContext;
    private readonly RoleManager<ApplicationUser> roleManager;
    private readonly KindDbContext context;

    public AccountController(
               UserManager<ApplicationUser> userManager,
               SignInManager<ApplicationUser> signInManager,
               IEmailSender emailSender,
               ISmsSender smsSender,
               ILoggerFactory loggerFactory,
               IAuthyService authyService,
               ApplicationDbContext appContext,
               KindDbContext context
               //RoleManager<ApplicationDbContext> roleManager
               )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailSender = emailSender;
        _smsSender = smsSender;
        _logger = loggerFactory.CreateLogger<AccountController>();
        _authyService = authyService;
        this.appContext = appContext;
        this.context = context;
        //this.roleManager = roleManager;
    }

    private async Task<ApplicationUser> GetCurrentUserAsync()
    {
        return await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User));
    }

    //TODO: Need a method here that will get us our user object for storage on the client. 
    //      this needs to come after authentication and provide roles/claims, whatever is needed 
    //      on the client to be stored in local storage.

    [HttpPost("authenticate")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel user)
    {
        //DO NOT LOG THE USER'S PASSWORD HERE!

        _logger.LogDebug("Entering Login");
        IActionResult _result = new ObjectResult(false);
        GenericResult _authenticationResult = null;

        try
        {
            //HACK: This needs to be set to true for production. 
            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, lockoutOnFailure: false);
            //HACK: added just to test an authy only path.
            //await _signInManager.SignOutAsync();
            if (result.Succeeded)
            {
                //TODO: Apply roles. 
                /*
                IEnumerable<Role> _roles = _userRepository.GetUserRoles(user.Username);
                List<Claim> _claims = new List<Claim>();
                foreach (Role role in _roles)
                {
                    Claim _claim = new Claim(ClaimTypes.Role, "Admin", ClaimValueTypes.String, user.Username);
                    _claims.Add(_claim);
                }
                await HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(new ClaimsIdentity(_claims, CookieAuthenticationDefaults.AuthenticationScheme)));

                */
                var userProfile = await _userManager.GetUserAsync(User);
                if (userProfile != null)
                    _authenticationResult = new GenericResult()
                    {
                        Succeeded = true,
                        Message = await _userManager.GetUserIdAsync(userProfile) ?? "Success",
                        Requires2FA = true // result.RequiresTwoFactor
                    };
            }
            else
            {
                _logger.LogDebug("Authentication Failed.");
                _authenticationResult = new GenericResult()
                {
                    Succeeded = false,
                    Message = "Authentication failed"
                };
            }
        }
        catch (Exception ex)
        {
            _authenticationResult = new GenericResult()
            {
                Succeeded = false,
                Message = ex.Message
            };

            _logger.LogError("Exception thrown at Login", ex);
        }

        _result = new ObjectResult(_authenticationResult);
        return _result;
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        _logger.LogDebug("Entering Logout()");
        try
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception thrown at Logout", ex);

            return BadRequest();
        }

    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterViewModel user)
    {
        _logger.LogDebug("Entering Register");
        IActionResult _result = new ObjectResult(false);
        GenericResult _registrationResult = null;

        //TODO: Wire up what we need for registration. 
        var appUser = new ApplicationUser { UserName = user.Email, Email = user.Email, PhoneNumber = user.Phone };
        var result = await _userManager.CreateAsync(appUser, user.Password);
        if (result.Succeeded)
        {
            _logger.LogDebug("User creation successful.");

            await _userManager.UpdateAsync(appUser);
            await _userManager.SetPhoneNumberAsync(appUser, user.Phone);
            await _userManager.SetTwoFactorEnabledAsync(appUser, false);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = appUser.Id, code = code }, protocol: HttpContext.Request.Scheme);
            await _emailSender.SendEmailAsync(user.Email, "Activate Your Account for the RI MM Plant Tags System ",
                $@"
Welcome to the Rhode Island Medical Marijuana Plant RFID Tag System

Thanks for creating an account on the Rhode Island Medical Marijuana Plant RFID Tag System. 

To verify your account, please click the link below:
{callbackUrl} 

Please note: 
If you cannot access this link, copy and paste the entire URL into your browser.
 ");

            _logger.LogInformation(3, "User created a new account with password.");
            _registrationResult = new GenericResult() { Succeeded = true, Message = "Registration Suceeded, Must Confirm Email." };

        }
        else
        {
            _logger.LogWarning(string.Format("User registration failed: {0}", ((List<IdentityError>)result.Errors)[0].Description ?? "Registration failed."));
            _registrationResult = new GenericResult() { Succeeded = false, Message = ((List<IdentityError>)result.Errors)[0].Description ?? "Registration failed." };
        }

        //TODO: Logging; 

        _result = new ObjectResult(_registrationResult);
        return _result;
    }

    [HttpPost("registerWufoo")]
    public async Task<IActionResult> RegisterWufoo([FromBody] RegisterViewModel user)
    {
        _logger.LogDebug("Entering Register");
        IActionResult _result = new ObjectResult(false);
        GenericResult _registrationResult = null;
        Guid personId = Guid.Empty;

        //TODO: Wire up what we need for registration. 
        var appUser = new ApplicationUser { UserName = user.Email, Email = user.Email, PhoneNumber = user.Phone };
        var result = await _userManager.CreateAsync(appUser, user.Password);
        if (result.Succeeded)
        {
            _logger.LogDebug("User creation successful.");

            var person = await context.People.Where(p => p.EmailAddress == user.Email && p.State == TrackableEntityState.IsActive).FirstOrDefaultAsync();
            if (null != person)
                personId = person.PersonId;

            await _userManager.UpdateAsync(appUser);
            await _userManager.SetPhoneNumberAsync(appUser, user.Phone);
            await _userManager.SetTwoFactorEnabledAsync(appUser, false);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = appUser.Id, code = code }, protocol: HttpContext.Request.Scheme);
            await _emailSender.SendEmailAsync(user.Email, "Activate Your Account for the RI MM Plant Tags System ",
                $@"
Welcome to the Rhode Island Medical Marijuana Plant RFID Tag System

Thanks for creating an account on the Rhode Island Medical Marijuana Plant RFID Tag System. 

To verify your account, please click the link below:
{callbackUrl} 

Please note: 
If you cannot access this link, copy and paste the entire URL into your browser.
 ");

            _logger.LogInformation(3, "User created a new account with password.");
            _registrationResult = new GenericResult() { Succeeded = true, Message = "Registration Suceeded, Must Confirm Email.", PersonId = personId };

        }
        else
        {
            _logger.LogWarning(string.Format("User registration failed: {0}", ((List<IdentityError>)result.Errors)[0].Description ?? "Registration failed."));
            _registrationResult = new GenericResult() { Succeeded = false, Message = ((List<IdentityError>)result.Errors)[0].Description ?? "Registration failed." };
        }

        //TODO: Logging; 

        _result = new ObjectResult(_registrationResult);
        return _result;
    }

    [HttpGet("confirmEmail")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail([FromQuery]string userId, [FromQuery]string code)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                //Shouldn't tell them the email is bad or already confirmed.
                return Redirect("~/");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return Redirect("~/");
        }

        return Redirect("~/");
    }

    [HttpPost("ForgotPassword")]
    [AllowAnonymous]
    public async Task<IActionResult> ForgotPassword([FromBody]ForgotPasswordViewModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (null != user)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            byte[] tokenGeneratedBytes = Encoding.UTF8.GetBytes(code);
            var codeEncoded = WebEncoders.Base64UrlEncode(tokenGeneratedBytes);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = codeEncoded }, protocol: HttpContext.Request.Scheme);
            await _emailSender.SendEmailAsync(user.Email, "Password Reset for the RI MM Plant Tag System ",
                $@"
We received a request to reset the password for your account.


If you requested a reset for {user.Email}, click the link below. If you didn’t make this request, please ignore this email.

Reset password:
{callbackUrl} ");

        }

        var result = new GenericResult { Succeeded = true, Message = "Reset email sent." };
        //Don't tell them they have a bad email. Display a message telling them to check it.
        return new ObjectResult(result);
    }

    [HttpPost("ConfirmResetPassword")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmResetPassword([FromBody]ConfirmResetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByIdAsync(model.UserId);
        if (null != user)
        {
            var codeDecodedBytes = WebEncoders.Base64UrlDecode(model.Code);
            var codeDecoded = Encoding.UTF8.GetString(codeDecodedBytes);
            var result = await _userManager.ResetPasswordAsync(user, codeDecoded, model.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.ToList());
            }
        }



        //Redirect to login, don't want to tell them the account doesn't exist or the code was bad.
        return Redirect("~/");
    }

    [HttpGet("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromQuery]string userId, [FromQuery]string code)
    {
        var encodedCode = UrlEncoder.Default.Encode(code);
        var encodedUserId = UrlEncoder.Default.Encode(userId);
        var url = $"~/#/confirmResetPassword?userId=" + encodedUserId + "&code=" + encodedCode;
        return Redirect(url);
    }

    [HttpGet("GetUserRole")]
    public async Task<IActionResult> GetRoles()
    {
        var user = await _userManager.GetUserAsync(User);

        var roles = await _userManager.GetRolesAsync(user);

        return Ok(roles);
    }

    [HttpGet("getUserInfo")]
    public async Task<IActionResult> GetUser()
    {
        var user = await _userManager.GetUserAsync(User);

        return Ok(user);
    }

    [HttpGet("GetAllLicenses")]
    [Authorize(Roles = "SuperAdmin,StateProcessor,KindProcessor")]
    public async Task<IActionResult> GetAllLicenses()
    {
        var licenses = await context.StateLicenses.Select(u => new StateLicenseViewModel { LicenseNumber = u.LicenseNumber, IsMedicareID = u.IsMedicaid })
            .OrderBy(u => u.LicenseNumber)
            .ToListAsync();

        return Ok(licenses);
    }

    [HttpGet("GetAllUsers")]
    [Authorize(Roles = "SuperAdmin,StateProcessor,KindProcessor")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await appContext.Users.Select(u => new UserViewModel { UserId = u.Id, EmailAddress = u.UserName, PhoneNumber = u.PhoneNumber, EmailConfirmed = u.EmailConfirmed })
            .OrderBy(u => u.EmailAddress)
            .ToListAsync();

        return Ok(users);
    }

    [HttpGet("GetAllRoles")]
    [Authorize(Roles = "SuperAdmin,StateProcessor,KindProcessor")]
    public async Task<IActionResult> GetAllRoles()
    {
        var roles = await appContext.Roles.ToListAsync();

        return Ok(roles);

    }

    [HttpPost("SetRoles")]
    [Authorize(Roles = "SuperAdmin,StateProcessor,KindProcessor")]
    public async Task<IActionResult> SetRoles([FromBody]SetRolesViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.UserId);
        if (null == user)
            return BadRequest(new { Success = false, Message = "User doesn't exist!" });

        // foreach (var role in model.Roles)
        // {
        //    await roleManager.SetRoleNameAsync(user, role);

        // }
        // Remove all roles from user
        var roles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, roles.ToArray());

        // assing new role
        await _userManager.AddToRoleAsync(user, model.Roles.SingleOrDefault());

        return Ok(new { Success = true, Message = "Permission has been assigned to user successfully!" });
    }

    [HttpPost("SetReducedFee")]
    [Authorize(Roles = "SuperAdmin,StateProcessor,KindProcessor")]
    public async Task<IActionResult> SetReducedFee([FromBody]StateLicenseViewModel model)
    {
        var licenses = await context.StateLicenses.Where(u => u.LicenseNumber == model.LicenseNumber)
            .FirstOrDefaultAsync();
        if (null == licenses)
            return BadRequest(new { Success = false, Message = "License Number doesn't exist!" });
        else
        {
            licenses.IsMedicaid = model.IsMedicareID;
            await context.SaveChangesAsync();
        }

        return Ok(new { Success = true, Message = "Reduced Fee applied successfully!" });
    }

    [HttpPost("SetEmailConfirmed")]
    [Authorize(Roles = "SuperAdmin,StateProcessor,KindProcessor")]
    public async Task<IActionResult> SetEmailConfirmed([FromBody]SetRolesViewModel model)
    {
        //var user = await _userManager.FindByIdAsync(model.UserId);
        var users = await appContext.Users.Where(u => u.Id == model.UserId)
            .FirstOrDefaultAsync();
        if (null == users)
            return BadRequest(new { Success = false, Message = "User doesn't exist!" });
        else
        {
            users.EmailConfirmed = true;
            await appContext.SaveChangesAsync();
            _logger.LogInformation(3, "User account " + users.Email + " have been confirmed.");
        }

        return Ok(new { Success = true, Message = "User Email/Account is confirmed successfully!" });
    }

    [HttpPost("DenyAccess")]
    [Authorize(Roles = "SuperAdmin,StateProcessor,KindProcessor")]
    public async Task<IActionResult> DenyAccess([FromBody]SetRolesViewModel model)
    {
        //var user = await _userManager.FindByIdAsync(model.UserId);
        var users = await appContext.Users.Where(u => u.Id == model.UserId)
            .FirstOrDefaultAsync();
        if (null == users)
            return BadRequest(new { Success = false, Message = "User doesn't exist!" });
        else
        {
            users.EmailConfirmed = false;
            await appContext.SaveChangesAsync();
        }

        return Ok(new { Success = true, Message = "User Email/Account is denied access successfully!" });
    }

    //[Route("sendcode")]
    //[HttpPost]
    //public async Task<IActionResult> SendCode([FromBody] LoginViewModel user)
    //{
    //    _logger.LogDebug("Entering SendCode()");
    //    IActionResult _result = new ObjectResult(false);
    //    GenericResult _sendCodeResult = new GenericResult() { Succeeded = false, Message = "An error occured" };

    //    var appUser = await _userManager.FindByNameAsync(user.Email);
    //    if (null != appUser)
    //    {
    //        _logger.LogDebug("Sending Authy SMS");
    //        var result = _authyService.SendSms(appUser.AuthyUserId);
    //        if (!result.Success) _logger.LogWarning(string.Format("Authy request failed: {0}", result.Message));
    //        _sendCodeResult = new GenericResult() { Succeeded = result.Success, Message = result.Message };
    //    }

    //    _result = new ObjectResult(_sendCodeResult);
    //    return _result;
    //}

    //[Route("verifycode")]
    //[HttpPost]
    //public async Task<IActionResult> VerifyCode([FromBody]LoginViewModel model)
    //{
    //    _logger.LogDebug("Entering VerifyCode()");
    //    IActionResult _result = new ObjectResult(false);
    //    GenericResult _verifyResult = new GenericResult() { Succeeded = false, Message = "An error occured" };

    //    var user = await _userManager.FindByNameAsync(model.Email);
    //    if (null != user)
    //    {
    //        _logger.LogDebug("Sending authy request.");
    //        var result = _authyService.VerifyToken(user.AuthyUserId, model.Code);
    //        _verifyResult = new GenericResult() { Succeeded = result.Success, Message = result.Message };
    //        if (result.Success)
    //        {
    //            _logger.LogDebug("Authy verification succeeded, signing in user.");
    //            var signIn = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
    //            if (!signIn.Succeeded) _logger.LogError(string.Format("Could not sign in user for some reason: {0}", signIn));
    //        }
    //    }
    //    else
    //    {
    //        _logger.LogDebug("User not found");
    //    }

    //    _result = new ObjectResult(_verifyResult);
    //    return _result;
    //}
}