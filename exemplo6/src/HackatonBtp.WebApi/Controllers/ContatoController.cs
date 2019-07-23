using System.Threading.Tasks;
using HackathonBtp.Application.Email;
using HackathonBtp.Domain.Helpers;
using HackathonBtp.Domain.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using HackathonBtp.Domain.Models;

namespace HackatonBtp.WebApi.Controllers
{
    [ApiController]
    [Route("btp/[controller]")]
    public class ContatoController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IContantoEmailSender _emailSender;

        public ContatoController(IConfiguration config, IContantoEmailSender emailSender)
        {
            _config = config;
            _emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> EnviarMensagem([FromForm]ContatoDTO contatoDto)
        {
            if (!await GoogleRecaptcha.IsReCaptchaPassedAsync(Request.Form["g-recaptcha-response"], _config["GoogleReCaptcha:SecretKey"]))
            {
                return BadRequest(new { error = "Captcha inválido" });
            }

            var contato = Mapper.Map<Contato>(contatoDto);

            bool enviouEmail = _emailSender.EnviarEmail(contato.Nome, contato.Email, contato.Mensagem);

            if (enviouEmail)
                return Ok(contatoDto);

            return BadRequest(new { error = "Falha ao enviar email" });
        }

    }
}