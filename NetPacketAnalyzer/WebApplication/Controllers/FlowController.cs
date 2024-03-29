﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DTO;
using WebApplication.DataMappers;
using DataObjects.Models;
using DataObjects.Enums;
using ModelLogic.Controllers;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("/api/v1/flows")]
    public class FlowController : ControllerBase
    {
        private readonly FlowsRawController _flowController;

        public FlowController(FlowsRawController flowController)
        {
            _flowController = flowController;
        }

        /// <summary>
        /// Вывод информации о потоках
        /// </summary>
        /// <param name="from">Временная метка "от"</param>
        /// <param name="to">Временная метка "до"</param>
        /// <param name="bytes_min">Временная метка "от"</param>
        /// <param name="bytes_max">Временная метка "до"</param>
        /// <param name="src">Временная метка "от"</param>
        /// <param name="dst">Временная метка "до"</param>
        /// <returns>Потоки</returns>
        /// <response code="200">Успешно выведено</response>
        /// <response code="502">Внутренняя необрабатываемая ошибка БД</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<FlowDTO>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status502BadGateway)]
        public IActionResult GetAll([FromQuery] DateTime? from = null, [FromQuery] DateTime? to = null,
                                    [FromQuery] int? bytes_min = null, [FromQuery] int? bytes_max = null,
                                    [FromQuery] string? src = null, [FromQuery] string? dst = null)
        {
            List<Flow> flows = null;
            FlowFilters filters = new FlowFilters(MinTimeFlowStart: from, MaxTimeFlowStart: to,
                                                  MinBytes: bytes_min, MaxBytes: bytes_max,
                                                  SrcAddr: src, DstAddr: dst);

            Error error;
            (flows, error) = _flowController.FindFiltered(filters);
            if (error == Error.Internal || flows == null)
            {
                return StatusCode(StatusCodes.Status502BadGateway);
            }

            return Ok(flows.Select(c => FlowMapperDTO.MapToDto(c)).ToList());
        }
    }
}
