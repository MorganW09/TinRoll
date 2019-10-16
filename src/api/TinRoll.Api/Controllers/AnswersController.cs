﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TinRoll.Api.ApiErrors;
using TinRoll.Logic.Managers.Interfaces;
using TinRoll.Shared.Dtos;

namespace TinRoll.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswerManager _answerManager;
        
        public AnswersController(IAnswerManager answerManager)
        {
            _answerManager = answerManager;
        }

        [HttpGet]
        public async Task<IEnumerable<AnswerDto>> GetAnswers()
        {
            var answers = await _answerManager.GetAnswersAsync();
            return answers;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAnswer(int id)
        {
            var answer = await _answerManager.GetAnswerAsync(id);

            if (answer == null)
            {
                return NotFound(new NotFoundError("The answer was not found"));
            }

            return Ok(answer);
        }


        [HttpPost]
        public async Task<AnswerDto> CreateAnswer(AnswerDto answer)
        {
            var newAnswer = await _answerManager.CreateAnswerAsync(answer);
            return newAnswer;
        }
    }
}