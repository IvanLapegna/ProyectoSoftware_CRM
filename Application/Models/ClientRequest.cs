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
            var valores = new Dictionary<string, string> { { nameof(name), name }, { nameof(email), email }, { nameof(company), company }, { nameof(address), address } };

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

            if (!IsValidPhoneNumber(phone))
            {
                throw new InvalidOperationException("Formato de numero no valido");
            }


        }

        private bool IsValidPhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return false;
            }

            // Expresión regular para validar el número de teléfono (solo dígitos y longitud entre 7 y 15)
            var regex = new System.Text.RegularExpressions.Regex(@"^\d{7,15}$");
            return regex.IsMatch(phone);
        }
    }
}
