using Microsoft.AspNetCore.Mvc;
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
    [Route("/api/v1/users")]
    public class UserController : ControllerBase
    {
        private readonly CredentialsController _userController;

        public UserController(CredentialsController userController)
        {
            _userController = userController;
        }

        /// <summary>
        /// Вывод информации о пользователях
        /// </summary>
        /// <param name="login">Найти по логину</param>
        /// <returns>Пользователи</returns>
        /// <response code="200">Успешно выведено</response>
        /// <response code="502">Ошибка БД</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<UserDTO>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status502BadGateway)]
        public IActionResult GetAll([FromQuery] string? login = null)
        {
            List<User> users = null;
            Error error;
            if (login == null)
            {
                (users, error) = _userController.FindAllUsers();
            }
            else
            {
                User user = null;
                (user, error) = _userController.FindUserByLogin(login);
                if (user != null)
                {
                    users = new List<User>();
                    users.Add(user);
                }
            }

            if (error == Error.Internal || users == null)
            {
                return StatusCode(StatusCodes.Status502BadGateway);
            }

            return Ok(users.Select(c => UserMapperDTO.MapToDto(c)).ToList());
        }


        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        /// <param name="userCreateDTO">Пользователь</param>
        /// <response code="200">Успешно добавлено</response>
        /// <response code="409">Пользователь уже есть</response>
        /// <response code="500">Не добавилось в БД без ошибки БД</response>
        /// <response code="502">Ошибка БД</response>
        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status201Created)]
        [ProducesResponseType(statusCode: StatusCodes.Status409Conflict)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(statusCode: StatusCodes.Status502BadGateway)]
        public IActionResult Add([FromBody] UserCreateDTO userCreateDTO)
        {
            Error error;
            User user;
            (user, error) = _userController.FindUserByLogin(userCreateDTO.Username);
            if (error == Error.Internal)
            {
                return StatusCode(StatusCodes.Status502BadGateway);
            }
            if (user != null)
            {
                return StatusCode(StatusCodes.Status409Conflict);
            }

            error = _userController.CreateUser(userCreateDTO.Username, userCreateDTO.Password, userCreateDTO.Role);
            if (error == Error.Internal)
            {
                return StatusCode(StatusCodes.Status502BadGateway);
            }

            (user, error) = _userController.FindUserByLogin(userCreateDTO.Username);
            if (error == Error.Internal)
            {
                return StatusCode(StatusCodes.Status502BadGateway);
            }

            if (user == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        /// <summary>
        /// Удваление пользователя
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <response code="200">Удалено</response>
        /// <response code="500">Не удалилось из БД без ошибки БД</response>
        /// <response code="502">Ошибка БД</response>
        [HttpDelete]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(statusCode: StatusCodes.Status502BadGateway)]
        public IActionResult Delete([FromQuery] string login)
        {
            Error error;
            User user;
            (user, error) = _userController.FindUserByLogin(login);
            if (error == Error.Internal)
            {
                return StatusCode(StatusCodes.Status502BadGateway);
            }
            if (user == null)
            {
                return StatusCode(StatusCodes.Status200OK);
            }

            error = _userController.DeleteUser(login);
            if (error == Error.Internal)
            {
                return StatusCode(StatusCodes.Status502BadGateway);
            }

            (user, error) = _userController.FindUserByLogin(login);
            if (error == Error.Internal)
            {
                return StatusCode(StatusCodes.Status502BadGateway);
            }

            if (user != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        /// <summary>
        /// Изменение роли пользователя
        /// </summary>
        /// <param name="login">Логин изменяемого пользователя</param>
        /// /// <param name="role">Новая роль</param>
        /// <response code="200">Удалено</response>
        /// <response code="500">Не изменилась роль без ошибки БД</response>
        /// <response code="502">Ошибка БД</response>
        [HttpPatch]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(statusCode: StatusCodes.Status502BadGateway)]
        public IActionResult ChangeUser([FromQuery] string login, [FromQuery] Role role)
        {
            Error error;
            error = _userController.GrantUserRole(login, role);
            if (error == Error.Internal)
            {
                return StatusCode(StatusCodes.Status502BadGateway);
            }

            User user;
            (user, error) = _userController.FindUserByLogin(login);
            if (error == Error.Internal)
            {
                return StatusCode(StatusCodes.Status502BadGateway);
            }

            if (user == null || role != user.Role)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }
    }
}
