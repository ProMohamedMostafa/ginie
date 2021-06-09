using GenieMistro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenieMistro.BL
{
    public class IndicatorsLogic
    {
        private readonly genieDBContext _context;
        public IndicatorsLogic(genieDBContext context)
        {
            _context = context;
        }

        // get all Indicators 
        public async Task<List<Indicator>> GetIndicators()
        {
            var indicators = await _context.Indicators.ToListAsync();
            return indicators;
        }

        // Get Indicator with id
        public async Task<Indicator> GetIndicator(int id)
        {
            var indicator = await _context.Indicators.FindAsync(id);
            return indicator;
        }

        // Update indicator
        public async Task<bool> PutIndicator(int id, Indicator indicator)
        {
            //_context.Entry(indicator).State = EntityState.Modified;
            _context.Indicators.Update(indicator);
            await _context.SaveChangesAsync();

            return true;
        }

        // create new  Indicator
        public async Task<Indicator> PostIndicator(Indicator indicator)
        {
            await _context.Indicators.AddAsync(indicator);
            await _context.SaveChangesAsync();
            return indicator;
        }

        // Delete Indicator with id
        public async Task<bool> DeleteIndicator(int id)
        {
            var indicator = await _context.Indicators.FindAsync(id);
            if (indicator == null)
            {
                return false;
            }
            _context.Indicators.Remove(indicator);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> DeleteCascadeComp(int CompId)
        {
            var indicators = await  _context.Indicators.Where(x=>x.ComId==CompId).ToListAsync();
            if (indicators == null)
            {
                return true;
            }
            foreach(Models.Indicator item in  indicators)
            {
                _context.Indicators.Remove(item);
                await _context.SaveChangesAsync();
            }
            
          
            return true;

        }

        // check if Indicator Exist
        public bool IndicatorExists(int id)
        {
            var IndicatorExist = _context.Indicators.Any(e => e.Id == id);
            if (IndicatorExist == false)
            {
                return false;
            }
            return true;
        }
    }
}
