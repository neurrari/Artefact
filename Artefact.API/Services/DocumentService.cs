using Artefact.API.Data;
using Artefact.API.Data.Dtos.Document;
using Artefact.API.Data.Models;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly BaseDbContext _context;

        public DocumentService(BaseDbContext context)
        {
            _context = context;
        }
        public async Task<DocumentReadModel> CreateAsync(DocumentCreateModel dto)
        {
            var document = new Document
            {
                DocumentNumber = dto.DocumentNumber,
                DateCreated = DateTime.UtcNow,
                IdDocumentType = dto.IdDocumentType,
                IdExhibition = dto.IdExhibition
            };

            // Handle Many-to-Many ExhibitDocuments
            if (dto.ExhibitIds != null && dto.ExhibitIds.Any())
            {
                document.ExhibitDocuments = dto.ExhibitIds.Select(exhibitId => new ExhibitDocument { IdExhibit = exhibitId }).ToList();
            }

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            var createdDocument = await _context.Documents
                                    .Include(d => d.DocumentType)
                                    .Include(d => d.Exhibition)
                                    .Include(d => d.ExhibitDocuments).ThenInclude(ed => ed.Exhibit)
                                    .FirstOrDefaultAsync(d => d.Id == document.Id);

            return MapToReadModel(createdDocument);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var document = await _context.Documents.FindAsync(id);
            if (document == null) return false;
            // Associated ExhibitDocuments usually deleted by cascade or need manual removal
            _context.Documents.Remove(document);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<DocumentReadModel>> GetAllAsync()
        {
            return await _context.Documents
                    .Include(d => d.DocumentType)
                    .Include(d => d.Exhibition)
                    .Include(d => d.ExhibitDocuments).ThenInclude(ed => ed.Exhibit)
                    .Select(d => MapToReadModel(d))
                    .ToListAsync();
        }

        public async Task<DocumentReadModel?> GetByIdAsync(int id)
        {
            var document = await _context.Documents
                                    .Include(d => d.DocumentType)
                                    .Include(d => d.Exhibition)
                                    .Include(d => d.ExhibitDocuments).ThenInclude(ed => ed.Exhibit)
                                    .FirstOrDefaultAsync(d => d.Id == id);

            return document == null ? null : MapToReadModel(document);
        }

        public async Task<bool> UpdateAsync(int id, DocumentUpdateModel dto)
        {
            var document = await _context.Documents
                                   .Include(d => d.ExhibitDocuments)
                                   .FirstOrDefaultAsync(d => d.Id == id);
            if (document == null) return false;

            if (dto.DocumentNumber != null) document.DocumentNumber = dto.DocumentNumber;
            if (dto.IdDocumentType.HasValue) document.IdDocumentType = dto.IdDocumentType.Value;
            document.IdExhibition = dto.IdExhibition;

            // Handle Many-to-Many ExhibitDocuments update
            if (dto.ExhibitIds != null)
            {
                document.ExhibitDocuments.RemoveAll(ed => !dto.ExhibitIds.Contains(ed.IdExhibit));
                
                var existingExhibitIds = document.ExhibitDocuments.Select(ed => ed.IdExhibit).ToList();
                var newExhibitIds = dto.ExhibitIds.Except(existingExhibitIds);
                foreach (var exhibitId in newExhibitIds)
                {
                    document.ExhibitDocuments.Add(new ExhibitDocument { IdExhibit = exhibitId });
                }
            }

            _context.Entry(document).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Documents.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }

        // Helper method for mapping Document to DocumentReadModel
        private static DocumentReadModel MapToReadModel(Document document)
        {
            if (document == null) return null;
            return new DocumentReadModel
            {
                Id = document.Id,
                DocumentNumber = document.DocumentNumber,
                DateCreated = document.DateCreated,
                IdDocumentType = document.IdDocumentType,
                DocumentTypeName = document.DocumentType?.NameDocumentType,
                IdExhibition = document.IdExhibition,
                ExhibitionName = document.Exhibition?.NameExhibition,
                Exhibits = document.ExhibitDocuments? 
                            .Select(ed => ed.Exhibit == null ? null : new ExhibitMinimalReadModel
                            {
                                Id = ed.Exhibit.Id,
                                NameExhibit = ed.Exhibit.NameExhibit,
                                InventoryNumber = ed.Exhibit.InventoryNumber 
                            })
                            .Where(e => e != null)
                            .ToList() ?? new List<ExhibitMinimalReadModel>()
            };
        }
    }
}