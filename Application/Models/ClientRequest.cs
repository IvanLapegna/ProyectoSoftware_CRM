using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ClientRequest
    {
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }

        public string ClientPhone { get; set; }
        public string ClientCompany { get; set; }

        public string ClientAddress { get; set; }

        public void validacion()
        {
            if (string.IsNullOrWhiteSpace(ClientName))
            {
                throw new ArgumentNullException(nameof(ClientName));
            }
            else if (string.IsNullOrWhiteSpace(ClientEmail))
            {
                throw new ArgumentNullException(nameof(ClientEmail));
            }
            else if (string.IsNullOrWhiteSpace(ClientPhone))
            {
                throw new ArgumentNullException(nameof(ClientPhone));
            }
            else if (string.IsNullOrWhiteSpace(ClientCompany))
            {
                throw new ArgumentNullException(nameof(ClientCompany));
            }
            else if (string.IsNullOrWhiteSpace(ClientAddress))
            {
                throw new ArgumentNullException(nameof(ClientAddress));
            }
        }

        public void validaciones()
        {
            string respuesta = null;
            var valores = new Dictionary<string, string> { { nameof(ClientName), ClientName }, { nameof(ClientEmail), ClientEmail } };

            foreach (var property in valores)
            {
                if (string.IsNullOrWhiteSpace(property.Value))
                {
                    respuesta += $"{property.Key} ";
                }
            }

            if (!string.IsNullOrEmpty(respuesta)) 
            {
                throw new ArgumentNullException(respuesta);
            
            
            }
        }
    }
}
