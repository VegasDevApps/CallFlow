using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace CallFlow.DAL
{
    public class ClientsRepository
    {
        public bool AddOrUpdateClient(Client client)
        {
            using (var db = new ClientsBaseEntities())
            {
                try
                {
                    db.Clients.AddOrUpdate(client);
                    return (db.SaveChanges() > 0);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        }

        public bool DeleteClient(Client client)
        {
            using (var db = new ClientsBaseEntities())
            {
                try
                {
                    db.Clients.Remove(client);
                    return (db.SaveChanges() > 0);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        }

        public bool CheckIfCitizenIdExists(string citizenId)
        {
            using (var db = new ClientsBaseEntities())
            {
                try
                {
                    var citizen = db.Clients.FirstOrDefault(c => c.CitizenID == citizenId);
                    return (citizen != null);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        }

        public List<Client> SearchClients(string conditionType, string condition)
        {
            using (var db = new ClientsBaseEntities())
            {
                try
                {
                    return db.Clients.Where(GetSearchCondition(condition, conditionType)).ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        }

        public Client GetClientById(int  id)
        {
            using (var db = new ClientsBaseEntities())
            {
                try
                {
                    return db.Clients.FirstOrDefault(c => c.Id == id);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message, ex);
                }
            }
        }

        private Func<Client, bool> GetSearchCondition(string condition, string conditionType)
        {

             switch (conditionType.ToUpper())
            {
                case "CITIZENID":
                    return c => c.CitizenID.Contains(condition);
                case "PHONENUMBER":
                    return c => c.PhoneNumber.Contains(condition);
                default: throw new Exception("Invalid search condition");
            };
        }
    }
}