using System;
using System.Net;
using System.Threading.Tasks;
using HackatonBtp.Domain.Interfaces.Repository;
using HackatonBtp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using HackatonBtp.Application.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;

namespace HackatonBtp.WebApi.Core.Controllers
{
    [Route("btp/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ITimeRepository _timeRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _config;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _context;

        public EmailController(ITimeRepository timeRepository, IHostingEnvironment hostingEnvironment, IConfiguration config,
                               IEmailSender emailSender, IHttpContextAccessor context)
        {
            _timeRepository = timeRepository;
            _hostingEnvironment = hostingEnvironment;
            _config = config;
            _emailSender = emailSender;
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> EnviarEmailsSemEnvio()
        {
            var htmlTemplates = _config.GetSection("HtmlTemplates");

            using (var timeRepository = _timeRepository)
            {
                var timesSemEnvioEmail = await timeRepository.ObterTimesSemEnvioEmail();

                foreach (var time in timesSemEnvioEmail)
                {
                    var emailDTO = new EmailDTO()
                    {
                        Time = time,
                        UrlEnvioEmail = _context.HttpContext.Request.Host.Value + "/btp/email&ConfirmarEmail=" + time.Id,
                        EmailTemplate = _hostingEnvironment.WebRootPath + htmlTemplates["EnvioEmail"]
                    };

                    if (_emailSender.SendEmail(emailDTO))
                    {
                        await timeRepository.ConfirmarEnvioEmail(time.Id);
                    }
                }

                return StatusCode(202);
            }

        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Time))]
        [ProducesResponseType(400)]
        [Route("~/btp/email&ConfirmarEmail={timeId}")]
        public async Task<IActionResult> ConfirmarEmail(string timeId)
        {
            var timeGuid = new Guid(timeId);

            using (var timeRepository = _timeRepository)
            {
                int rows = await timeRepository.ConfirmarEmail(timeGuid);

                if (rows > 0)
                {
                    var htmlTemplates = _config.GetSection("HtmlTemplates");

                    var file = System.IO.File.ReadAllText(_hostingEnvironment.WebRootPath + htmlTemplates["EmailSucesso"]);

                    return new ContentResult
                    {
                        ContentType = "text/html",
                        StatusCode = (int)HttpStatusCode.OK,
                        Content = file
                    };
                }

                return BadRequest(new { error = "Ocorreu um erro ao confirmar sua inscrição" });
            }
        }
    }
}