using GenieMistro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenieMistro.BL
{
    public class PurposesLogic
    {
        private readonly genieDBContext _context;
        public PurposesLogic(genieDBContext context)
        {
            _context = context;
        }

        // get all Purposes 
        public async Task<List<Purpose>> GetPurposes()
        {
            var Purpose = await _context.Purpose.ToListAsync();
            return Purpose;
        }

        // Get Purpose with id
        public async Task<Purpose> GetPurpose(int id)
        {
            var Purpose = await _context.Purpose.FindAsync(id);
            return Purpose;
        }

        // Update Purpose
        public async Task<bool> PutPurpose(int id, Purpose purpose)
        {
            //_context.Entry(purpose).State = EntityState.Modified;
            _context.Purpose.Update(purpose);
            await _context.SaveChangesAsync();

            return true;
        }

        // create new  Purpose
        public async Task<Purpose> PostPurpose(Purpose purpose)
        {
            await _context.Purpose.AddAsync(purpose);
            await _context.SaveChangesAsync();
            return purpose;
        }

        // Delete Purpose with id
        public async Task<bool> DeletePurpose(int id)
        {
            var Purpose = await _context.Purpose.FindAsync(id);
            if (Purpose == null)
            {
                return false;
            }
            _context.Purpose.Remove(Purpose);
            await _context.SaveChangesAsync();
            return true;

        }

        // check if Purpose Exist
        public bool PurposeExists(int id)
        {
            var PurposeExist = _context.Purpose.Any(e => e.Id == id);
            if (PurposeExist == false)
            {
                return false;
            }
            return true;
        }


    }
}
