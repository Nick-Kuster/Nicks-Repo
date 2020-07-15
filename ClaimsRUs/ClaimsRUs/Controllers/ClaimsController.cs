using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClaimsRUs.Entity;
using ClaimsRUs.Entity.Models;
using ClaimsRUs.Data.Abstractions.Readers;
using ClaimsRUs.Models;
using ClaimsRUs.Data.ViewModels;
using ClaimsRUs.Data.Abstractions.Models;
using System.Collections.Generic;
using ClaimsRUs.Data.Abstractions.Writers;

namespace ClaimsRUs.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly Context _context;
        private readonly IClaimsReader _claimsReader;
        private readonly IContactsReader _contactsReader;
        private readonly IVehiclesReader _vehiclesReader;
        private readonly IClaimsWriter _claimsWriter;

        public ClaimsController(Context context, IClaimsReader claimsReader, IContactsReader contactsReader, IVehiclesReader vehiclesReader, IClaimsWriter claimsWriter)
        {
            _context = context;
            _claimsReader = claimsReader;
            _contactsReader = contactsReader;
            _vehiclesReader = vehiclesReader;
            _claimsWriter = claimsWriter;
        }

        // GET: Claims
        public IActionResult Index()
        {
            var claims = _claimsReader.ReadAll();
            return View(claims);
        }

        // GET: Claims/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.claim
                .FirstOrDefaultAsync(m => m.ClaimId == id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

        // GET: Claims/Create
        public IActionResult Create()
        {
            var contactList = _contactsReader.ReadAll().ToList();
            var vehicleList = _vehiclesReader.ReadAll().ToList();
            CreateClaimViewModel vm = new CreateClaimViewModel()
            {
                Claim = new ClaimViewModel(),
                ContactList = contactList,
                VehicleList = vehicleList
            };
            return View(vm);
        }

        // POST: Claims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClaimViewModel claim, Guid selectedContactId, Guid selectedVehicleId)
        {

            if (ModelState.IsValid)
            {
                var contactVehicleList = new List<ContactVehicleViewModel>() { new ContactVehicleViewModel() { ContactId = selectedContactId, VehicleId = selectedVehicleId } };
                claim.ContactVehicles = contactVehicleList;
                _claimsWriter.Write(claim);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Claims/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.claim.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }
            return View(claim);
        }

        // POST: Claims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ClaimId,DateCreated,DateOfClaim,Description")] Claim claim)
        {
            if (id != claim.ClaimId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(claim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClaimExists(claim.ClaimId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(claim);
        }

        // GET: Claims/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.claim
                .FirstOrDefaultAsync(m => m.ClaimId == id);
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

        // POST: Claims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var claim = await _context.claim.FindAsync(id);
            _context.claim.Remove(claim);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClaimExists(Guid id)
        {
            return _context.claim.Any(e => e.ClaimId == id);
        }
    }
}
