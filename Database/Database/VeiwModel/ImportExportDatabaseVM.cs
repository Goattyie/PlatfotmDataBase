using Database.Services;
using Database.Services.ExcelParser;
using Database.VeiwModel.Commands;
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Database.VeiwModel
{
    class Error
    {
        public string Table { get; set; }
        public int Row { get; set; }
    }
    class ImportExportDatabaseVM : BasePropertyChanged
    {
        private Dispatcher _dispatcher;
        private Action _executeAction;
        private string _availabilityFilename;
        private string _sellFilename;
        private string _delProdFilename;
        private BaseCommand _changeDeliverProductCommand;
        private BaseCommand _changeAvailabilityCommand;
        private BaseCommand _changeSellCommand;
        private BaseCommand _executeCommand;
        private int _deliverProductBarBalue;
        private int _availabilityProgressBarValue;
        private int _sellProgressBarValue;
        private string _delProdStatus;
        private string _availabilityStatus;
        private string _sellStatus;
        private OpenFileDialog _fileDialog;
        private bool _startEnabled = true;

        #region Properties

        public bool StartEnabled
        {
            get { return _startEnabled; }
            set { _startEnabled = value; OnPropertyChanged(nameof(StartEnabled)); }
        }
        public BaseCommand ChangeDeliverProductCommand
        {
            get
            {
                return _changeDeliverProductCommand ??= new BaseCommand(obj =>
                {
                    SelectExcelFile();
                    _delProdFilename = (_fileDialog.FileName != "") ? _fileDialog.FileName : null;
                    if (_delProdFilename != null)
                        DeliverProductStatus = "Файл выбран";
                });
            }
        }
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

        public BaseCommand ExecuteCommand
        {
            get { return _executeCommand ??= new BaseCommand(obj => { _executeAction();  }); }
        }
        public int DeliverProductProgressBarValue
        {
            get { return _deliverProductBarBalue; }
            set { _deliverProductBarBalue = value; OnPropertyChanged(nameof(DeliverProductProgressBarValue)); }
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
        public string DeliverProductStatus
        {
            get { return _delProdStatus; }
            set { _delProdStatus = value; OnPropertyChanged(nameof(DeliverProductStatus)); }
        }
       

        public string SellStatus
        {
            get { return _sellStatus; }
            set { _sellStatus = value; OnPropertyChanged(nameof(SellStatus)); }
        }
        #endregion
        public BindingList<Error> Errors { get; set; } = new BindingList<Error>();
        public ImportExportDatabaseVM(Dispatcher dispatcher, bool import)
        {
            if (import)
                _executeAction = new Action(ImportFiles);
            else
                _executeAction = new Action(ExportFiles);
            _dispatcher = dispatcher;
            AvailabilityStatus = "Файл не выбран";
            SellStatus = "Файл не выбран";
        }
        private void SelectExcelFile()
        {
            _fileDialog = new OpenFileDialog();
            _fileDialog.Filter = "Excel Files|*.xlsx;*.xlsm";
            _fileDialog.ShowDialog();
        }
        private void ExportFiles()
        {
            if (_availabilityFilename != null)
            {
                new Thread(() =>
                {
                    var excelWriter = new ExcelWriter(new AvailabilityWriterStrategy(), _availabilityFilename);
                    excelWriter.NewIterationEvent += UpdateAvailabilityProgressBar;
                    excelWriter.WriteNodes();
                })
                { Priority = ThreadPriority.Highest, IsBackground = false }.Start();
            }

            if (_sellFilename != null)
            {
                 new Thread(() =>
                {
                    var excelWriter = new ExcelWriter(new SellWriterStrategy(), _sellFilename);
                    excelWriter.NewIterationEvent += UpdateSellProgressBar;
                    excelWriter.WriteNodes();
                })
                 { Priority = ThreadPriority.Highest, IsBackground = false }.Start();
            }

            if(_delProdFilename != null)
            {
                new Thread(() =>
                {
                    var excelWriter = new ExcelWriter(new DeliverProductWriterStrategy(), _delProdFilename);
                    excelWriter.NewIterationEvent += UpdateDeliverProductProgressBar;
                    excelWriter.WriteNodes();
                })
                { Priority = ThreadPriority.Highest, IsBackground = false }.Start();
            }
        }
        private void ImportFiles()
        {
            if (_availabilityFilename != null || _sellFilename != null || _delProdFilename != null)
            {
                new Thread(() =>
                {
                    StartEnabled = false;
                    ExcelReader excelDownloader;
                    if (_availabilityFilename != null)
                    {
                        excelDownloader = new ExcelReader(new AvailabilityReaderStrategy(), _availabilityFilename);
                        excelDownloader.NewIterationEvent += UpdateAvailabilityProgressBar;
                        excelDownloader.ErrorDownloadEvent += UpdateErrors;
                        excelDownloader.DownloadNodes();
                        _dispatcher.Invoke((Action)(() =>
                        {
                            Service.availabilityMapper.NotifyObserver();
                            Service.productMapper.NotifyObserver();
                            Service.cardMapper.NotifyObserver();
                            Service.profileMapper.NotifyObserver();
                        }
                        ));
                    }

                    if (_sellFilename != null)
                    {
                        excelDownloader = new ExcelReader(new SellReaderStrategy(), _sellFilename);
                        excelDownloader.NewIterationEvent += UpdateSellProgressBar;
                        excelDownloader.ErrorDownloadEvent += UpdateErrors;
                        excelDownloader.DownloadNodes();
                        _dispatcher.Invoke((Action)(() =>
                        {
                            Service.sellMapper.NotifyObserver();
                            Service.productMapper.NotifyObserver();
                            Service.cardMapper.NotifyObserver();
                            Service.clientMapper.NotifyObserver();
                        }
                        ));
                    }

                    if (_delProdFilename != null)
                    {
                        excelDownloader = new ExcelReader(new DeliverProductReaderStrategy(), _delProdFilename);
                        excelDownloader.NewIterationEvent += UpdateDeliverProductProgressBar;
                        excelDownloader.ErrorDownloadEvent += UpdateErrors;
                        excelDownloader.DownloadNodes();
                        _dispatcher.Invoke((Action)(() =>
                        {
                            Service.deliverMapper.NotifyObserver();
                            Service.productMapper.NotifyObserver();
                            Service.deliverProductMapper.NotifyObserver();
                        }
                        ));
                    }
                    StartEnabled = true;
                })  { Priority = ThreadPriority.Highest,  IsBackground = true  }.Start();
            }
        }
        private void UpdateDeliverProductProgressBar(int obj)
        {
            DeliverProductProgressBarValue = obj;
        }

        private async void UpdateErrors(string arg1, int arg2)
        {
            await Task.Run(() => {
                _dispatcher.Invoke((Action)(() =>
                {
                    Errors.Add(new Error() { Table = arg1, Row = arg2 });
                }));
            });

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
