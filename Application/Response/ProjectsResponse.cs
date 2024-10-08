﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Response
{
    public class ProjectsResponse
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public ClientsResponse Client { get; set; }
        public GenericResponse CampaignType { get; set; }




    }



}
