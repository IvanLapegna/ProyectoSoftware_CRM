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
        public string name { get; set; }
        public string email { get; set; }

        public string company { get; set; }
        public string phone { get; set; }

        public string address { get; set; }


        public void validaciones()
        {
            string respuesta = null;
            var valores = new Dictionary<string, string> { { nameof(name), name }, { nameof(email), email } };

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
