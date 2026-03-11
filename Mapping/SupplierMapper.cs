using InvoiceAPI.DTOs;
using InvoiceAPI.Entity;

namespace InvoiceAPI.Mapping
{
    
        /// Manual Mapper for Supplier Entity and DTOs
    
        public static class SupplierMapper
        {
            //  Convert CreateDto → Entity (for POST)
            public static Supplier ToEntity(this SupplierCreateDto dto)
            {
                return new Supplier
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    MobileNo = dto.MobileNo,
                    GstNo = dto.GstNo,
                    Address = dto.Address
                };
            }

            //  Convert UpdateDto → Entity (for PUT/PATCH)
            public static Supplier ToEntity(this SupplierUpdateDto dto)
            {
                return new Supplier
                {
               
                    Name = dto.Name,
                    Email = dto.Email,
                    MobileNo = dto.MobileNo,
                    GstNo = dto.GstNo,
                    Address = dto.Address
                };
            }

            //  Convert Entity → ReadDto (for GET)
            public static SupplierReadDto ToReadDto(this Supplier entity)
            {
                return new SupplierReadDto
                {
                    SupplierId = entity.SupplierId,
                    Name = entity.Name,
                    Email = entity.Email,
                    MobileNo = entity.MobileNo,
                    GstNo = entity.GstNo,
                    Address = entity.Address
                };
            }

            //  Convert Entity → UpdateDto (useful when pre-filling an edit form)
            public static SupplierUpdateDto ToUpdateDto(this Supplier entity)
            {
                return new SupplierUpdateDto
                {
                
                    Name = entity.Name,
                    Email = entity.Email,
                    MobileNo = entity.MobileNo,
                    GstNo = entity.GstNo,
                    Address = entity.Address
                };
            }
        }
    }
