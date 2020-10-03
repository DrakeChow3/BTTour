﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TourApp.Entity;

namespace TourApp.Repository.IRepository
{
    public interface ITourRepository : ICommonRepository<Tour>
    {
        IEnumerable<Tour> getWhere(String Ten);
        Tour getById(int TourId, String MaTour);

    }
}
