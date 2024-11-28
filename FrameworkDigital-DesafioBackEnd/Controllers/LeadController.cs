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

        /// <summary>
        /// Criar pedido de pagamento
        /// </summary>
        /// <param name="request">Dados</param>
        /// <response code="200">dentificador do pedido (OrderId)</response>
        /// <returns>Identificador do pedido</returns>I
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


        /// <summary>
        /// Retorna uma Lead específica pelo seu identificador.
        /// </summary>
        /// <param name="leadId">ID da Lead que será buscada.</param>
        /// <returns>A Lead correspondente ao ID fornecido ou uma mensagem de erro caso não seja encontrada.</returns>
        /// <response code="200">Lead retornada com sucesso.</response>
        /// <response code="400">Lead não encontrada.</response>
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

        /// <summary>
        /// Cria uma nova Lead.
        /// </summary>
        /// <param name="leadRequest">Dados necessários para criar uma nova Lead.</param>
        /// <returns>A Lead criada ou uma mensagem de erro.</returns>
        /// <response code="200">Lead criada com sucesso.</response>
        /// <response code="400">Erro ao criar a Lead.</response>
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


        /// <summary>
        /// Atualiza uma Lead existente pelo ID.
        /// </summary>
        /// <param name="leadModel">Dados atualizados da Lead.</param>
        /// <param name="leadId">ID da Lead que será atualizada.</param>
        /// <returns>A Lead atualizada ou uma mensagem de erro.</returns>
        /// <response code="200">Lead atualizada com sucesso.</response>
        /// <response code="400">Erro ao atualizar a Lead.</response>
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


        /// <summary>
        /// Atualiza apenas o status de uma Lead existente.
        /// </summary>
        /// <param name="statusRequest">Novo status da Lead.</param>
        /// <param name="leadId">ID da Lead que terá o status atualizado.</param>
        /// <returns>Mensagem de confirmação ou uma mensagem de erro.</returns>
        /// <response code="200">Status da Lead atualizado com sucesso.</response>
        /// <response code="400">Erro ao atualizar o status da Lead.</response>
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
