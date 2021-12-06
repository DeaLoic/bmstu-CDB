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
        /// <response code="404">Записей нет</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<UserDTO>), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        public IActionResult GetAll([FromQuery] string? login = null)
        {
            List<User> users = null;
            if (login == null)
            {
                users = _userController.FindAllUsers();
            }
            else
            {
                var user = _userController.FindUserByLogin(login);
                if (user != null)
                {
                    users = new List<User>();
                    users.Add(user);
                }
            }


            if (users == null)
            {
                return NotFound();
            }

            return Ok(users.Select(c => UserMapperDTO.MapToDto(c)).ToList());
        }

        
        /// <summary>
        /// Добавление нового пользователя
        /// </summary>
        /// <param name="userDTO">Пользователь</param>
        /// <response code="200">Успешно проведено</response>
        [HttpPost]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult Add([FromBody] UserDTO userDTO)
        {
            _userController.CreateUser(userDTO.Username, userDTO.Password, RoleExtension.IntToEnum(userDTO.Role));
            var user = _userController.FindUserByLogin(userDTO.Username);
            if (user == null)
            {
                BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Удваление пользователя
        /// </summary>
        /// <param name="userDTO">Пользователь</param>
        /// <response code="200">Успешно проведено</response>
        /// <response code="400">Плохой запрос</response>
        [HttpDelete]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult Delete([FromQuery] string login)
        {
            _userController.DeleteUser(login);
            var user = _userController.FindUserByLogin(login);
            if (user != null)
            {
                BadRequest();
            }

            return Ok();
        }

        /// <summary>
        /// Изменение роли пользователя
        /// </summary>
        /// <param name="userDTO">Экземпляр изменяемого пользователя</param>
        /// <response code="200">Успешно изменено</response>
        /// <response code="404">Такой записи нет</response>
        [HttpPatch]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        public IActionResult ChangeUser([FromBody] UserDTO userDTO)
        {
            var role = RoleExtension.IntToEnum(userDTO.Role);
            _userController.GrantUserRole(userDTO.Username, RoleExtension.IntToEnum(userDTO.Role));
            var user = _userController.FindUserByLogin(userDTO.Username);
            if (user == null || role != user.Role)
            {
                BadRequest();
            }

            return Ok();
        }
    }
}
