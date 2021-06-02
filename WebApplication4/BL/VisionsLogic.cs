using GenieMistro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenieMistro.BL
{
    public class VisionsLogic
    {
        private readonly genieDBContext _context;
        public VisionsLogic(genieDBContext context)
        {
            _context = context;
        }

        // get all Visions 
        public async Task<List<Vision>> GetVisions()
        {
            var Vision = await _context.Vision.ToListAsync();
            return Vision;
        }

        // Get Vision with id
        public async Task<Vision> GetVision(int id)
        {
            var vision = await _context.Vision.FindAsync(id);
            return vision;
        }

        // Update Vision
        public async Task<bool> PutVision(int id, Vision vision)
        {
            //_context.Entry(vision).State = EntityState.Modified;
            _context.Vision.Update(vision);
            await _context.SaveChangesAsync();

            return true;
        }

        // create new  Vision
        public async Task<Vision> PostVision(Vision vision)
        {
            await _context.Vision.AddAsync(vision);
            await  _context.SaveChangesAsync();
            return vision;
        }

        // Delete Vision with id
        public async Task<bool> DeleteVision(int id)
        {
            var vision = await _context.Vision.FindAsync(id);
            if (vision == null)
            {
                return false;
            }
            _context.Vision.Remove(vision);
            await _context.SaveChangesAsync();
            return true;

        }

        // check if vision Exist
        public bool VisionExists(int id)
        {
            var VisionsExist = _context.Vision.Any(e => e.Id == id);
            if (VisionsExist == false)
            {
                return false;
            }
            return true;
        }

    }
}
