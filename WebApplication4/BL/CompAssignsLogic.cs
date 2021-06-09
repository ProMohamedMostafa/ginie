using GenieMistro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenieMistro.BL
{
    public class CompAssignsLogic
    {

        private readonly genieDBContext _context;
        public CompAssignsLogic(genieDBContext context)
        {
            _context = context;
        }

        // get all CompAssigns 
        public async Task<List<CompAssign>> GetCompAssigns()
        {
           
            var compAssign = await _context.CompAssigns.ToListAsync();
            return compAssign;
        }
        // Get CompAssign with id
        public async Task<CompAssign> GetCompAssign(int id)
        {
            var compAssign = await _context.CompAssigns.FindAsync(id);
            return compAssign;
        }

        // Put CompAssign
        public async Task<bool> PutCompAssign(int id, CompAssign compAssign)
        {
            //_context.Entry(compAssign).State = EntityState.Modified;
            _context.CompAssigns.Update(compAssign);
            await _context.SaveChangesAsync();

            return true;
        }

        // Post CompAssign
        public async Task<CompAssign> PostCompAssign(CompAssign compAssign)
        {
            await _context.CompAssigns.AddAsync(compAssign);
            await _context.SaveChangesAsync();
            return compAssign;
        }
        // Delete CompAssign with id
        public async Task<bool> DeleteCompAssign(int id)
        {
            var compAssign = await _context.CompAssigns.FindAsync(id);
            if (compAssign == null)
            {
                return false;
            }
            _context.CompAssigns.Remove(compAssign);
            await _context.SaveChangesAsync();
            return true;

        }






        public async Task<bool> DeleteCascadeComp_CompAssigns(int CompId)
        {
            var CompAssigns = await _context.CompAssigns.Where(x => x.CompId == CompId).ToListAsync();
            if (CompAssigns == null)
            {
                return true;
            }
            foreach (Models.CompAssign item in CompAssigns)
            {
                _context.CompAssigns.Remove(item);
                await _context.SaveChangesAsync();
            }


            return true;

        }


        // check if CompAssign Exist
        public bool CompAssignExists(int id)
        {
            var CompAssignExist = _context.CompAssigns.Any(e => e.Id == id);
            if (CompAssignExist == false)
            {
                return false;
            }
            return true;
        }
    }
}
