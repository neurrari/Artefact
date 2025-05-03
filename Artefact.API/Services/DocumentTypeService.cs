using Artefact.API.Data;
using Artefact.API.Data.Dtos.DocumentType;
using Artefact.API.Data.Models;
using Artefact.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Artefact.API.Services
{
    public class DocumentTypeService : IDocumentTypeService
    {
        private readonly BaseDbContext _context;

        public DocumentTypeService(BaseDbContext context)
        {
            _context = context;
        }
        public async Task<DocumentTypeReadModel> CreateAsync(DocumentTypeCreateModel dto)
        {
            var docType = new DocumentType { NameDocumentType = dto.NameDocumentType };
            _context.DocumentTypes.Add(docType);
            await _context.SaveChangesAsync();
            return new DocumentTypeReadModel { Id = docType.Id, NameDocumentType = docType.NameDocumentType };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var docType = await _context.DocumentTypes.FindAsync(id);
            if (docType == null) return false;
            _context.DocumentTypes.Remove(docType);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<DocumentTypeReadModel>> GetAllAsync()
        {
            return await _context.DocumentTypes
                .Select(dt => new DocumentTypeReadModel { Id = dt.Id, NameDocumentType = dt.NameDocumentType })
                .ToListAsync();
        }

        public async Task<DocumentTypeReadModel?> GetByIdAsync(int id)
        {
            var docType = await _context.DocumentTypes.FindAsync(id);
            return docType == null ? null : new DocumentTypeReadModel { Id = docType.Id, NameDocumentType = docType.NameDocumentType };
        }

        public async Task<bool> UpdateAsync(int id, DocumentTypeUpdateModel dto)
        {
            var docType = await _context.DocumentTypes.FindAsync(id);
            if (docType == null) return false;

            if (dto.NameDocumentType != null)
            {
                docType.NameDocumentType = dto.NameDocumentType;
            }
            _context.Entry(docType).State = EntityState.Modified;
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.DocumentTypes.AnyAsync(e => e.Id == id)) return false; else throw;
            }
        }
    }
}
