using GenieMistro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenieMistro.BL
{
    public class MissionsLogic
    {
        private readonly genieDBContext _context;
        public MissionsLogic(genieDBContext context)
        {
            _context = context;
        }

        // get all Missions 
        public async Task<List<Mission>> GetMissions()
        {
            var missions = await _context.Missions.ToListAsync();
            return missions;
        }

        // Get Mission with id
        public async Task<Mission> GetMission(int id)
        {
            var mission = await _context.Missions.FindAsync(id);
            return mission;
        }

        // Update Mission
        public async Task<bool> PutMission(int id, Mission mission)
        {
            //_context.Entry(mission).State = EntityState.Modified;
            _context.Missions.Update(mission);
            await _context.SaveChangesAsync();

            return true;
        }

        // create new  Mission
        public async Task<Mission> PostMission(Mission mission)
        {
            await _context.Missions.AddAsync(mission);
            await _context.SaveChangesAsync();
            return mission;
        }

        // Delete Mission with id
        public async Task<bool> DeleteMission(int id)
        {
            var mission = await _context.Missions.FindAsync(id);
            if (mission == null)
            {
                return false;
            }
            _context.Missions.Remove(mission);
            await _context.SaveChangesAsync();
            return true;

        }

        // check if Mission Exist
        public bool MissionExists(int id)
        {
            var MissionExist = _context.Missions.Any(e => e.Id == id);
            if (MissionExist == false)
            {
                return false;
            }
            return true;
        }

    }
}
