using GenieMistro.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenieMistro.BL
{
    public class TbEmployeesLogic
    {
        private readonly genieDBContext _context;
        public TbEmployeesLogic(genieDBContext context)
        {
            _context = context;
        }

        // get all tbEmployees 
        public async Task<List<TbEmployee>> GetTbEmployees()
        {
            var tbEmployees = await _context.TbEmployee.ToListAsync();
            return tbEmployees;
        }

        // Get tbEmployee with id
        public async Task<TbEmployee> GetTbEmployee(int id)
        {
            var tbEmployee = await _context.TbEmployee.FindAsync(id);
            return tbEmployee;
        }

        // Update TbEmployee
        public async Task<bool> PutTbEmployee(int id, TbEmployee tbEmployee)
        {
            //_context.Entry(tbEmployee).State = EntityState.Modified;
            _context.TbEmployee.Update(tbEmployee);
            await _context.SaveChangesAsync();

            return true;
        }

        // create new  TbEmployee
        public async Task<TbEmployee> PostTbEmployee(TbEmployee tbEmployee)
        {
            await _context.TbEmployee.AddAsync(tbEmployee);
            await _context.SaveChangesAsync();
            return tbEmployee;
        }

        // Delete TbEmployee with id
        public async Task<bool> DeleteTbEmployee(int id)
        {
            var tbEmployee = await _context.TbEmployee.FindAsync(id);
            if (tbEmployee == null)
            {
                return false;
            }
            _context.TbEmployee.Remove(tbEmployee);
            await _context.SaveChangesAsync();
            return true;

        }

        // check if TbEmployees Exist
        public bool TbEmployeeExists(int id)
        {
            var TbEmployeeExist = _context.TbEmployee.Any(e => e.Id == id);
            if (TbEmployeeExist == false)
            {
                return false;
            }
            return true;
        }

        // get max TbEmployees 
        public int GetLevelsCount()
        {
            int max = 0;
            max = (int)_context.TbEmployee.Max(e => e.EmpTitleLevel);

            return max;
        }

    }
}
