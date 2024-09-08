﻿using Application.Interfaces;
using Application.Response;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Querys
{
    public class InteractionTypesQuery : IInteractionTypesQuery
    {
        private readonly AppDbContext _context;

        public InteractionTypesQuery(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<GenericResponse>> GetAll()
        {
            var interactionTypes = await _context.InteractionTypes.ToListAsync();
            ICollection<GenericResponse> list = new List<GenericResponse>();
            foreach (var x in interactionTypes)
            {
                GenericResponse res = new GenericResponse()
                {
                    Id = x.Id,
                    Name = x.Name,
                };
                list.Add(res);


            };

            return list;
        }

        public async Task<bool> existe(int id)
        {
            var exist = await _context.InteractionTypes.AnyAsync(c => c.Id == id);

            if (!exist)
            {
                throw new InvalidOperationException("El id introducido para InteractionTypes no coincide con ningun registro");

            }
            return exist;
        }

    }
}