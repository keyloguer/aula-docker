using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HackatonBtp.Domain.Models.DTOs;
using HackatonBtp.Domain.Models;
using AutoMapper;
using HackatonBtp.Domain.Interfaces.Repository;
using HackatonBtp.Application.Email;
using HackatonBtp.WebApi.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;

namespace HackatonBtp.WebApi.Core.Controllers
{
    [Route("btp/[controller]")]
    [SqlExceptionFilter]
    [ApiController]
    public class TimeController : ControllerBase
    {
        private readonly ITimeRepository _timeRepository;
        private readonly IEmailSender _emailSender;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IHttpContextAccessor _context;
        private readonly IConfiguration _config;

        public TimeController(ITimeRepository timeRepository, IEmailSender emailSender, IConfiguration config,
                              IHostingEnvironment hostingEnvironment, IHttpContextAccessor context)
        {
            _timeRepository = timeRepository;
            _emailSender = emailSender;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
            _config = config;
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Time))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Participar([FromBody]TimeDTO timeIntegrante)
        {
            var time = Mapper.Map<TimeDTO, Time>(timeIntegrante);

            using (var timeRepository = _timeRepository)
            {
                await timeRepository.Save(time);

                var htmlTemplates = _config.GetSection("HtmlTemplates");

                var emailDto = new EmailDTO()
                {
                    Time = time,
                    UrlEnvioEmail = _context.HttpContext.Request.Host.Value + "/btp/email&ConfirmarEmail=" + time.Id,
                    EmailTemplate = _hostingEnvironment.WebRootPath + htmlTemplates["EnvioEmail"]
                };

                return CreatedAtAction(nameof(Participar), new { id = time.Id }, time);
            }
        }

        [Route("~/btp/time&VerificarNomeTime={nomeTime}")]
        [HttpGet]
        public async Task<IActionResult> VerificarNomeTime(string nomeTime)
        {
            using (var timeRepository = _timeRepository)
            {
                var time = await timeRepository.BuscarPorNome(nomeTime);

                if (time != null)
                    return Ok(new { error = "O nome do time já está em uso." });

                return Ok(new { success = "Nome de time disponível." });

            }
        }

        [Route("~/btp/time&VerificarEmailTime={emailTime}")]
        [HttpGet]
        public async Task<IActionResult> VerificarEmailTime(string emailTime)
        {
            using (var timeRepository = _timeRepository)
            {
                var time = await timeRepository.BuscarPorEmail(emailTime);

                if (time != null)
                    return Ok(new { error = "O email informado já está em uso." });

                return Ok(new { success = "Email não está em uso." });
            }
        }
    }
}