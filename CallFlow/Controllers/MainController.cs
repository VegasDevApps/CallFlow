using CallFlow.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CallFlow.Controllers
{
    public class MainController : ApiController
    {
        ClientsRepository _repo = new ClientsRepository();

        [HttpGet]
        public IHttpActionResult GetClients(string findBy, string findTo)
        {
            string[] allowedCoonditions = { "CITIZENID", "PHONENUMBER" };

            if (
                !allowedCoonditions.Contains(findBy.ToUpper()) ||
                !findTo.All(char.IsDigit))
                return BadRequest("Invalid filter criterias");


            return Ok(_repo.SearchClients(findBy, findTo));
        }

        [HttpPost]
        public IHttpActionResult AddNewClient([FromBody] ClientDataInput clientDataInput)
        {

            if (_repo.CheckIfCitizenIdExists(clientDataInput.CitizenID))
            {
                return BadRequest("This citizen ID already in base!");
            }

            Client client = new Client(clientDataInput);

            if (_repo.AddOrUpdateClient(client))
            {
                return Created(Request.RequestUri + "/" + client.Id.ToString(), client);
            }

            return InternalServerError(new Exception("Unable to save data into database"));
        }

        [HttpPut]
        public IHttpActionResult UpdateClient([FromBody] Client client)
        {

            var target = _repo.GetClientById(client.Id);

            if (target == null) return NotFound();

            target.UpdateProperties(client);

            if (_repo.AddOrUpdateClient(target))
            {
                return Created(Request.RequestUri + "/" + client.Id.ToString(), client);
            }
           
            return InternalServerError(new Exception("Unable to save data into database"));
        }
    }
}
