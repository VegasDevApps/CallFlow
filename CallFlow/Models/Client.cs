using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CallFlow
{
    public partial class Client
    {
        public Client() { }

        public Client(ClientDataInput clientDataInput) 
        { 
            this.FromInputToClient(clientDataInput);
        }

        public void UpdateProperties(Client client) 
        {
            this.CitizenID = client.CitizenID;
            this.PhoneNumber = client.PhoneNumber;
            this.FirstName = client.FirstName;
            this.LastName = client.LastName;
            this.BitrthDate = client.BitrthDate;
            this.Notes = client.Notes;
        }
        private void FromInputToClient(ClientDataInput clientDataInput)
        {
            this.CitizenID = clientDataInput.CitizenID;
            this.PhoneNumber = clientDataInput.PhoneNumber;
            this.FirstName = clientDataInput.FirstName;
            this.LastName = clientDataInput.LastName;
            this.BitrthDate = clientDataInput.BitrthDate;
            this.Notes = clientDataInput.Notes;
        }
    }
}