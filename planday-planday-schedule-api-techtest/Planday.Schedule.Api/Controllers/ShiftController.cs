using Microsoft.AspNetCore.Mvc;
using Planday.Schedule.Api.Models;
using Planday.Schedule.Commands;
using Planday.Schedule.Queries;

namespace Planday.Schedule.Api.Controllers
{
    [ApiController]
    [Route("shift")]
    public class ShiftController : ControllerBase
    {
        private IGetShiftByIdQuery _query;
        private ICreateOpenShift _command;
        private IAssignShiftToEmployeeCommand _assignShiftToEmployeeCommand;
        public ShiftController(IGetShiftByIdQuery query, ICreateOpenShift command, IAssignShiftToEmployeeCommand assignShiftToEmployeeCommand)
        {
            _query = query;
            _command = command;
            _assignShiftToEmployeeCommand = assignShiftToEmployeeCommand;

        }

        [HttpGet("{id}")]
        public async Task<object> GetShiftById(int id) 
        {
            return await _query.QueryByIdAsync(id);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOpenShift([FromBody] ShiftDto shift)
        {

            var validator = new ShiftValidator();
            var validationResult = validator.Validate(shift);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.FirstOrDefault()?.ErrorMessage);
            }

            var result = await _command.CreateOpenShiftAsync(shift);
            if (result)
            {
                return Ok();
            }
            return BadRequest(result + " Shift is empty");
        }

        [HttpPost("{shiftId}/{employeeId}")]
        public async Task<IActionResult> AssignShiftToEmployee(int shiftId, int employeeId)
        {
            var result = await _assignShiftToEmployeeCommand.AssignShiftToEmployee(shiftId, employeeId);

            switch (result)
            {
                case 200: return Ok();
                case 400: return BadRequest("You cannot assign the same shift to two or more employees.");
                case 404: return BadRequest("Employee ot Shift was not found");
                case 599: return BadRequest("The employee must not have overlapping shifts.");
                    default: return BadRequest();
            }
        }
    }    
}

