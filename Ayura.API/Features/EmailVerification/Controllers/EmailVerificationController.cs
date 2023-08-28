using Ayura.API.Features.EmailVerification.DTOs;
using Ayura.API.Features.EmailVerification.Services;
using Ayura.API.Features.Profile.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Ayura.API.Features.EmailVerification.Controllers;

[ApiController]
[Route("api/evc")]
public class EmailVerificationController : Controller
{
    private readonly IEmailVerificationService _emailVerificationService;

    public EmailVerificationController(IEmailVerificationService emailVerificationService)
    {
        _emailVerificationService = emailVerificationService;
    }

    [HttpPost("generate")]
    public IActionResult GenerateEmailVerification([FromBody] EvcRequestDto evcodeRequest)
    {
        var userId = ResolveJWT.ResolveEmailFromJWT(Request);
        Console.Write($"ID is {userId}\n");
        var result = _emailVerificationService.GenerateEmailVerificationCode(evcodeRequest, userId);
        return Ok(result);
    }


    [HttpPost("verify")]
    public IActionResult VerifyEmail([FromBody] EvcVerifyDto evcodeVerify)
    {
        var userId = ResolveJWT.ResolveEmailFromJWT(Request);
        var result = _emailVerificationService.VerifyEmail(evcodeVerify, userId);
        return Ok(result);
    }
}