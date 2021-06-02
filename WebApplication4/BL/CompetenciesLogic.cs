using GenieMistro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenieMistro.BL
{
    public class CompetenciesLogic
    {
        private readonly genieDBContext _context;
        public CompetenciesLogic(genieDBContext context)
        {
            _context = context;
        }

        // get all Competencies 
        public async Task<List<Competency>> GetCompetencies()
        {
            var competencies = await _context.Competency.ToListAsync();
            return competencies;
        }

        // Get Competency with id
        public async Task<Competency> GetCompetency(int id)
        {
            var competency = await _context.Competency.FindAsync(id);
            return competency;
        }

        // UPdate Competency
        public async Task<bool> PutCompetency(int id, Competency competency)
        {
            //_context.Entry(competency).State = EntityState.Modified;
            _context.Competency.Update(competency);
            await _context.SaveChangesAsync();

            return true;
        }

        // create new  Competency
        public async Task<Competency> PostCompetency(Competency competency)
        {
            await _context.Competency.AddAsync(competency);
            await _context.SaveChangesAsync();
            return competency;
        }

        // Delete Competency with id
        public async Task<bool> DeleteCompetency(int id)
        {
            var competency = await _context.Competency.FindAsync(id);
            if (competency == null)
            {
                return false;
            }
            _context.Competency.Remove(competency);
            await _context.SaveChangesAsync();
            return true;

        }

        // check if Competency Exist
        public bool CompetencyExists(int id)
        {
            var CompetencyExists = _context.Competency.Any(e => e.Id == id);
            if (CompetencyExists == false)
            {
                return false;
            }
            return true;
        }

    }
}
