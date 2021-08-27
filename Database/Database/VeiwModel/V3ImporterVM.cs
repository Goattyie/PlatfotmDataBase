﻿using Database.Services;
using Database.Services.ExcelParser;
using Database.VeiwModel.Commands;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Threading;

namespace Database.VeiwModel
{
    class Error
    {
        public string Table { get; set; }
        public int Row { get; set; }
    }
    class V3ImporterVM : BasePropertyChanged
    {
        private Dispatcher _dispatcher;
        private string _availabilityFilename;
        private string _sellFilename;
        private BaseCommand _changeAvailabilityCommand;
        private BaseCommand _changeSellCommand;
        private BaseCommand _importCommand;
        private int _availabilityProgressBarValue;
        private int _sellProgressBarValue;
        private string _availabilityStatus;
        private string _sellStatus;
        private OpenFileDialog _fileDialog;
        private Thread _availabilityThread;
        private Thread _sellThread;

        #region Properties
        public BaseCommand ChangeAvailabilityCommand
        {
            get { return _changeAvailabilityCommand ??= new BaseCommand(obj => 
            { 
                SelectExcelFile();  
                _availabilityFilename = (_fileDialog.FileName != "") ? _fileDialog.FileName : null ;
                if (_availabilityFilename != null)
                    AvailabilityStatus = "Файл выбран";
            }); }
        }
        public BaseCommand ChangeSellComand
        {
            get { return _changeSellCommand ??= new BaseCommand(obj => { 
                SelectExcelFile(); 
                _sellFilename = (_fileDialog.FileName != "") ? _fileDialog.FileName : null;
                if (_sellFilename != null)
                    SellStatus = "Файл выбран";
            }); }
        }

        public BaseCommand ImportCommand
        {
            get { return _importCommand ??= new BaseCommand(obj => { ImportFiles();  }); }
        }
        public int AvailabilityProgressBarValue
        {
            get { return _availabilityProgressBarValue; }
            set { _availabilityProgressBarValue = value; OnPropertyChanged(nameof(AvailabilityProgressBarValue)); }
        }
        public int SellProgressBarValue
        {
            get { return _sellProgressBarValue; }
            set { _sellProgressBarValue = value; OnPropertyChanged(nameof(SellProgressBarValue)); }
        }
        public string AvailabilityStatus
        {
            get { return _availabilityStatus; }
            set { _availabilityStatus = value; OnPropertyChanged(nameof(AvailabilityStatus)); }
        }

        public string SellStatus
        {
            get { return _sellStatus; }
            set { _sellStatus = value; OnPropertyChanged(nameof(SellStatus)); }
        }
        #endregion
        public BindingList<Error> Errors { get; set; } = new BindingList<Error>();
        public V3ImporterVM(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
            AvailabilityStatus = "Файл не выбран";
            SellStatus = "Файл не выбран";
        }
        private void SelectExcelFile()
        {
            _fileDialog = new OpenFileDialog();
            _fileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            _fileDialog.ShowDialog();
        }

        private void ImportFiles()
        {
            if (_availabilityFilename != null)
            {
                _availabilityThread = new Thread(() =>
                {
                    var excelDownloader = new ExcelDownloader(new V3AvailabilityStrategy(), _availabilityFilename);
                    excelDownloader.NewIterationEvent += UpdateAvailabilityProgressBar;
                    excelDownloader.ErrorDownloadEvent += UpdateErrors;
                    excelDownloader.DownloadNodes();
                    _dispatcher.Invoke((Action)(() => 
                    {
                        Service.availabilityMapper.NotifyObserver();
                        Service.productMapper.NotifyObserver();
                        Service.cardMapper.NotifyObserver();
                    }));
                });
                _availabilityThread.Priority = ThreadPriority.Highest;
                _availabilityThread.IsBackground = false;
                _availabilityThread.Start();
            }

            if (_sellFilename != null)
            {
                _sellThread = new Thread(() =>
                {
                    var excelDownloader = new ExcelDownloader(new V3SellStrategy(), _sellFilename);
                    excelDownloader.NewIterationEvent += UpdateSellProgressBar;
                    excelDownloader.ErrorDownloadEvent += UpdateErrors;
                    excelDownloader.DownloadNodes();
                    _dispatcher.Invoke((Action)(() =>
                    {
                        Service.sellMapper.NotifyObserver();
                        Service.productMapper.NotifyObserver();
                        Service.cardMapper.NotifyObserver();
                        Service.clientMapper.NotifyObserver();
                    }));
                });
                _sellThread.Priority = ThreadPriority.Highest;
                _sellThread.IsBackground = false;
                _sellThread.Start();
            }

        }

        private void UpdateErrors(string arg1, int arg2)
        {
            _dispatcher.Invoke((Action)(() =>
            {
                Errors.Add(new Error() { Table = arg1, Row = arg2 });
            }));
        }

        private void UpdateSellProgressBar(int obj)
        {
            SellProgressBarValue = obj;
        }

        private void UpdateAvailabilityProgressBar(int obj)
        {
            AvailabilityProgressBarValue = obj;
        }
    }
}
