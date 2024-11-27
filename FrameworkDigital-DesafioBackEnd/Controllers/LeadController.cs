﻿using FrameworkDigital_DesafioBackEnd.Application.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Entity.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Model.Lead;
using Microsoft.AspNetCore.Mvc;

namespace FrameworkDigital_DesafioBackEnd.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class LeadController : ControllerBase
    {
        private readonly ILeadService _leadService;
        private readonly IConfiguration _configuration;

        public LeadController(ILeadService leadService, IConfiguration configuration)
        {
            _leadService = leadService;            
            _configuration = configuration;
        }
        [HttpGet("get-leads")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetLeads(
          int page = 1,
          int pageSize = 10)
        {
            var leads = _leadService.GetLeads();
            if (leads != null) {

                return Ok(leads);
            }
            else
            {
                return BadRequest("Lead não encontrada");
            }

        }

        [HttpGet("get-lead/{leadId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetUnitById(int leadId)
        {
            var lead = _leadService.GetLeadById(leadId);

            if (lead != null)
            {
                return Ok(lead);
            }
            else
            {
                return BadRequest("Lead não encontrada");
            }
        }

        [HttpPost("create-lead")]        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateUnit([FromBody] LeadRequest leadRequest)
        {

            if (leadRequest == null)
            {
                return BadRequest("Dados inválidos!");
            }
            var createdLead = _leadService.CreateLead(leadRequest);
            if (createdLead != null)
            {
                return Ok(leadRequest);
            }
            else
            {
                return BadRequest("Erro ao criar nova Lead");
            }
        }

        [HttpPut("update-lead/{leadId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateUnit([FromBody] LeadRequest leadModel, int leadId)
        {
            if (leadModel == null)
            {
                return BadRequest("Dados inválidos");
            }
            LeadModel updatedLead = _leadService.UpdateLead(leadId, leadModel);
            if (updatedLead != null)
            {
                return Ok(updatedLead);
            }
            return BadRequest("Falha ao atualizar");

        }

        [HttpPatch("update-lead-status/{leadId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateLeadStatus(int leadId, [FromBody] string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return BadRequest("Status inválido");
            }

            var updated = _leadService.UpdateLeadStatus(leadId, status);

            if (updated)
            {
                return Ok($"Lead ID {leadId} atualizado para o status '{status}'");
            }

            return BadRequest("Falha ao atualizar o status do Lead");
        }


    }
}
