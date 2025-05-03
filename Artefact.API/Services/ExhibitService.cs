using Artefact.API.Data;
using Artefact.API.Data.Dtos.Exhibit;
using Artefact.API.Data.Dtos.Material;
using Artefact.API.Data.Models;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class ExhibitService : IExhibitService
    {
        private readonly BaseDbContext _context;
        // Add IMapper _mapper if using AutoMapper

        public ExhibitService(BaseDbContext context /*, IMapper mapper */)
        {
            _context = context;
            // _mapper = mapper;
        }

        public async Task<ExhibitReadModel> CreateAsync(ExhibitCreateModel dto)
        {
            var exhibit = new Exhibit
            {
                RBnumber = dto.RBnumber,
                IdInventoryCypher = dto.IdInventoryCypher,
                InventoryNumber = dto.InventoryNumber,
                NameExhibit = dto.NameExhibit,
                IdAuthor = dto.IdAuthor,
                PeriodYearCreated = dto.PeriodYearCreated,
                Length = dto.Length,
                Width = dto.Width,
                Height = dto.Height,
                IdMeasure = dto.IdMeasure,
                IdTechnique = dto.IdTechnique,
                IdRecievingWay = dto.IdRecievingWay,
                CommonDescription = dto.CommonDescription,
                DamageDescription = dto.DamageDescription,
                IdStoringType = dto.IdStoringType,
                Picture = dto.Picture,
                DateRecordCreated = DateTime.Now,
                DateRecordLastUpdated = DateTime.Now
            };

            // Handle Many-to-Many MaterialExhibits
            if (dto.MaterialIds != null && dto.MaterialIds.Any())
            {
                exhibit.MaterialExhibits = dto.MaterialIds.Select(materialId => new MaterialExhibit { IdMaterial = materialId }).ToList();
            }

            _context.Exhibits.Add(exhibit);
            await _context.SaveChangesAsync();

            // Reload to include related data
            var createdExhibit = await _context.Exhibits
                                   .Include(e => e.InventoryCypher)
                                   .Include(e => e.Author)
                                   .Include(e => e.Measure)
                                   .Include(e => e.Technique)
                                   .Include(e => e.RecievingWay)
                                   .Include(e => e.StoringType)
                                   .Include(e => e.MaterialExhibits).ThenInclude(me => me.Material)
                                   .FirstOrDefaultAsync(e => e.Id == exhibit.Id);

            return MapToReadModel(createdExhibit);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var exhibit = await _context.Exhibits.FindAsync(id);
            if (exhibit == null) return false;
            // Associated MaterialExhibits and ExhibitDocuments usually handled by cascade or manual removal
            _context.Exhibits.Remove(exhibit);
            return await _context.SaveChangesAsync() > 0;
        }

        // Implementation includes optional filtering by storingTypeId
        public async Task<IEnumerable<ExhibitReadModel>> GetAllAsync(int? storingTypeId = null)
        {
            var query = _context.Exhibits
                           .Include(e => e.InventoryCypher)
                           .Include(e => e.Author)
                           .Include(e => e.Measure)
                           .Include(e => e.Technique)
                           .Include(e => e.RecievingWay)
                           .Include(e => e.StoringType)
                           .Include(e => e.MaterialExhibits).ThenInclude(me => me.Material)
                           .AsQueryable(); // Start as queryable

            if (storingTypeId.HasValue)
            {
                query = query.Where(e => e.IdStoringType == storingTypeId.Value);
            }

            return await query.Select(e => MapToReadModel(e)).ToListAsync(); // Execute query
        }

        public async Task<ExhibitReadModel?> GetByIdAsync(int id)
        {
            var exhibit = await _context.Exhibits
                                    .Include(e => e.InventoryCypher)
                                    .Include(e => e.Author)
                                    .Include(e => e.Measure)
                                    .Include(e => e.Technique)
                                    .Include(e => e.RecievingWay)
                                    .Include(e => e.StoringType)
                                    .Include(e => e.MaterialExhibits).ThenInclude(me => me.Material)
                                    .FirstOrDefaultAsync(e => e.Id == id);

            return exhibit == null ? null : MapToReadModel(exhibit);
        }

        public async Task<bool> UpdateAsync(int id, ExhibitUpdateModel dto)
        {
            var exhibit = await _context.Exhibits
                                   .Include(e => e.MaterialExhibits) // Include for update
                                   .FirstOrDefaultAsync(e => e.Id == id);

            if (exhibit == null) return false;

            // Update properties - handle nullables carefully
            if (dto.RBnumber != null) exhibit.RBnumber = dto.RBnumber;
            exhibit.IdInventoryCypher = dto.IdInventoryCypher;
            if (dto.InventoryNumber != null) exhibit.InventoryNumber = Convert.ToInt32(dto.InventoryNumber); // Careful conversion
            if (dto.NameExhibit != null) exhibit.NameExhibit = dto.NameExhibit;
            if (dto.IdAuthor.HasValue) exhibit.IdAuthor = dto.IdAuthor.Value;
            if (dto.PeriodYearCreated != null) exhibit.PeriodYearCreated = dto.PeriodYearCreated;
            if (dto.Length.HasValue) exhibit.Length = dto.Length.Value;
            if (dto.Width.HasValue) exhibit.Width = dto.Width.Value;
            exhibit.Height = dto.Height;
            if (dto.IdMeasure.HasValue) exhibit.IdMeasure = dto.IdMeasure.Value;
            if (dto.IdTechnique.HasValue) exhibit.IdTechnique = dto.IdTechnique.Value;
            exhibit.IdRecievingWay = dto.IdRecievingWay;
            if (dto.CommonDescription != null) exhibit.CommonDescription = dto.CommonDescription;
            if (dto.DamageDescription != null) exhibit.DamageDescription = dto.DamageDescription;
            if (dto.IdStoringType.HasValue) exhibit.IdStoringType = dto.IdStoringType.Value;
            if (dto.Picture != null) exhibit.Picture = dto.Picture; // Handle file storage appropriately

            exhibit.DateRecordLastUpdated = DateTime.UtcNow;

            // Handle Many-to-Many MaterialExhibits update
            if (dto.MaterialIds != null)
            {
                // Remove existing relations not in the new list
                exhibit.MaterialExhibits.RemoveAll(me => !dto.MaterialIds.Contains(me.IdMaterial));
                // Add new relations from the list
                var existingMaterialIds = exhibit.MaterialExhibits.Select(me => me.IdMaterial).ToList();
                var newMaterialIds = dto.MaterialIds.Except(existingMaterialIds);
                foreach (var materialId in newMaterialIds)
                {
                    exhibit.MaterialExhibits.Add(new MaterialExhibit { IdMaterial = materialId });
                }
            } // If dto.MaterialIds is null, relations are untouched

            _context.Entry(exhibit).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Exhibits.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }

        // Helper method for mapping Exhibit to ExhibitReadModel
        private static ExhibitReadModel MapToReadModel(Exhibit exhibit)
        {
            if (exhibit == null) return null;
            return new ExhibitReadModel
            {
                Id = exhibit.Id,
                RBnumber = exhibit.RBnumber,
                IdInventoryCypher = exhibit.IdInventoryCypher,
                InventoryCypherName = exhibit.InventoryCypher?.NameCypher,
                InventoryNumber = exhibit.InventoryNumber,
                NameExhibit = exhibit.NameExhibit,
                IdAuthor = exhibit.IdAuthor,
                AuthorFullName = exhibit.Author != null ? $"{exhibit.Author.Lastname} {exhibit.Author.Firstname} {exhibit.Author.Middlename ?? ""}".Trim() : null,
                PeriodYearCreated = exhibit.PeriodYearCreated,
                Length = exhibit.Length,
                Width = exhibit.Width,
                Height = exhibit.Height,
                IdMeasure = exhibit.IdMeasure,
                MeasureName = exhibit.Measure?.NameMeasure,
                IdTechnique = exhibit.IdTechnique,
                TechniqueName = exhibit.Technique?.NameTechnique,
                IdRecievingWay = exhibit.IdRecievingWay,
                RecievingWayName = exhibit.RecievingWay?.NameWay,
                CommonDescription = exhibit.CommonDescription,
                DamageDescription = exhibit.DamageDescription,
                IdStoringType = exhibit.IdStoringType,
                StoringTypeName = exhibit.StoringType?.NameStoringType,
                Picture = exhibit.Picture, // Handle file retrieval appropriately
                DateRecordCreated = exhibit.DateRecordCreated,
                DateRecordLastUpdated = exhibit.DateRecordLastUpdated,
                Materials = exhibit.MaterialExhibits?
                            .Select(me => me.Material == null ? null : new MaterialReadModel { Id = me.Material.Id, NameMaterial = me.Material.NameMaterial })
                            .Where(m => m != null)
                            .ToList() ?? new List<MaterialReadModel>()
            };
        }
    }
}
