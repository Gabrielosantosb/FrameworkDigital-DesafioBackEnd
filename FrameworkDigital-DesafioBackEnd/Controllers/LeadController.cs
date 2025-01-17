﻿using FrameworkDigital_DesafioBackEnd.Application.Lead;
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetLeads([FromQuery] PaginationDTO pagination, [FromQuery] GetLeadsFilterDTO filters)
        {
            try
            {
                var (leads, totalCount) = _leadService.GetLeads(pagination, filters);
                
                return Ok(new
                {
                    totalCount,
                    leads
                });
            }
            catch (Exception ex)
            {                
                return BadRequest(new
                {
                    message = "Erro ao processar a requisição.",
                    error = ex.Message
                });
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
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public IActionResult UpdateLeadStatus([FromBody] UpdateLeadStatusRequest statusRequest, int leadId)
{
    if (statusRequest == null)
    {
        return BadRequest(new { Message = "Requisição inválida.", Success = false });
    }

    try
    {
        if (_leadService.UpdateLeadStatus(leadId, statusRequest))
        {
            return Ok(new { Message = $"Lead ID {leadId} atualizado para o status '{statusRequest.Status}'", Success = true });
        }

        return BadRequest(new { Message = "Falha ao atualizar o status do Lead.", Success = false });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { Message = "Erro interno ao atualizar o Lead.", Success = false, Error = ex.Message });
    }
}

    }
}
