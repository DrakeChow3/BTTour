﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TourApp.Const;
using TourApp.Context;
using TourApp.Entity;
using TourApp.Repository.IRepository;

namespace TourApp.Repository
{
    public class NhanVienRepository : INhanVienRepository
    {
        private TourContext _context;

        public NhanVienRepository(TourContext context)
        {
            _context = context;
        }


        public void Add(NhanVien entity)
        {
            _context.NhanViens.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(NhanVien entity)
        {
            entity.isDeleted = Status.Deleted;
            _context.NhanViens.Update(entity);
            _context.SaveChanges();
        }

        public IEnumerable<NhanVien> getAll()
        {
            return _context.NhanViens.Where(t => t.isDeleted == Status.NotDeleted).ToList();
        }
        public IEnumerable<NhanVien> getAllDelete()
        {
            return _context.NhanViens.ToList();
        }
        public NhanVien getById(int NVId = 1, string MaNV = "abc")
        {
            return _context.NhanViens.Where(t => t.NVId == NVId || t.MaNV == MaNV)                               
                                       .FirstOrDefault();
        }

        public IEnumerable<NhanVien> getWhere(string Ten, int isDeleted)
        {
            return _context.NhanViens.Where(t => t.Ten.Contains(Ten))
                                 .Where(t => t.isDeleted == isDeleted)
                                       .ToList();
        }

        public void Update(NhanVien entity)
        {
            _context.NhanViens.Update(entity);
            _context.SaveChanges();
        }
    }
}
