using FrameworkDigital_DesafioBackEnd.Application.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Entity.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Model.Lead;
using FrameworkDigital_DesafioBackEnd.ORM.Model.Pagination;
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
        public IActionResult GetLeads([FromQuery] PaginationDTO pagination, [FromQuery] GetLeadsFilterDTO filters)
        {            
            var leads = _leadService.GetLeads(pagination, filters);
        
            if (leads.Any())
            {
                return Ok(leads);
            }
            else
            {
                return NotFound("Leads ainda não criadas!");
            }

        }

        [HttpGet("get-lead/{leadId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetLeadById(int leadId)
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
        public IActionResult CreateLead([FromBody] CreateLeadRequest leadRequest)
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
        public IActionResult UpdateLead([FromBody] UpdateLeadRequest leadModel, int leadId)
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
        public IActionResult UpdateLeadStatus([FromBody] UpdateLeadStatusRequest statusRequest, int leadId)
        {
            if (statusRequest == null)
            {
                return BadRequest("Requisição inválida.");
            }


            var updated = _leadService.UpdateLeadStatus(leadId, statusRequest);

            if (updated)
            {
                return Ok($"Lead ID {leadId} atualizado para o status '{statusRequest.Status}'");
            }

            return BadRequest("Falha ao atualizar o status do Lead");
        }

    }
}
