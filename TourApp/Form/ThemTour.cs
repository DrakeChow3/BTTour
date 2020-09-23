﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TourApp.Entity;
using TourApp.Repository.IRepository;

namespace TourApp
{
    public partial class ThemTour : Form
    {
        private readonly ITourRepository _tourRepository;
        private readonly IDiaDiemRepository _diadiemRepository;
        private readonly ICTTourRepository _cTTourRepository;
        private readonly IGiaRepository _giaRepository;

        public ThemTour(ITourRepository tourRepository,IDiaDiemRepository diaDiemRepository,ICTTourRepository cTTourRepository,IGiaRepository giaRepository)
        {
            _tourRepository = tourRepository;
            _diadiemRepository = diaDiemRepository;
            _cTTourRepository = cTTourRepository;
            _giaRepository = giaRepository;
            InitializeComponent();
        }

        private async Task showThongtin()
        {
            IEnumerable<DiaDiem> listdd = await _diadiemRepository.getAll();

            /*Dia Diem Tour*/
            foreach (var DD in listdd)
            {
                ListViewItem diaDiem = new ListViewItem(new[] { DD.DDId.ToString(), DD.TenDD });
                diadiem.Items.Add(diaDiem);
            }
            /* Loại hình */
            LoaiHinhDL item1 = new LoaiHinhDL();
            item1.LHDLId = 1;
            item1.Ten = "Loại 1";
            loaihinhcbb.Items.Add(item1);
            loaihinhcbb.DisplayMember = "Ten";
            loaihinhcbb.ValueMember = "LHDLId";
            loaihinhcbb.SelectedItem = item1;
            
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Tour newtour = new Tour();
            Gia newgia = new Gia(); 
            newtour.MaTour = matourtb.Text;
            newtour.Ten = tentourtb.Text;

            LoaiHinhDL item = new LoaiHinhDL();
            item = (LoaiHinhDL)loaihinhcbb.SelectedItem;
            newtour.LHDLId = item.LHDLId;
            await _tourRepository.Add(newtour);
            foreach (ListViewItem dd in diadiem.CheckedItems)
            {
                MessageBox.Show("hello");
                CTTour newcttour = new CTTour();
                newcttour.DDId = Convert.ToInt32(dd.SubItems[0].Text);
                newcttour.TourId = newtour.TourId;
                await _cTTourRepository.Add(newcttour);
            }
            newgia.GiaTri = (int)mucgia.Value;
            newgia.TGBD = tungay.Value;
            newgia.TGKT = denngay.Value;
            newgia.TourId = newtour.TourId;
           
            await _giaRepository.Add(newgia);


        }

        protected override async void OnLoad(EventArgs e)
        {
            await showThongtin();
        }

    }
}
